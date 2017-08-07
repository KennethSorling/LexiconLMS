using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        //
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string TypeName { get; set; }
    }
}