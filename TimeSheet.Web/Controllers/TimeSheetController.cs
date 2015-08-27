using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Web.Models;

namespace TimeSheet.Web.Controllers
{
    [Authorize]
    public class TimeSheetController : Controller
    {
        private readonly TimeSheetContext _db;

        public TimeSheetController()
        {
            _db = new TimeSheetContext();
        }

        // GET: TimeSheet
        public ActionResult Index()
        {
            var query = (from task in _db.tb_Task
                        join userTask in _db.tb_User_Task
                        on task.Id equals userTask.TaskId
                        select new TaskModel { Client = task.ClientName, Description = task.Description, HoursSpent = task.Hours, Type = task.Type }).ToList();
            return View(query);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            return View();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            return View();
        }

    }
}