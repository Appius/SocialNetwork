#region

using System;
using System.Web.Mvc;

#endregion

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