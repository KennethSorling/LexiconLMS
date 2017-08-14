using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            var currentDate = DateTime.Now.Date;

            dashboard.CourseName = course.Name;
            dashboard.StudentName = currentUser.FirstName + " " + currentUser.LastName;
            dashboard.TodaysDate = currentDate;
            dashboard.ModuleExists = true;
            dashboard.ActivityExists = true;


            var module = db.Modules.Where(c => c.CourseId == courseId)
                                            .Where(s => s.StartDate <= currentDate)
                                            .Where(e => e.EndDate >= currentDate)
                                            .FirstOrDefault();

            if (module != null)
            {
                dashboard.ModuleName = module.Name;

                var moduleId = module.Id;
                dashboard.ActivitiesList = db.Activities.Where(m => m.ModuleId == moduleId).ToList();
                if (dashboard.ActivitiesList.Count() > 0)
                {
                    //Read activity type names into a List of strings
                    List<string> typenames = new List<string>();
                    foreach (var item in dashboard.ActivitiesList)
                    {
                        activityType = db.ActivityTypes.Find(item.ActivityTypeId).TypeName;
                        typenames.Add(activityType);
                    }
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
            }

            return View("Dashboard", dashboard);
        }


        // GET: DashboardVMs
        public ActionResult Index()
        {
            return View(db.DashboardVMs.ToList());
        }

        // GET: DashboardVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardVM dashboardVM = db.DashboardVMs.Find(id);
            if (dashboardVM == null)
            {
                return HttpNotFound();
            }
            return View(dashboardVM);
        }

        // GET: DashboardVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PageHeader,CourseName,StudentName,TodaysDate,ModuleName")] DashboardVM dashboardVM)
        {
            if (ModelState.IsValid)
            {
                db.DashboardVMs.Add(dashboardVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dashboardVM);
        }

        // GET: DashboardVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardVM dashboardVM = db.DashboardVMs.Find(id);
            if (dashboardVM == null)
            {
                return HttpNotFound();
            }
            return View(dashboardVM);
        }

        // POST: DashboardVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PageHeader,CourseName,StudentName,TodaysDate,ModuleName")] DashboardVM dashboardVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dashboardVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dashboardVM);
        }

        // GET: DashboardVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardVM dashboardVM = db.DashboardVMs.Find(id);
            if (dashboardVM == null)
            {
                return HttpNotFound();
            }
            return View(dashboardVM);
        }

        // POST: DashboardVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DashboardVM dashboardVM = db.DashboardVMs.Find(id);
            db.DashboardVMs.Remove(dashboardVM);
            db.SaveChanges();
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
