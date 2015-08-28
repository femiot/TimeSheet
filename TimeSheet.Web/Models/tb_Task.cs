using System;
using System.Collections.Generic;

namespace TimeSheet.Web.Models
{
    public partial class tb_Task
    {
        public int Id { get; set; }
        public int User_TaskId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Type { get; set; }
        public virtual tb_User_Task tb_User_Task { get; set; }
    }
}
