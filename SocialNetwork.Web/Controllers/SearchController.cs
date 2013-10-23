#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        public ActionResult Index(string str = "search")
        {
            return View();
        }
    }
}