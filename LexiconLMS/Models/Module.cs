using System;
using System.Collections.Generic;

namespace LexiconLMS.Models
{
    public class Module
    {
        public int Id { get; set; } //will be passed to the Activities module in the controller
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Activity> Activities { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}
