using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class EditTeacherAccountVM
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}