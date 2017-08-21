﻿using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Student")]
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

        public ActionResult ShowModules()
        {
            ApplicationUser currentUser = db.Users
                .Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            ModulesForStudentVM modulesForStudent = new ModulesForStudentVM();

            var courseId = currentUser.CourseId;

            var course = db.Courses.Find(courseId);

            modulesForStudent.CourseName = course.Name;

            modulesForStudent.Modules = db.Modules.Where(c => c.CourseId == courseId).ToList();

            return View("CourseModulesForStudent", modulesForStudent);
        }

        public ActionResult ShowActivities(int id)
        {
            ApplicationUser currentUser = db.Users
                .Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            ActivitiesForStudentVM activitiesForStudent = new ActivitiesForStudentVM();

            string activityType = " ";

            var moduleId = id;

            var module = db.Modules.Find(moduleId);

            var course = db.Courses.Find(module.CourseId);

            activitiesForStudent.CourseName = course.Name;
            activitiesForStudent.ModuleName = module.Name;

            activitiesForStudent.Activities = db.Activities.Where(m => m.ModuleId == moduleId).ToList();

            if (activitiesForStudent.Activities.Count > 0)
            {
                List<string> typenames = new List<string>();
                foreach (var item in activitiesForStudent.Activities)
                {
                    //The following is only needed if activities are allowed to span more than one day. Not yet implemented.
                    //Check if the start date of the activity is less than the current date. If it is the set start time = 8:30.
                    //Check if the end date of the activity is greater than the current date. If it is the set end time = 17:00.

                    //Read activity type names into a List of strings
                    activityType = db.ActivityTypes.Find(item.ActivityTypeId).TypeName;
                    typenames.Add(activityType);
                }
                activitiesForStudent.ActivityTypes = typenames;
            }

            return View("ActivitiesForStudent", activitiesForStudent);
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
