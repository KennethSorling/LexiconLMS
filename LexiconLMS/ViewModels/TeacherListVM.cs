using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class TeacherListVM
    {
        public string Id { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display (Name ="Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}