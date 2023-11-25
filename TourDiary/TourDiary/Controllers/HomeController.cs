using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using TourDiary.Models;
using TourDiary.Helper;
using System.Configuration;
using System.Collections.Generic;

namespace TourDiary.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection Form)
        {
            string username = Convert.ToString(Form["username"]);
            string password = Convert.ToString(Form["password"]);

            DAUtils utils = new DAUtils();
            User user = utils.GetUserDetailsByUsername(username);

            if(user.UserPassword == password)
            {
                AuthorizeUser authUser = new AuthorizeUser();
                authUser.SetCurrentUser(user);
                ViewBag.Error = "Login Successful";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Error";
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            AuthorizeUser authUser = new AuthorizeUser();
            User user = new User
            {
                UserName = String.Empty,
                UserEmail = String.Empty,
                UserRole = String.Empty,
                UserPassword = String.Empty,
            };

            authUser.SetCurrentUser(user);

            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public String UpdatePassword(String UserEmail, String NewPassword)
        {
            String auth = Request.Headers.Get("authorization");
            String originalAuth = ConfigurationManager.AppSettings["AuthToken"].ToString();
            if(auth != originalAuth)
            {
                return "Error..";
            }
            DAUtils utils = new DAUtils();
            utils.UpdatePassword(UserEmail, NewPassword);
            return "Updated";
        }

        public ActionResult Unauthorize()
        {
            return View();
        }
    }
}