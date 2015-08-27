using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Web.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        [Display(Name = "Hours spent")]
        public string HoursSpent { get; set; }
        [Display(Name = "Task Category")]
        public string Type { get; set; }
    }
}