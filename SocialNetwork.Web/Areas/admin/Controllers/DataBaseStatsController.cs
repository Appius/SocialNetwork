using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class DataBaseStatsController : BaseController
    {
        //
        // GET: /admin/DataBase/
        public ActionResult Index()
        {
            var databaseStatsRepository = DependencyResolver.Current.GetService<IDatabaseStatsRepository>();
            DbUsage dbUsage = databaseStatsRepository.GetStats();
            return View(dbUsage);
        }
    }
}
