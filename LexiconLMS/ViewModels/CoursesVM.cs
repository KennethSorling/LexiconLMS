using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class CoursesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public virtual List<Module> Modules { get; set; }
        public virtual List<Document> Documents { get; set; }

        public string ViewTitle { get; set; }
        public List<Course> Courses { get; set; }

    }
}