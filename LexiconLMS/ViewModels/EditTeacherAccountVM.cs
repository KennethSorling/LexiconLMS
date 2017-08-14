using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class EditTeacherAccountVM
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

}