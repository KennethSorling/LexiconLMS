using LexiconLMS.Models;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class ModulesForStudentVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public List<Module> Modules { get; set; }
    }
}