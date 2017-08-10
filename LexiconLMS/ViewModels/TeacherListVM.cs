using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class TeacherListVM
	{
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name => FirstName + " " + LastName;

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        public int CourseId { get; set; }
        [Display(Name = "Course")]
        public string CourseName { get; set; }


    }
}