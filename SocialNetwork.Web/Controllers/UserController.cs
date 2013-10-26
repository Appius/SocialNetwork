#region

using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using SocialNetwork.Core.Helpers;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Главная страница просмотра информации о пользователе
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя (0 - твоя страница)</param>
        public ActionResult Index(int id = 0)
        {
            if (id == 0) id = CurrentUser.Id;
            var user = UserRepository.Get(id);

            var photoName = user.Id.ToString(CultureInfo.InvariantCulture).ComputeStringHash();
            var photoPath = Path.Combine(Server.MapPath("~/Pics"), photoName);

            if (System.IO.File.Exists(photoPath))
            {
                ViewBag.PhotoUrl = Url.Content(Path.Combine(Url.Content("~/Pics/"), photoName));
            }
            return View(user);
        }
    }
}