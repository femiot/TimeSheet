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

        [Display(Name = "Client's Name")]
        [Required(ErrorMessage="Please provide client's name")]
        [StringLength(250, ErrorMessage = "Maximum length is 250")]
        public string Client { get; set; }

        [Required(ErrorMessage = "Please provide description")]
        [StringLength(500, ErrorMessage = "Maximum length is 500")]
        public string Description { get; set; }

        [RegularExpression(@"\d{2}\:\d{2}", ErrorMessage="Invalid format for hours e.g. 02:30")]
        [Required(ErrorMessage = "Please provide hours spent")]
        [Display(Name = "Hours spent (Format: HH:MM)")]
        [StringLength(5, ErrorMessage = "Maximum length is 5")]
        public string HoursSpent { get; set; }

        [Display(Name = "Task Category")]
        [StringLength(50, ErrorMessage = "Maximum length is 50")]
        public string Type { get; set; }
        public DateTime TaskDate { get; set; }
    }
}