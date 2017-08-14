using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.ViewModels
{
    public class CreateStudentAccountVM : CreateTeacherAccountVM
    {
        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public bool ReturnToIndex { get; set;}
    }
}