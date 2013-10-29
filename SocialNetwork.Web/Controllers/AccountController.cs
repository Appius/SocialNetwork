#region

using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper.Internal;
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
        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        ///     Форма входа
        /// </summary>
        [HttpGet]
        public ActionResult LogOn()
        {
            if (CurrentUser != null) RedirectToAction("Index", "Account");

            return View();
        }

        /// <summary>
        ///     Процедура аутентификации пользователей
        /// </summary>
        /// <param name="logOnViewModel">Модель входа</param>
        /// <param name="returnUrl">Куда идти</param>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (CurrentUser != null) return RedirectToAction("Index", "Account");

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
        public ActionResult LogOff()
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
            if (CurrentUser != null) return RedirectToAction("Index", "Account");
            return View();
        }

        /// <summary>
        ///     Форма регистрации
        /// </summary>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (CurrentUser != null) return RedirectToAction("Index", "Account");

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

        /// <summary>
        /// Страница изменения настроек
        /// </summary>
        /// <param name="act">Категория настроек</param>
        [Authorize]
        [HttpGet]
        public ActionResult Edit(string act = "")
        {
            ViewBag.Act = act;

            var mapper = DependencyResolver.Current.GetService<IMapper>();
            switch (act)
            {
                case "changepass":
                {
                    return View(new ChangePasswordViewModel());
                }
                case "advanced":
                {
                    return View(mapper.Map(CurrentUser, typeof(User), typeof(AdvancedInfoViewModel)));
                }
                default:
                {
                    var user = (GeneralInfoViewModel)mapper.Map(CurrentUser, typeof (User), typeof (GeneralInfoViewModel));
                    var photoName = CurrentUser.Id.ToString(CultureInfo.InvariantCulture).ComputeStringHash();
                    var photoPath = Path.Combine(Server.MapPath("~/Pics"), photoName);

                    if (System.IO.File.Exists(photoPath))
                    {
                        user.AvatarUrl = Url.Content(Path.Combine(Url.Content("~/Pics/"), photoName));
                    }
                    return View(user);
                }
            }
        }

        /// <summary>
        /// Страница изменения настроек
        /// </summary>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(object model)
        {
            return View(model);
        }

        /// <summary>
        /// Сохранение основных данных
        /// </summary>
        /// <param name="model">Моделька</param>
        /// <param name="photo">Фотография пользователя</param>
        [Authorize]
        [HttpPost]
        public ActionResult SaveGeneralInfo(GeneralInfoViewModel model, HttpPostedFileBase photo)
        {
            if (photo != null && photo.ContentLength > 0)
            {
                var fileName = CurrentUser.Id.ToString(CultureInfo.InvariantCulture).ComputeStringHash();
                var path = Path.Combine(Server.MapPath("~/Pics"), fileName);
                photo.SaveAs(path);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = CurrentUser;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.MiddleName = model.MiddleName;
                    user.Mobile = model.Mobile;
                    user.Skype = model.Skype;
                    user.Email = model.Email;
                    user.Website = model.Website;
                    user.CurrentCity = model.CurrentCity;

                    UserRepository.Update(user);

                    if (CurrentUser.Email != user.Email)
                        LogOff();
                    
                    return RedirectToAction("Edit", "Account", new { success = 1 });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            return View("Edit", model);
        }

        /// <summary>
        /// Сохранение второстепенных данных
        /// </summary>
        /// <param name="model">Моделька</param>
        [Authorize]
        [HttpPost]
        public ActionResult SaveAdvancedInfo(AdvancedInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = CurrentUser;
                    user.Activies = model.Activies;
                    user.AboutMe = model.AboutMe;
                    user.FavoriteBooks = model.FavoriteBooks;
                    user.FavoriteGames = model.FavoriteGames;
                    user.FavoriteMovies = model.FavoriteMovies;
                    user.FavoriteMusic = model.FavoriteMusic;
                    user.FavoriteQuotes = model.FavoriteQuotes;
                    user.Interests = model.Interests;
                
                    UserRepository.Update(user);

                    return RedirectToAction("Edit", "Account", new {act = "advanced", success = 1});
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            ViewBag.Act = "advanced";
            return View("Edit", model);
        }

        /// <summary>
        /// Сохранение второстепенных данных
        /// </summary>
        /// <param name="model">Моделька</param>
        [Authorize]
        [HttpPost]
        public ActionResult SaveNewPassInfo(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CurrentUser.Password == model.OldPassword.ComputeStringHash())
                    {
                        CurrentUser.Password = model.NewPassword.ComputeStringHash();
                        UserRepository.Update(CurrentUser);

                        return RedirectToAction("Edit", "Account", new { act = "changepass", success = 1 });
                    }
                    else
                        ModelState.AddModelError("OldPassword", Resources.OldPasswordNotCorrect);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            ViewBag.Act = "changepass";
            return View("Edit", model);
        }
    }
}