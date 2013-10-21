#region

using System;
using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC;

#endregion

namespace SocialNetwork.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] {"SocialNetwork.Web.Controllers"}
                );
        }
    }
}