using LexiconLMS.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses

        /// <summary>
        /// Provides an manageable overview of the details of a course
        /// </summary>
        /// <param name="id">id of the course in question</param>
        /// <returns>A view of the course</returns>
        [Authorize(Roles="Teacher")]
        public ActionResult Manage(int id)
        {

            //var course = db.Courses.Find(id);
            var course = db.Courses.Include("Documents").Where(c => c.Id == id).FirstOrDefault();

            if (course == null)
            {
                return HttpNotFound();
            }
            /*
             * This is because EF doesn't automatically populate the students member
             */
            var students = db.Users
                .Where(s => s.CourseId == id).ToList()
                .ConvertAll(s => new Student
                {
                    Id = s.Id,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNumber = s.PhoneNumber
                });
            course.Students = students;

            //course.Students = students;
            ViewBag.Title = "Course Details";
            return View(course);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            var courses = db.Courses.OrderByDescending(s => s.StartDate);
            ViewBag.Title = "Courses";

            return View("Index", courses.ToList());
        }

        //// GET: Courses/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = db.Courses.Find(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        // GET: Courses/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            var sibling = db.Courses.Where(c => c.Name == course.Name).FirstOrDefault();
            if (sibling != null)
            {
                ModelState.AddModelError("", $"There is already a course named '{sibling.Name}'.");
            }

            if (ModelState.IsValid)
            {
                course.DateChanged = System.DateTime.Now;
                db.Courses.Add(course);
                db.SaveChanges();

                TempData["Message"] = "The new course " + '"' + course.Name + '"' + " was added";

                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            DateTime earliest = course.StartDate;
            DateTime latest = course.EndDate;

            var modules = db.Modules.Where(m => m.CourseId == course.Id).ToList();
            foreach (var sibling in modules)
            {
                if (sibling.StartDate < earliest) earliest = sibling.StartDate;
                if (sibling.EndDate > latest) latest = sibling.EndDate;

            }

            if (course.StartDate > earliest)
            {
                ModelState.AddModelError("StartDate", $"Course cannot start later than its earliest module ({earliest.ToShortDateString()})");
            }
            if (course.EndDate < latest)
            {
                ModelState.AddModelError("EndDate", $"Course cannot end sooner than its last module does ({latest.ToShortDateString()})");
            }

            /*
             * Try to ascertain if user is attempting  to rename the course, and in that case to what.
             */

            var conflict = db.Courses.Where(c => (c.Name.ToLower() == course.Name.ToLower()) && c.Id != course.Id).FirstOrDefault() ;
            if (conflict != null)
            {
                ModelState.AddModelError("Name", "There is another course by this name already.");
            }

            if (ModelState.IsValid)
            {
                course.DateChanged = System.DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = "Edits for the course " + '"' + course.Name + '"' + " were saved";

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();

            TempData["Message"] = "The course " + '"' + course.Name + '"' + " was deleted";

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
