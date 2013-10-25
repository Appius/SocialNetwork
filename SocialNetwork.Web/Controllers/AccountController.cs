#region

using System;
using System.Web.Mvc;
using SocialNetwork.Core.Helpers;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Core.Models.Repos;
using SocialNetwork.Web.App_GlobalResources;
using SocialNetwork.Web.Mappers;
using SocialNetwork.Web.ViewModels;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : BaseController
    {

        #region Private methods

        /// <summary>
        /// Добавление к пользователю определенной роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="roleName">Имя роли</param>
        private void AddUserToRole(User user, string roleName)
        {
            var role = RoleRepository.Get(roleName);

            AddUserToRole(user, role);
        }

        /// <summary>
        /// Добавление к пользователю определенной роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        private void AddUserToRole(User user, Role role)
        {
            var userRoleRepository = DependencyResolver.Current.GetService<IUserRoleRepository>();
            userRoleRepository.Add(user, role);
        }

        #endregion
        
        // GET: /Account/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Форма входа
        /// </summary>
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        ///     Продедура аутентификации пользователей
        /// </summary>
        /// <param name="logOnViewModel">Модель входа</param>
        /// <param name="returnUrl">Куда идти</param>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var pass = logOnViewModel.Password.ComputeStringHash();
                var user = Auth.Login(logOnViewModel.Email, pass,
                    logOnViewModel.RememberMe);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", Resources.ErrorEnterence);
            }
            return View(logOnViewModel);
        }

        /// <summary>
        ///     Выход из системы
        /// </summary>
        public ActionResult Logout()
        {
            Auth.LogOff();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Форма регистрации
        /// </summary>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        ///     Форма регистрации
        /// </summary>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var user = UserRepository.Get(model.Email);
            if (user != null)
                ModelState.AddModelError("Email", Resources.ExistEmail);

            if (ModelState.IsValid)
            {
                var mapper = DependencyResolver.Current.GetService<IMapper>();

                try
                {
                    user = (User) mapper.Map(model, typeof (RegisterViewModel), typeof (User));

                    // сохраняем пароль для входа сразу после регистрации
                    var pass = user.Password;

                    user.Password = user.Password.ComputeStringHash();
                    UserRepository.Add(user);

                    // добавление роли пользователю
                    AddUserToRole(UserRepository.Get(user.Email), "user");
                    // вход
                    LogOn(new LogOnViewModel { Email = user.Email, Password = pass }, "/account/");

                    return RedirectToAction("Index", "Account");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            return View(model);
        }

        /// <summary>
        ///     Страница, которая говорит, что регистрация прошла успешно
        /// </summary>
        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}