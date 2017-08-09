using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.ViewModels
{
    public class ModulesVM  //Name to change 2/3
    {
        //Properties from Models.Module.cs
        public int      Id          { get; set; }
        public int      CourseId    { get; set; }
        [Display(Name = "Module Name")]
        public string   Name        { get; set; }
        public string   Description { get; set; }
        public string   ViewTitle   { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yy-mm-dd}")]
        [Display(Name= "Start Date")]
        public DateTime StartDate   { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: yy-mm-dd}")]
        [Display(Name = "End Date")]
        public DateTime EndDate     { get; set; }

        [Display(Name= "End Time")]
        public DateTime EndTime     { get; set; }

        


        public virtual List<Activity> Activities { get; set; }
        public virtual List<Document> Documents  { get; set; }

        
        
        public List<Module>          Modules  { get; set; }
        public List<ApplicationUser> Students { get; set; }
    }
}