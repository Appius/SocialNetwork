#region

using System;
using System.Security.Principal;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Web.Auth
{
    /// <summary>
    ///     Реализация интерфейса для идентификации пользователя
    /// </summary>
    public class UserIndentity : IIdentity, IUserProvider
    {
        /// <summary>
        ///     Тип класса для пользователя
        /// </summary>
        public string AuthenticationType
        {
            get { return typeof (User).ToString(); }
        }

        /// <summary>
        ///     Авторизован или нет
        /// </summary>
        public bool IsAuthenticated
        {
            get { return User != null; }
        }

        /// <summary>
        ///     Имя пользователя (уникальное) (email)
        /// </summary>
        public string Name
        {
            get { return User != null ? User.Email : "anonym"; }
        }

        /// <summary>
        ///     Текущий пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Инициализация по имени
        /// </summary>
        /// <param name="email">E-mail пользователя</param>
        /// <param name="repository">Репозиторий пользателей</param>
        public void Init(string email, IUserRepository repository)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User = repository.Get(email);
            }
        }
    }
}