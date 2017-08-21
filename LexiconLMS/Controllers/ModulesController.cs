using LexiconLMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Modules
        //public ActionResult Index(int? id)
        //{
        //    var modules = db.Modules.
        //                     Where(c => c.CourseId == id).OrderByDescending(s => s.StartDate).ToList();
        //    //var courses = db.Courses.Where(c => c.Id == id).OrderByDescending(s => s.StartDate).ToList();
        //    return View(modules);
        //}

        // GET: Modules/Details/5
        //
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Module module = db.Modules.Find(id);
        //    if (module == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(module);
        //}
        private bool Conflicts(Module a, Module b)
        {
            // two moduls confilict if either date of one is found within
            // the other. Thus we test this both ways

            return
                ((a.StartDate >= b.StartDate) && (a.StartDate <= b.EndDate)) ||
                ((a.EndDate >= b.StartDate) && (a.EndDate <= b.EndDate)) ||
                ((b.StartDate >= a.StartDate) && (b.StartDate <= a.EndDate)) ||
                ((b.EndDate >= a.StartDate) && (b.EndDate <= a.EndDate));
        }

        // GET: Modules/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int courseId)
        {
            var course = db.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (course != null)
            {
                ViewBag.CourseName = course.Name;
                var module = new Module { CourseId = course.Id };
                return View(module);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,CourseId,Name,Description,StartDate,EndDate")] Module module)
        {
            var courseId = module.CourseId;
            var course = db.Courses.Find(courseId);

            if (module.StartDate > module.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date can not be before Start Date.");
            }
            if (module.StartDate < course.StartDate)
            {
                ModelState.AddModelError("StartDate", $"Start Date must occur on or after Course Start Date ({course.StartDate.ToShortDateString()})");
            }
            if (module.EndDate > course.EndDate)
            {
                ModelState.AddModelError("EndDate", $"End Date must occur before or on Course End date ({course.EndDate.ToShortDateString()})");
            }

            var siblings = db.Modules.Where(m => m.CourseId == courseId);

            foreach (var sibling in siblings)
            {
                if (sibling.Name.ToLower().Equals( module.Name.ToLower()))
                {
                    ModelState.AddModelError("", $"There is already a Module named '{sibling.Name}' in this Course.");
                    break;
                }

                if (Conflicts(module, sibling))
                {
                    ModelState.AddModelError("", $"The module's dates conflict with module '{sibling.Name}'");
                    break;
                }
            }
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                course.DateChanged = System.DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Module saved.";
                return RedirectToAction("Manage", "Courses", new { id = courseId });
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            var course = db.Courses.Find(module.CourseId);
            ViewBag.CourseName = course.Name;
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Teacher")]
        public ActionResult Edit([Bind(Include = "Id,CourseId,Name,Description,StartDate,EndDate")] Module module)
        {
            var courseId = module.CourseId;
            var course = db.Courses.Find(courseId);

            if (module.StartDate > module.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date can not be before Start Date.");
            }
            if (module.StartDate < course.StartDate)
            {
                ModelState.AddModelError("StartDate", $"Start Date must occur on or after Course Start Date ({course.StartDate.ToShortDateString()})");
            }
            if (module.EndDate > course.EndDate)
            {
                ModelState.AddModelError("EndDate", $"End Date must occur before or on Course End date ({course.EndDate.ToShortDateString()})");
            }

            var siblings = db.Modules.Where(m => (m.CourseId == courseId) && (m.Id != module.Id));

            foreach (var sibling in siblings)
            {
                if (Conflicts(module, sibling))
                {
                    ModelState.AddModelError("", $"The module's dates conflict with module '{sibling.Name}'");
                    break;
                }
                if (sibling.Name.ToLower().Equals(module.Name.ToLower()))
                {
                    ModelState.AddModelError("Name", $"There is already a Module named '{sibling.Name}' in this Course.");
                    break;
                }

            }

            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                course.DateChanged = System.DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Module updated.";
                return RedirectToAction("Manage", "Courses", new { id= courseId });
            }
            ViewBag.CourseName = course.Name;
            return View(module);
        }

        public ActionResult Manage(int id)
        {
            var module = db.Modules.Where(m => m.Id == id).FirstOrDefault();
            var course = db.Courses.Where(c => c.Id == module.CourseId).FirstOrDefault();
            ViewBag.CourseName = course.Name;
            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            int courseId = 0;
            Module module = db.Modules.Find(id);
            courseId = module.CourseId;
            db.Modules.Remove(module);
            db.SaveChanges();
            TempData["Message"] = "Module deleted.";
            return RedirectToAction("Manage", "Courses", new { id = courseId });
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
