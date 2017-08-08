using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LexiconLMS.Models;

namespace LexiconLMS.ViewModels
{
    public class CourseManageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        List<Module> Modules { get; set; }
        List<Student> Students { get; set; }
    }

    public class ModuleManageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Activity> Activities { get; set; }
    }
}