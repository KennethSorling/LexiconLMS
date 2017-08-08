using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.Type);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        public ActionResult Manage(int id)
        {
            var activity = db.Activities.Where(a => a.Id == id).FirstOrDefault();
            var module = db.Modules.Where(m => m.Id == activity.ModuleId).FirstOrDefault();
            var course = db.Courses.Where(c => c.Id == module.CourseId).FirstOrDefault();
            ViewBag.ModuleName = module.Name;
            ViewBag.CourseName = course.Name;
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create(int moduleId)
        {
            var module = db.Modules.Where(m => m.Id == moduleId).FirstOrDefault();
            var course = db.Courses.Where(c => c.Id == module.CourseId).FirstOrDefault();

            ViewBag.CourseName = course.Name;
            ViewBag.ModuleName = module.Name;
            ViewBag.ModuleId = moduleId;

            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModuleId,ActivityTypeId,Name,Description,StartDate,StartTime,EndTime,External")] CreateEditActivityVM activityVM)
        {
            Activity activity;
            if (ModelState.IsValid)
            {
                activity = new Models.Activity
                {
                    ModuleId = activityVM.ModuleId,
                    Name = activityVM.Name,
                    Description = activityVM.Description,
                    External = activityVM.External,
                    Type = db.ActivityTypes.Where(t => t.Id == activityVM.ActivityTypeId).FirstOrDefault(),
                    StartDate = activityVM.StartDate.Date.Add(activityVM.StartTime.TimeOfDay),
                    EndDate = activityVM.StartDate.Date.Add(activityVM.EndTime.TimeOfDay)
                };
                db.Activities.Add(activity);
                db.SaveChanges();
                TempData["Message"] = "Activity Created.";
                return RedirectToAction("Manage","Modules", new { Id = activity.ModuleId });
            }

            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activityVM.ActivityTypeId);
            return View("Create", activityVM);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            var module = db.Modules.Where(m => m.Id == activity.ModuleId).FirstOrDefault();
            ViewBag.ModuleName = module.Name;
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activity.ActivityTypeId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModuleId,ActivityTypeId,Name,Description,StartDate,EndDate,DateApproved,External")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activity.ActivityTypeId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int moduleId;
            Activity activity = db.Activities.Find(id);
            moduleId = activity.ModuleId;
            db.Activities.Remove(activity);
            db.SaveChanges();
            TempData["Message"] = "Activity Deleted.";
            return RedirectToAction("Manage", "Modules", new { id= moduleId });
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
