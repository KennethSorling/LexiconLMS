using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class CreateEditActivityVM
    {
        public int Id { get; set; }

        public int CourseID { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [Display(Name="Type")]
        public int ActivityTypeId { get; set; }

        [Display(Name="Course")]
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name="Start Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="yyyy-MM-dd")]
        public DateTime StartDate { get; set; }

        [Display(Name="Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        
        [Display(Name="End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }

        [Display(Name="External Lecturer")]
        public bool External { get; set; }
    }
}