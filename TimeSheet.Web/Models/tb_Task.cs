using System;
using System.Collections.Generic;

namespace TimeSheet.Web.Models
{
    public partial class tb_Task
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Type { get; set; }
    }
}
