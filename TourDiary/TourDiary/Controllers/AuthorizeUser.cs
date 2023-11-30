using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using TourDiary.Models;
using System.Web.Routing;
using System.Configuration;
using System.Collections.Generic;

namespace TourDiary.Controllers
{
    public class AuthorizeUser: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                if(HttpContext.Current.Request.Cookies["userInfo"]["userName"] != String.Empty)
                {
                    return true;
                }
            }

            return false;
        }

        public User GetCurrentUser()
        {
            User user = new User();
            if(HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                user.UserEmail = HttpContext.Current.Request.Cookies["userInfo"]["email"];
                user.UserName = HttpContext.Current.Request.Cookies["userInfo"]["UserName"];
                user.FirstName = HttpContext.Current.Request.Cookies["userInfo"]["FirstName"];
                user.LastName = HttpContext.Current.Request.Cookies["userInfo"]["LastName"];
                user.FullName = HttpContext.Current.Request.Cookies["userInfo"]["FullName"];
                user.UserRole = HttpContext.Current.Request.Cookies["userInfo"]["UserRole"];
                user.DesignationName = HttpContext.Current.Request.Cookies["userInfo"]["DesignationName"];
            }
            else
            {
                user.UserEmail = String.Empty; 
                user.UserName = String.Empty;
                user.FirstName = String.Empty;
                user.LastName = String.Empty;
                user.FullName = String.Empty;
                user.UserRole = String.Empty;
                user.DesignationName = String.Empty;
            }

            return user;
        }

        public void SetCurrentUser(User user)
        {
            HttpCookie userInfo = new HttpCookie("userInfo");
            userInfo["UserName"] = user.UserName;
            userInfo["FirstName"] = user.FirstName;
            userInfo["LastName"] = user.LastName;
            userInfo["FullName"] = user.FullName;
            userInfo["email"] = user.UserEmail;
            userInfo["UserRole"] = user.UserRole;
            userInfo["DesignationName"] = user.DesignationName;
            userInfo.Expires.Add(new TimeSpan(0, 1, 0));
            HttpContext.Current.Response.Cookies.Add(userInfo);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if(HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                if(HttpContext.Current.Request.Cookies["userInfo"]["UserName"] == String.Empty)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new{
                            Controller = "Home",
                            action = "Login"
                        })
                    );
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            Controller = "Home",
                            action = "Login"
                        })
                    );
            }
        }
    }
}