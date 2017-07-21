using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.ViewModels
{
    public class CreateStudentAccountVM : CreateTeacherAccountVM
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}