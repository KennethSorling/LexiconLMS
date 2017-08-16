using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class DashboardVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// ShowDashboard is the default action for a student. It collects the information 
        /// that is relevant for the logged in student for the current date.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDashboard()
        {
            ApplicationUser currentUser = db.Users
                .Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            DashboardVM dashboard = new DashboardVM();

            string activityType = " ";

            var courseId = currentUser.CourseId;

            var course = db.Courses.Find(courseId);

            var currentDate = DateTime.Now;

            dashboard.CourseName = course.Name;
            dashboard.StudentName = currentUser.FirstName + " " + currentUser.LastName;
            dashboard.TodaysDate = currentDate;
            dashboard.ModuleExists = true;
            dashboard.ActivityExists = true;

            List<Activity> activities = new List<Activity>();

            var module = db.Modules.Where(c => c.CourseId == courseId)
                                            .Where(s => s.StartDate <= currentDate)
                                            .Where(e => e.EndDate >= currentDate)
                                            .FirstOrDefault();

            if (module != null)
            {
                dashboard.ModuleName = module.Name;

                var moduleId = module.Id;

                activities = db.Activities.Where(m => m.ModuleId == moduleId)
                                                        .ToList();

                List<Activity> dashboardActivities = new List<Activity>();

                //Filter for activities valid for the current date only
                if (activities != null)
                {
                    foreach (var item in activities)
                    {
                        if (item.StartDate.ToString("yyyy-MM-dd") == currentDate.ToString("yyyy-MM-dd"))
                        {
                            dashboardActivities.Add(item);
                        }
                    }
                }

                if (dashboardActivities.Count > 0)
                {
                    List<string> typenames = new List<string>();
                    foreach (var item in dashboardActivities)
                    {
                        //The following is only needed if activities are allowed to span more than one day. Not yet implemented.
                        //Check if the start date of the activity is less than the current date. If it is the set start time = 8:30.
                        //Check if the end date of the activity is greater than the current date. If it is the set end time = 17:00.

                        //Read activity type names into a List of strings
                        activityType = db.ActivityTypes.Find(item.ActivityTypeId).TypeName;
                        typenames.Add(activityType);
                    }
                    dashboard.ActivitiesList = dashboardActivities;
                    dashboard.ActivityTypesList = typenames;
                }
                else
                {
                    dashboard.ActivityExists = false;
                }
            }
            else
            {
                dashboard.ModuleExists = false;
                dashboard.ActivityExists = false;
            }

            return View("Dashboard", dashboard);
        }


        // GET: DashboardVMs
        //public ActionResult Index()
        //{
        //    return View(db.DashboardVMs.ToList());
        //}

        //// GET: DashboardVMs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DashboardVM dashboardVM = db.DashboardVMs.Find(id);
        //    if (dashboardVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dashboardVM);
        //}

        //// GET: DashboardVMs/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DashboardVMs/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,PageHeader,CourseName,StudentName,TodaysDate,ModuleName")] DashboardVM dashboardVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DashboardVMs.Add(dashboardVM);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dashboardVM);
        //}

        //// GET: DashboardVMs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DashboardVM dashboardVM = db.DashboardVMs.Find(id);
        //    if (dashboardVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dashboardVM);
        //}

        //// POST: DashboardVMs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,PageHeader,CourseName,StudentName,TodaysDate,ModuleName")] DashboardVM dashboardVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dashboardVM).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(dashboardVM);
        //}

        //// GET: DashboardVMs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DashboardVM dashboardVM = db.DashboardVMs.Find(id);
        //    if (dashboardVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dashboardVM);
        //}

        //// POST: DashboardVMs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DashboardVM dashboardVM = db.DashboardVMs.Find(id);
        //    db.DashboardVMs.Remove(dashboardVM);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
