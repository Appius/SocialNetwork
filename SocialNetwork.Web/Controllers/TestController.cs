using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Core.Models.Repos;

namespace SocialNetwork.Web.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            var roleRepository = new RoleRepository();
            var roles = roleRepository.GetAll();

            return View(roles);
        }

    }
}
