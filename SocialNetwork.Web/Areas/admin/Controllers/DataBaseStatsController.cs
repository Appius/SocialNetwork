using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class DataBaseStatsController : BaseController
    {
        //
        // GET: /admin/DataBase/

        public ActionResult Index()
        {
            return View();
        }
    }
}
