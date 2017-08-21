using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int MimeTypeId { get; set; }
        public int StatusId { get; set; }
        public int PurposeId { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
        public string OwnerId { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string Filename { get; set; }
        public int FileSize { get; set; }
        public string Title { get; set; }

        [Display(Name ="File Type")]
        public string FileType { get; set; }
        
        [Required]
        [Display(Name ="Date Uploaded")]
        public DateTime DateUploaded { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [DataType(DataType.DateTime)]
        public DateTime? DeadLine { get; set; }
        public virtual MimeType MimeType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual Purpose Purpose { get; set; }
    }
}