using System;
using System.Collections.Generic;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? DateApproved { get; set; }
        public ActivityType Type { get; set; }
        public Boolean External { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}