using LexiconLMS.Models;
using System;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class DashboardVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string StudentName { get; set; }

        public DateTime TodaysDate { get; set; }

        public string ModuleName { get; set; }

        public bool ModuleExists { get; set; }

        public bool ActivityExists { get; set; }

        public List<Activity> ActivitiesForTodayList { get; set; }

        public List<string> ActivityTypesForTodayList { get; set; }

        public List<Document> HandIns { get; set; }

        public List<AssignmentAndRowEmphasis> AssignmentDescriptionAndEmphasis { get; set; }

        public List<Document> OtherDocuments { get; set; }

        public List<FeedbackObject> FeedbackList { get; set; }

        public bool FilterHandIns { get; set; }

        public string AssignmentDescriptionFilename { get; set; }
    }

    public class FeedbackObject
    {
        public bool FeedbackExists { get; set; }

        public int FeedbackId { get; set; }
    }

    public class AssignmentAndRowEmphasis
    {
        public Document AssignmentDescription { get; set; }

        public string RowEmphasis { get; set; }
    }
}