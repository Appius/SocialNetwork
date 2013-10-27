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
        public ActionResult Index(string srchTerm, string currCity, bool? sex = null)
        {
            return View();
        }
    }
}