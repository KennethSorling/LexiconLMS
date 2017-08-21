using LexiconLMS.Models;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class ActivitiesForStudentVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string ModuleName { get; set; }

        public List<Activity> Activities { get; set; }

        public List<string> ActivityTypes { get; set; }
    }
}