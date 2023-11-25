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
                user.UserRole = HttpContext.Current.Request.Cookies["userInfo"]["UserRole"];
            }
            else
            {
                user.UserEmail = String.Empty; 
                user.UserName = String.Empty;
                user.UserRole = String.Empty;
            }

            return user;
        }

        public void SetCurrentUser(User user)
        {
            HttpCookie userInfo = new HttpCookie("userInfo");
            userInfo["UserName"] = user.UserName;
            userInfo["email"] = user.UserEmail;
            userInfo["UserRole"] = user.UserRole;
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