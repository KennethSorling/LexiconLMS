using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.ViewModels
{
    public class UploadVM
    {
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public int PurposeId { get; set; }
        public string ReturnTo { get; set; }
        public string CourseName { get; set; }
        public string ModuleName { get; set; }
        public string ActivityName { get; set; }
        public string UserName { get; set; }
        public int AssignmentDocId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeadLine { get; set; }

        public List<SelectListItem> Purposes { get; set; }
    }
}