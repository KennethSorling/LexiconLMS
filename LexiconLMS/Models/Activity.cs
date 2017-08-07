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
        public int ActivityTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Date Approved")]
        public DateTime? DateApproved { get; set; }

        public ActivityType Type { get; set; }

        [Display(Name = "External Lecturer")]
        public Boolean External { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}