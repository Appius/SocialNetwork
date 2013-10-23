#region

using System;
using System.Web.Mvc;
using SocialNetwork.Web.ViewModels;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Форма входа
        /// </summary>
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Продедура аутентификации пользователей
        /// </summary>
        /// <param name="logOnViewModel">Модель входа</param>
        /// <param name="returnUrl">Куда идти</param>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = Auth.Login(logOnViewModel.UserName, logOnViewModel.Password, logOnViewModel.RememberMe);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState["Password"].Errors.Add("Ошибка входа");
            }
            return View(logOnViewModel);
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Форма регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

/*        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.UserName, "user");
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie #1#);
                    Session["id"] = null;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }
            return View(model);
        }*/
    }
}