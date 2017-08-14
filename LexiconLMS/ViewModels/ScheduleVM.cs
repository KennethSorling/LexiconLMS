using System;
using System.Collections.Generic;

namespace LexiconLMS.ViewModels
{
    public class ScheduleVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool ModuleExists { get; set; }

        //public bool ModuleExists { get; set; }

        //public bool ActivityExists { get; set; }

        public List<ScheduleRow> ScheduleRowList { get; set; }
    }

    public class ScheduleRow
    {
        public string ScheduleRowDate { get; set; }

        public string ScheduleRowWeekDay { get; set; }

        public string ModuleName { get; set; }

        public List<string> AmActivity { get; set; }

        public List<string> PmActivity { get; set; }

        public bool External { get; set; }
    }
}