#region

using System;
using System.Drawing;
using System.Web.Mvc;
using SocialNetwork.Core.Helpers;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.Controllers
{
    /// <summary>
    ///     Контроллер, который занимается отображением картинок из базы данных
    /// </summary>
    public class PictureController : BaseController
    {
        /// <summary>
        ///     Ссылка на картинку пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public ActionResult GetPhoto(int id)
        {
            var user = UserRepository.Get(id);
            if (user == null || user.Avatar == null)
            {
                Image image = Image.FromFile(Server.MapPath("~/Resources/NoPhoto.bmp"));
                return File(image.ToByteArray(), "image/jpeg");
            }
            return File(user.Avatar, "image/jpeg");
        }
    }
}