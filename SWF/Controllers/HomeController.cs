using SWF.Models;
using SWF.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Shift");
        }

        public ActionResult Shift()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region API
        public JsonResult GetSchedule(DateTime date)
        {
            var schedule = ScheduleManager.InitEngineersShift();
            ScheduleManager.Set2WeekSchedule(schedule, date);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }
        #endregion API


        #region private method

        #endregion
    }

}