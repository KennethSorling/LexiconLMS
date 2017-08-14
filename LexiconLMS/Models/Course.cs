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
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Date Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime DateChanged { get; set; }
        public virtual List<Module> Modules { get; set; }

        public virtual List<Document> Documents { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}