#region

using System;
using System.Linq;
using System.Web.Mvc;
using MvcPaging;
using SocialNetwork.Core.Models;
using SocialNetwork.Web.Mappers;
using SocialNetwork.Web.ViewModels;

#endregion

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    public class UsersController : BaseController
    {
        /// <summary>
        /// Главная страница с отображением списка пользователей
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="act">Категория пользователей (все, активные, заблокированные)</param>
        public ActionResult Index(int? page, string act = "")
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            var mapper = DependencyResolver.Current.GetService<IMapper>();
            var users = UserRepository.GetAll();

            switch (act)
            {
                case "blocked":
                    users = users.Where(item => item.IsBlocked);
                    break;
                case "active":
                    users = users.Where(item => !item.IsBlocked);
                    break;
            }
            
            var usersViewModels = users.Select(
                item => (UserViewModel) mapper.Map(item, typeof (User), typeof (UserViewModel)))
                .ToPagedList(currentPageIndex, DefaultPageSize);

            ViewBag.CountAllUsers = UserRepository.GetAll().Count();
            ViewBag.CountActiveUsers = UserRepository.GetAll().Count(item=>!item.IsBlocked);
            ViewBag.CountBlockedUsers = UserRepository.GetAll().Count(item => item.IsBlocked);

            return View(usersViewModels);
        }

        /// <summary>
        /// Заблокировать/разблокировать пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        public ActionResult Block(int? id)
        {
            if (id != null && CurrentUser.Id != id) UserRepository.Block((int) id);
            if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
            return RedirectToAction("Index", "Users");
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        public ActionResult Delete(int? id)
        {
            if (id != null && CurrentUser.Id != id) UserRepository.Delete((int)id);
            if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
            return RedirectToAction("Index", "Users");
        }
    }
}