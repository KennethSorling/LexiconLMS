using LexiconLMS.Models;
using System;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class DashboardVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string StudentName { get; set; }

        public DateTime TodaysDate { get; set; }

        public string ModuleName { get; set; }

        public bool ModuleExists { get; set; }

        public bool ActivityExists { get; set; }

        public List<Activity> ActivitiesList { get; set; }

        public List<string> ActivityTypesList { get; set; }
    }
}