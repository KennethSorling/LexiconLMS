using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string TeacherId { get; set; }
        [Display(Name ="Teacher")]
        public string TeacherName { get; set; }
        [Display(Name = "Student")]
        public string StudentName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Reviewed")]
        public DateTime DateReviewed { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Comments { get; set; }
    }
}