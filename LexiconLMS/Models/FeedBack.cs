namespace LexiconLMS.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Comments { get; set; }
    }
}