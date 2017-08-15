using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class ScheduleVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScheduleVMs
        public ActionResult ShowSchedule(int? courseId)
        {
            if (courseId == null)
            {
                ApplicationUser currentUser = db.Users
                    .Where(u => u.UserName == User.Identity.Name)
                    .FirstOrDefault();

                courseId = currentUser.CourseId;
            }

            ScheduleVM schedule = new ScheduleVM();

            //var courseId = currentUser.CourseId;

            var course = db.Courses.Find(courseId);

            //Activities List for one module
            List<Activity> activities = new List<Activity>();

            //Activities List for all modules
            List<Activity> completeActivitiesList = new List<Activity>();

            //Activities List for all modules sorted on start date
            List<Activity> sortedActivities = new List<Activity>();


            //Read modules from database for this course
            var modules = db.Modules.Where(c => c.CourseId == courseId)
                            .OrderByDescending(s => s.StartDate).ToList();

            schedule.CourseName = course.Name;
            schedule.ModuleExists = true;

            //Read all activities for all modules from the database if modules were found for the course 
            if (modules.Count > 0)
            {
                foreach (var item in modules)
                {
                    //Read activities for one module
                    var moduleId = item.Id;
                    activities = db.Activities.Where(m => m.ModuleId == moduleId)
                                                            .ToList();
                    foreach (var activityItem in activities)
                    {
                        completeActivitiesList.Add(activityItem);
                    }
                }

                //Sort all activities according to start date
                sortedActivities = completeActivitiesList.OrderByDescending(s => s.StartDate).ToList();
                string activityType = " ";

                //Calculate number of schedule rows based course start date and end date
                TimeSpan courseSpan;
                courseSpan = course.EndDate - course.StartDate;
                int numberOfDays = int.Parse((courseSpan.Days + 1).ToString());

                List<ScheduleRow> rowList = new List<ScheduleRow>();

                //Populate viewmodel properties by iterating over each row in the schedule
                for (int i = 0; i < numberOfDays; i++)
                {
                    //Instantiate a new scheduleRow in the viewmodel
                    ScheduleRow scheduleRow = new ScheduleRow();

                    //Assign values to the ScheduleRowDate and ScheduleRowWeekDay properties in the viewmodel
                    scheduleRow.ScheduleRowDate = course.StartDate.AddDays(i).ToShortDateString();
                    scheduleRow.ScheduleRowWeekDay = course.StartDate.AddDays(i).DayOfWeek.ToString();

                    //Check if there is a module for this date
                    var module = modules.Where(s => s.StartDate <= course.StartDate.AddDays(i))
                                            .Where(e => e.EndDate >= course.StartDate.AddDays(i - 1))
                                            .FirstOrDefault();

                    //If there is a module for this date, assign it to the viewmodel,
                    //else assign an empty string to the module name in the viewmodel.
                    if (module != null)
                    {
                        //Assign the module name to the viewmodel if it is weekday
                        if (course.StartDate.AddDays(i).DayOfWeek.ToString() != "Saturday" && course.StartDate.AddDays(i).DayOfWeek.ToString() != "Sunday")
                        {
                            scheduleRow.ModuleName = module.Name;

                            //Check if there are any activities for this date
                            var activitiesForThisDate = sortedActivities.Where(s => s.StartDate.Date == course.StartDate.Date.AddDays(i))
                                                            .OrderBy(t => t.StartDate.TimeOfDay)
                                                            .ToList();

                            //Read activity type names into a list
                            if (activitiesForThisDate.Count > 0)
                            {
                                List<string> amActivities = new List<string>();
                                List<string> pmActivities = new List<string>();

                                foreach (var item in activitiesForThisDate)
                                {
                                    //The following is only needed if activities are allowed to span more than one day. Not yet implemented.
                                    //Check if the start date of the activity is less than the current date. If it is the set start time = 8:30.
                                    //Check if the end date of the activity is greater than the current date. If it is the set end time = 17:00.


                                    //Read activity type names into a List of strings
                                    activityType = db.ActivityTypes.Find(item.ActivityTypeId).TypeName;

                                    //Check if the activity is in the morning
                                    if (item.StartDate.Hour < 12 && item.EndDate.Hour <= 12)
                                    {
                                        if (!item.External)
                                        {
                                            amActivities.Add(item.StartDate.ToShortTimeString() + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description);
                                        }
                                        else
                                        {
                                            amActivities.Add(item.StartDate.ToShortTimeString() + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description + " (EXT)");
                                        }
                                    }
                                    //Check if the activity is in the afternoon
                                    else if (item.StartDate.Hour >= 12)
                                    {
                                        if (!item.External)
                                        {
                                            pmActivities.Add(item.StartDate.ToShortTimeString() + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description);
                                        }
                                        else
                                        {
                                            pmActivities.Add(item.StartDate.ToShortTimeString() + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description + " (EXT)");
                                        }
                                    }
                                    //The activity starts before lunch and finishes after lunch. Split it in one morning activity and one
                                    //afternoon activity.
                                    else
                                    {
                                        if (!item.External)
                                        {
                                            amActivities.Add(item.StartDate.ToShortTimeString() + " - " + "12:00"
                                                                + ": " + activityType + ", " + item.Description);
                                            pmActivities.Add("13:00" + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description);
                                        }
                                        else
                                        {
                                            amActivities.Add(item.StartDate.ToShortTimeString() + " - " + "12:00"
                                                                + ": " + activityType + ", " + item.Description + " (EXT)");
                                            pmActivities.Add("13:00" + " - " + item.EndDate.ToShortTimeString()
                                                                + ": " + activityType + ", " + item.Description + " (EXT)");
                                        }
                                    }
                                }
                                scheduleRow.AmActivity = amActivities;
                                scheduleRow.PmActivity = pmActivities;
                            }
                        }
                        else
                        {
                            scheduleRow.ModuleName = "";
                        }
                    }
                    else
                    {
                        scheduleRow.ModuleName = "";
                    }

                    rowList.Add(scheduleRow);
                }
                schedule.ScheduleRowList = rowList;
            }
            else
            {
                schedule.ModuleExists = false;
            }

            schedule.UpdatedDate = DateTime.Now; //course.UpdatedDate;
            return View("Schedule", schedule);
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