#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /admin/Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}