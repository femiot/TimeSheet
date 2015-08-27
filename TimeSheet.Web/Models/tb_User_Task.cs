using System;
using System.Collections.Generic;

namespace TimeSheet.Web.Models
{
    public partial class tb_User_Task
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public System.DateTime TaskDate { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
