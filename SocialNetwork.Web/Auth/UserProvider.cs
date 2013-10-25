#region

using System;
using System.Security.Principal;
using System.Web.Mvc;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Core.Models.Repos;

#endregion

namespace SocialNetwork.Web.Auth
{
    /// <summary>
    ///     Реализация интерфейса Principal
    /// </summary>
    public class UserProvider : IPrincipal
    {
        /// <summary>
        ///     Репозиторий ролей
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        #region IPrincipal Members

        /// <summary>
        ///     Идентификатор пользователя
        /// </summary>
        public IIdentity Identity
        {
            get { return UserIdentity; }
        }

        /// <summary>   
        ///     Находится в данной роли или нет
        /// </summary>
        /// <param name="role">Имя роли</param>
        public bool IsInRole(string role)
        {
            if (UserIdentity.User == null)
            {
                return false;
            }
            var userRoleRepository = DependencyResolver.Current.GetService<IUserRoleRepository>();
            var roleRoleRepository = DependencyResolver.Current.GetService<IRoleRepository>();

            return
                userRoleRepository.Contains(UserIdentity.User, roleRoleRepository.Get(role));
        }

        #endregion

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="repository">Репозитория пользователей</param>
        /// <param name="roleRepository">Репозиторий ролей</param>
        public UserProvider(string name, IUserRepository repository, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            UserIdentity = new UserIndentity();
            UserIdentity.Init(name, repository);
        }

        /// <summary>
        ///     Обьект UserIndentity
        /// </summary>
        private UserIndentity UserIdentity { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserIdentity.Name;
        }
    }
}