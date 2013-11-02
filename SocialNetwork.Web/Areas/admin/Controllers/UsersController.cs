#region

using System;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}