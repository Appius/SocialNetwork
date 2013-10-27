#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class OverviewController : BaseController
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
    }
}