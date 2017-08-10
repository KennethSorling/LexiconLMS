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

        // GET: Modules/Create
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
        public ActionResult Create([Bind(Include = "Id,CourseId,Name,Description,StartDate,EndDate")] Module module)
        {
            if (ModelState.IsValid)
            {
                var courseId = module.CourseId;
                db.Modules.Add(module);
                db.SaveChanges();
                TempData["Message"] = "Module saved.";
                return RedirectToAction("Manage", "Courses", new { id = courseId });
            }

            return View(module);
        }

        // GET: Modules/Edit/5
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
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,Name,Description,StartDate,EndDate")] Module module)
        {
            if (ModelState.IsValid)
            {
                var courseId = module.CourseId;
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Module updated.";
                return RedirectToAction("Manage", "Courses", new { id= courseId });
            }
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
