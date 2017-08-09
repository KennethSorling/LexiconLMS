using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Display(Name = "Module Id")]
        public int ModuleId { get; set; }

        [Required]
        [Display(Name="Type")]
        public int ActivityTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]

        [Display(Name = "Date Approved")]
        public DateTime? DateApproved { get; set; }

        public ActivityType Type { get; set; }

        [Display(Name = "External Lecturer")]
        public Boolean External { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}