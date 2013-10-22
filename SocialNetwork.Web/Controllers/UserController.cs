using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index(int id = 0)
        {
            return View();
        }

    }
}
