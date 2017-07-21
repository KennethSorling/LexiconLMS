using System;
using System.Collections.Generic;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Module> Modules { get; set; } 
        public virtual List<Document> Documents { get; set; }
        public virtual List<Course> Students { get; set; }
    }
}