using LexiconLMS.Models;
using System.Collections.Generic;
namespace LexiconLMS.ViewModels
{
    public class ModuleManageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Activity> Activities { get; set; }
    }
}