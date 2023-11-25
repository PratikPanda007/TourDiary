using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Configuration;
using System.Collections.Generic;

namespace TourDiary.Controllers
{
    public class AuthorizeAdmin:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string AdminAccess = WebConfigurationManager.AppSettings["AdminAccess"];
            string[] AdminUserList = AdminAccess.Split(',');

            String localUser = WebConfigurationManager.AppSettings["LocalUser"];

            try
            {
                AuthorizeUser authUser = new AuthorizeUser();
                localUser = localUser.Contains("none") ? localUser.Replace("none", authUser.GetCurrentUser().UserEmail) : localUser;
            }catch(Exception ex) { }

            String currentUser = HttpContext.Current.User.Identity.Name == "" ? localUser : HttpContext.Current.User.Identity.Name;
            currentUser = currentUser.ToLower();

            if (AdminUserList.Contains(currentUser))
                return true;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            Controller = "Home",
                            action = "Unauthorize"
                        })
                    );
        }

        public bool isAdmin()
        {
            string AdminAccess = WebConfigurationManager.AppSettings["AdminAccess"];
            string[] AdminUserList = AdminAccess.Split(',');

            String localUser = WebConfigurationManager.AppSettings["LocalUser"];

            try
            {
                AuthorizeUser authUser = new AuthorizeUser();
                localUser = localUser.Contains("none") ? localUser.Replace("none", authUser.GetCurrentUser().UserEmail) : localUser;
            }
            catch (Exception ex) { }

            String currentUser = HttpContext.Current.User.Identity.Name == "" ? localUser : HttpContext.Current.User.Identity.Name;
            currentUser = currentUser.ToLower();

            if (AdminUserList.Contains(currentUser))
                return true;
            return false;
        }
    }
}