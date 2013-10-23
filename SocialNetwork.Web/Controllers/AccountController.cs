#region

using System;
using System.Web.Mvc;
using System.Web.Security;
using Elmah.ContentSyndication;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Web.App_GlobalResources;
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
                var user = Auth.Login(logOnViewModel.Email, logOnViewModel.Password, logOnViewModel.RememberMe);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState["Password"].Errors.Add(Resources.ErrorEnterence);
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

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var userRepository = DependencyResolver.Current.GetService<IUserRepository>();
            var user = userRepository.Get(model.Email);
            if (user!=null)
                ModelState.AddModelError("Email", Resources.ExistEmail);

            
            if (ModelState.IsValid)
            {
                
            }
            return View(model);
        }
    }
}