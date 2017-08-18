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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateReviewed { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}