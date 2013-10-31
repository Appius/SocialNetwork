using System.Web.Mvc;
using LowercaseRoutesMVC;

namespace SocialNetwork.Web.Areas.admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRouteLowercase(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "SocialNetwork.Web.Areas.admin.Controllers" }
                );

/*            context.MapRouteLowercase(
                "Admin_elmah",
                "admin/elmah/{type}",
                new {action = "Index", controller = "Elmah", type = UrlParameter.Optional}
                );*/

        }
    }
}
