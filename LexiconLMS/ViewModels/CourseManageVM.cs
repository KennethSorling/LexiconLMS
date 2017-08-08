using LexiconLMS.Models;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class CourseManageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        List<Module> Modules { get; set; }
        List<Student> Students { get; set;}

    }

}