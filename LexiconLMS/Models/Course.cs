using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0: d MMM yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0: d MMM yyyy}")]
        public DateTime EndDate { get; set; }

        public virtual List<Module> Modules { get; set; }

        public virtual List<Document> Documents { get; set; }
        public virtual List<Course> Students { get; set; }
    }
}