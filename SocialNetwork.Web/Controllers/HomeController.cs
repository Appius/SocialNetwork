#region

using System;
using System.Web.Mvc;
using SocialNetwork.Web.Auth;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}