using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string Filename { get; set; }
        public string Title { get; set; }

        [Display(Name ="File Type")]
        public string FileType { get; set; }

        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public string UserId { get; set; }

        [Required]
        [Display(Name ="Date Uploaded")]
        public DateTime DateUploaded { get; set; }

        public DateTime? DeadLine { get; set; }
    }
}