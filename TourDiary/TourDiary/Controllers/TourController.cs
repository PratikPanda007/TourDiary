using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using TourDiary.Models;
using TourDiary.Helper;
using TourDiary.Controllers;
using System.Collections.Generic;

namespace TourDiary.Controllers
{
    [AuthorizeUser]
    public class TourController : Controller
    {
        // GET: Tour
        public ActionResult TourManagement()
        {
            DAUtils utils = new DAUtils();
            AuthorizeUser authUser = new AuthorizeUser();
            User user = authUser.GetCurrentUser();

            ViewBag.UserName = user.UserName;
            return View();
        }

        public ActionResult AddTour()
        {
            DAUtils utils = new DAUtils();
            AuthorizeUser authUser = new AuthorizeUser();
            User user = authUser.GetCurrentUser();
            ViewData["UserDetails"] = user;

            return View();
        }

        public ActionResult AllTours()
        {
            DAUtils utils = new DAUtils();
            AuthorizeUser authUser = new AuthorizeUser();
            User user = authUser.GetCurrentUser();
            ViewData["UserDetails"] = user;

            return View();
        }

        [HttpPost]
        public ActionResult AddTour(FormCollection Form)
        {
            TourData td = new TourData
            {
                UserName = Convert.ToString(Form["userName"]),
                Designation = Convert.ToString(Form["designation"]),
                HeadQuarters = Convert.ToString(Form["headQuarters"]),
                ActualPay = Convert.ToString(Form["actualPay"]),
                Dep_Station = Convert.ToString(Form["dep_station"]),
                Dep_Date = Convert.ToString(Form["dep_date"]),
                Dep_Hour = Convert.ToString(Form["dep_hour"]),
                Arv_Station = Convert.ToString(Form["arv_station"]),
                Arv_Date = Convert.ToString(Form["arv_date"]),
                Arv_Hour = Convert.ToString(Form["arv_hour"]),
                JourneyPurpose = Convert.ToString(Form["journeyPurose"]),
                JourneyKind = Convert.ToString(Form["jounreyKind"]),
                NoOfMiles = Convert.ToString(Form["road__miles"]),
                RoadRate = Convert.ToString(Form["road__rate"]),
                RoadAmount = Convert.ToString(Form["road_amount"]),
                NoOfDays = Convert.ToString(Form["noOfDays"]),
                AllowanceRate = Convert.ToString(Form["allowance__rate"]),
                AllowanceAmount = Convert.ToString(Form["allowance_amount"]),
                Particulars = Convert.ToString(Form["expense_Particulars"]),
                ExpenseAmount = Convert.ToString(Form["expense_amount"]),
            };

            DAUtils utils = new DAUtils();
            //utils.AddTour(td);
            return View();
        }
    }
}