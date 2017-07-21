using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.ViewModels
{
    public class EditStudentAccountVM
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required]
        public int CourseId { get; set; }

    }

}