using LexiconLMS.Models;
using System;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class CoursesVM
    {
        public int Id { get; set; }

        public string ViewTitle { get; set; }

        public List<Course> Courses { get; set; }
    }
}