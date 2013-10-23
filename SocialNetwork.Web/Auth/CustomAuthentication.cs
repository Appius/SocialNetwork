#region

using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Web.Auth
{
    public class CustomAuthentication : IAuthentication
    {
        /// <summary>
        ///     Имя куков
        /// </summary>
        private const string CookieName = "__AUTH_COOKIE";

        /// <summary>
        ///     Репозиторий пользователей
        /// </summary>
        public IUserRepository UserRepository { get; set; }

        #region IAuthentication Members

        /// <summary>
        ///     Текущий пользователь
        /// </summary>
        private IPrincipal _currentUser;

        public CustomAuthentication(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        /// <summary>
        ///     Процедура входа
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="isPersistent">Постоянная авторизация или нет</param>
        /// <returns>Пользователь</returns>
        public User Login(string userName, string password, bool isPersistent)
        {
            User retUser = UserRepository.Login(userName, password);
            if (retUser != null)
            {
                CreateCookie(userName, isPersistent);
            }
            return retUser;
        }

        /// <summary>
        ///     Выход из системы
        /// </summary>
        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[CookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        /// <summary>
        ///     Текущий пользователь
        /// </summary>
        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;

                try
                {
                    HttpCookie authCookie = HttpContext.Request.Cookies.Get(CookieName);
                    if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                    {
                        var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        if (ticket != null)
                        {
                            var roleRepository = DependencyResolver.Current.GetService<IRoleRepository>();
                            _currentUser = new UserProvider(ticket.Name, UserRepository, roleRepository);
                        }
                    }
                    else
                        _currentUser = new UserProvider(null, null, null);
                }
                catch (Exception)
                {
//                        logger.Error("Failed authentication: " + ex.Message);
                    _currentUser = new UserProvider(null, null, null);
                }
                return _currentUser;
            }
        }

        /// <summary>
        ///     Создание куков
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="isPersistent">Постоянная авторизация или нет</param>
        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                string.Empty,
                FormsAuthentication.FormsCookiePath);

            // Шифруем ticket
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Создаем куки
            var authCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }

        #endregion

        /// <summary>
        ///     Текущий HttpContext
        /// </summary>
        public HttpContext HttpContext { get; set; }
    }
}