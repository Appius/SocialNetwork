#region

using System;
using System.Web.Mvc;
using SocialNetwork.Core.Models.Repos;

#endregion

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