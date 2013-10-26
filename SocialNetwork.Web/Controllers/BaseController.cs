#region

using System;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Web.Auth;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(IAuthentication auth, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            Auth = auth;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }
        
        protected BaseController()
            : this(
                DependencyResolver.Current.GetService<IAuthentication>(),
                DependencyResolver.Current.GetService<IUserRepository>(),
                DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        /// <summary>
        ///     Объект авторизации
        /// </summary>
        public IAuthentication Auth { get; set; }

        /// <summary>
        ///     Репозиторий пользователей
        /// </summary>
        public IUserRepository UserRepository { get; set; }

        /// <summary>
        ///     Репозиторий ролей
        /// </summary>
        public IRoleRepository RoleRepository { get; set; }

        /// <summary>
        ///     Текущий пользователь
        /// </summary>
        public User CurrentUser
        {
            get { return ((IUserProvider) Auth.CurrentUser.Identity).User; }
        }
    }
}