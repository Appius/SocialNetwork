#region

using System;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.Web.Auth;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(IAuthentication auth)
        {
            Auth = auth;
        }

        protected BaseController()
            : this(DependencyResolver.Current.GetService<IAuthentication>())
        {
        }

        /// <summary>
        ///     Объект авторизации
        /// </summary>
        public IAuthentication Auth { get; set; }

        /// <summary>
        ///     Текущий пользователь
        /// </summary>
        public User CurrentUser
        {
            get { return ((IUserProvider) Auth.CurrentUser.Identity).User; }
        }
    }
}