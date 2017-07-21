namespace LexiconLMS.ViewModels
{
    public class StudentListVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public string Email { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}