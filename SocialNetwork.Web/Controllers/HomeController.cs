#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}