﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Web.Auth;

namespace SocialNetwork.Web.Areas.admin.Controllers
{
    [AdminAuthorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///     Дефолтное количество пользователей на странице
        /// </summary>
        protected const int DefaultPageSize = 9;

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
            get { return ((IUserProvider)Auth.CurrentUser.Identity).User; }
        }
    }
}
