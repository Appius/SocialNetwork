using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /admin/Users/

        public ActionResult Index()
        {
            return View();
        }

    }
}
