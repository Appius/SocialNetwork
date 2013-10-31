using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Web.Auth;

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        //
        // GET: /admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
