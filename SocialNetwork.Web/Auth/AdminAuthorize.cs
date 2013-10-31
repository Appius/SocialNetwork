#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Auth
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            
            if (!filterContext.HttpContext.Request.IsAuthenticated || !filterContext.HttpContext.User.IsInRole("admin"))
            {
                filterContext.Result = new RedirectResult("~/home/error");
            }
        }
    }
}