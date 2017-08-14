using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Activities
        //public ActionResult Index()
        //{
        //    var activities = db.Activities.Include(a => a.Type);
        //    return View(activities.ToList());
        //}

        //// GET: Activities/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Activity activity = db.Activities.Find(id);
        //    if (activity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(activity);
        //}

        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int moduleId)
        {
            //var module = db.Modules.Where(m => m.Id == moduleId).FirstOrDefault();
            var module = db.Modules.Find(moduleId);
            if (module == null)
            {
                return HttpNotFound();
            }
            //var course = db.Courses.Where(c => c.Id == module.CourseId).FirstOrDefault();
            var course = db.Courses.Find(module.CourseId);
            if (module == null)
            {
                return HttpNotFound();
            }
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
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,ModuleId,ActivityTypeId,Name,Description,StartDate,StartTime,EndTime,External")] CreateEditActivityVM activityVM)
        {
            Activity activity = new Models.Activity
                {
                    ModuleId = activityVM.ModuleId,
                    Name = activityVM.Name,
                    Description = activityVM.Description,
                    External = activityVM.External,
                    Type = db.ActivityTypes.Where(t => t.Id == activityVM.ActivityTypeId).FirstOrDefault(),
                    StartDate = activityVM.StartDate.Date.Add(activityVM.StartTime.TimeOfDay),
                    EndDate = activityVM.StartDate.Date.Add(activityVM.EndTime.TimeOfDay)
                };

            int moduleId = 0;

            if (activity.StartDate > activity.EndDate)
            {
                ModelState.AddModelError("EndDate", "The End Date must be later than or equal to the Start Date.");
            }
            moduleId = activity.ModuleId;
            var module = db.Modules.Find(moduleId);
            var course = db.Courses.Find(module.CourseId);
            if (activity.StartDate < module.StartDate)
            {
                ModelState.AddModelError("StartDate", $"The Start Date of the Activity must occur between {module.StartDate.ToShortDateString()} and {module.EndDate.ToShortDateString()}.");
            }
            if (activity.EndDate > module.EndDate)
            {
                ModelState.AddModelError("EndDate", $"The End Date of the Activity must occur between {module.StartDate.ToShortDateString()} and {module.EndDate.ToShortDateString()}.");
            }
            var siblings = db.Activities.Where(m => m.ModuleId == activity.ModuleId);
            foreach (var sibling in siblings)
            {
                if (Conflicts(activity, sibling))
                {
                    ModelState.AddModelError("", $"This activity's date/time conflicts with activity '{sibling.Name}");
                    break;
                }
            }


            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                course.DateChanged = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Activity Created.";
                return RedirectToAction("Manage", "Modules", new { Id = activity.ModuleId });
            }

            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activityVM.ActivityTypeId);
            return View("Create", activityVM);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "Teacher")]
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
            var module = db.Modules.Find(activity.ModuleId);
            var course = db.Courses.Find(module.CourseId);
            ViewBag.CourseName = course.Name;
            ViewBag.ModuleName = module.Name;
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activity.ActivityTypeId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,ModuleId,ActivityTypeId,Name,Description,StartDate,EndDate,DateApproved,External")] Activity activity)
        {
            int moduleId = 0;

            if (activity.StartDate > activity.EndDate)
            {
                ModelState.AddModelError("EndDate", "The End Date must be later than or equal to the Start Date.");
            }
            moduleId = activity.ModuleId;
            var module = db.Modules.Find(moduleId);
            var course = db.Courses.Find(module.CourseId);
            if (activity.StartDate < module.StartDate)
            {
                ModelState.AddModelError("StartDate", $"The Start Date of the Activity must occur between {module.StartDate.ToShortDateString()} and {module.EndDate.ToShortDateString()}.");
            }
            if (activity.EndDate > module.EndDate)
            {
                ModelState.AddModelError("EndDate", $"The End Date of the Activity must occur between {module.StartDate.ToShortDateString()} and {module.EndDate.ToShortDateString()}.");
            }
            var siblings = db.Activities.Where(m => (m.ModuleId == activity.ModuleId) && (m.Id != activity.Id));
            foreach (var sibling in siblings)
            {
                if (Conflicts(activity, sibling))
                {
                    ModelState.AddModelError("", $"This activity's date/time conflicts with activity '{sibling.Name}");
                    break;
                }
            }
            if (ModelState.IsValid)
            {
                moduleId = activity.ModuleId;
                db.Entry(activity).State = EntityState.Modified;
                /* Reflect this in the course entity */
                course.DateChanged = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Activity updated.";
                //Reflect the change in the course

                return RedirectToAction("Manage", "Modules", new { id = moduleId });
            }
            ViewBag.CourseName = course.Name;
            ViewBag.ModuleName = module.Name;
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "TypeName", activity.ActivityTypeId);
            return View(activity);
        }

        private bool Conflicts(Activity a, Activity b)
        {
            // two activities confilict if either date of one is found within
            // the other. Thus we test this both ways

            return 
                ((a.StartDate >= b.StartDate) && (a.StartDate <= b.EndDate)) ||
                ((a.EndDate >= b.StartDate) && (a.EndDate <= b.EndDate)) ||
                ((b.StartDate >= a.StartDate) && (b.StartDate <= a.EndDate)) ||
                ((b.EndDate >= a.StartDate) && (b.EndDate <= a.EndDate));
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
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
