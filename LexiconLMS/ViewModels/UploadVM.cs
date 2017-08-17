namespace LexiconLMS.ViewModels
{
    public class UploadVM
    {
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public int PurposeId { get; set; }
        public string ReturnTo { get; set; }
        public string CourseName { get; set; }
        public string ModuleName { get; set; }
        public string ActivityName { get; set; }
        public string UserName { get; set; }
    }
}