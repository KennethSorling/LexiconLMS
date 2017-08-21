using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class StudentListVM
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public int CourseId { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }
    }
}