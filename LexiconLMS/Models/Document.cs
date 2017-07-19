using System;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Title { get; set; }
        public string FileType { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public string UserId { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}