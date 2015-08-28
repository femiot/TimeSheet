using System;
using System.Collections.Generic;

namespace TimeSheet.Web.Models
{
    public partial class tb_User_Task
    {
        public tb_User_Task()
        {
            this.tb_Task = new List<tb_Task>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public System.DateTime TaskDate { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<tb_Task> tb_Task { get; set; }
    }
}
