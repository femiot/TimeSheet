using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Globalization;

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
            var model = (from task in _db.tb_Task
                        join userTask in _db.tb_User_Task
                        on task.User_TaskId equals userTask.Id
                        orderby task.Id descending
                        select 
                        new TaskModel { 
                            Client = task.ClientName, 
                            Description = task.Description, 
                            HoursSpent = task.Hours, 
                            Type = task.Type, 
                            TaskDate = userTask.TaskDate 
                        }).ToList();

            model = model.Count > 0 ? model.Where(x => x.TaskDate == DateTime.Now.Date).ToList() : model;

            if (model.Count > 0)
            {
                List<int> hours = new List<int>();
                List<int> minutes = new List<int>();
                model.ForEach(x => SplitTime(ref hours, ref minutes, x.HoursSpent));

               if(hours.Count > 0 && minutes.Count > 0)
               {
                   var hoursFromMinute = minutes.Sum() / 60;
                   var minutesRemainder = minutes.Sum() % 60;
                   ViewBag.TotalHoursSpent = String.Format("Total Time spent today: {0} hr(s) {1} minute(s)", hours.Sum() + hoursFromMinute, minutesRemainder); 
               }
            }


            TempData["TaskDate"] = model.Count > 0 ? model.FirstOrDefault().TaskDate : D(DateTime.UtcNow);
            ViewBag.TaskDate = model.Count > 0 ? model.FirstOrDefault().TaskDate.ToString("dd MMM yyyy") : D(DateTime.UtcNow).ToString("dd MMM yyyy");
            return View(model);
        }

        // GET: TimeSheet
        [HttpPost]
        public ActionResult Search(string search)
        {
            bool isDate = false;
            DateTime searchDateTime;
            isDate = DateTime.TryParse(search, out searchDateTime);
            if (isDate)
            {
                var model = (from task in _db.tb_Task
                             join userTask in _db.tb_User_Task
                             on task.User_TaskId equals userTask.Id
                             orderby task.Id descending
                             select
                             new TaskModel
                             {
                                 Client = task.ClientName,
                                 Description = task.Description,
                                 HoursSpent = task.Hours,
                                 Type = task.Type,
                                 TaskDate = userTask.TaskDate
                             }).ToList();

                model = model.Count > 0 ? model.Where(x => x.TaskDate == searchDateTime.Date).ToList() : model;

                if (model.Count > 0)
                {
                    List<int> hours = new List<int>();
                    List<int> minutes = new List<int>();
                    model.ForEach(x => SplitTime(ref hours, ref minutes, x.HoursSpent));

                    if (hours.Count > 0 && minutes.Count > 0)
                    {
                        var hoursFromMinute = minutes.Sum() / 60;
                        var minutesRemainder = minutes.Sum() % 60;
                        ViewBag.TotalHoursSpent = String.Format("Total Time spent today: {0} hr(s) {1} minute(s)", hours.Sum() + hoursFromMinute, minutesRemainder);
                    }
                }

                TempData["TaskDate"] = model.Count > 0 ? model.FirstOrDefault().TaskDate : D(DateTime.UtcNow);
                ViewBag.TaskDate = model.Count > 0 ? model.FirstOrDefault().TaskDate.ToString("dd MMM yyyy") : D(DateTime.UtcNow).ToString("dd MMM yyyy");

                return View("Index", model);
            }

            return RedirectToAction("Index");
        }


        public void SplitTime(ref List<int> hours, ref List<int> minutes, string hoursSpent)
        {
            if(!string.IsNullOrEmpty(hoursSpent))
            {
                var TimeArray = hoursSpent.Split(':');
                if(TimeArray.Length > 1)
                {
                    var hour = Convert.ToInt32(TimeArray[0].TrimStart('0'));
                    var minute = Convert.ToInt32(TimeArray[1].TrimStart('0'));

                    hours.Add(hour);
                    minutes.Add(minute);
                }
            }
        }

        public ActionResult Create()
        {
            TaskModel model = new TaskModel();
            if (TempData["TaskDate"] != null)
                model.TaskDate = (DateTime)TempData["TaskDate"];
            else
                return RedirectToAction("Index");

            ViewBag.TaskDate = model.TaskDate.ToString("dd MMM yyyy");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskModel model)
        {

            Action<int> executeTask = (int userTaskId) =>
                {
                    tb_Task task = new tb_Task
                    {
                        User_TaskId = userTaskId,
                        ClientName = model.Client,
                        Description = model.Description,
                        Hours = model.HoursSpent,
                        Type = model.Type
                    };

                    _db.tb_Task.Add(task);

                    _db.SaveChanges();
                };

            // get the user id and create the UserTask record
           var userId = User.Identity.GetUserId();

           // check if task date for user has been created for the day
           var userTask = _db.tb_User_Task.Where(x => x.UserId == userId).ToList()
               .FirstOrDefault(x => x.TaskDate.Date == model.TaskDate.Date);

            if(userTask == null)
            {
                var userTaskObject = new tb_User_Task { UserId = userId, TaskDate = D(model.TaskDate).Date };
                _db.tb_User_Task.Add(userTaskObject);

                _db.SaveChanges();

                executeTask(userTaskObject.Id);
            }
            else
            {
                executeTask(userTask.Id);
            }


            return RedirectToAction("Index");
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

        public DateTime D(DateTime dateTime)
        {
           return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
           dateTime, "South Africa Standard Time");
        }

    }
}