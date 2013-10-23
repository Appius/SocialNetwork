#region

using System;
using System.Security.Principal;
using System.Web;
using SocialNetwork.Core.Models;

#endregion

namespace SocialNetwork.Web.Auth
{
    /// <summary>
    ///     Интерфейс для авторизации
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        ///     Текущий HttpContext
        /// </summary>
        HttpContext HttpContext { get; set; }

        /// <summary>
        ///     Данные о текущем пользователе
        /// </summary>
        IPrincipal CurrentUser { get; }

        /// <summary>
        ///     Процедура входа
        /// </summary>
        /// <param name="email">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="isPersistent">Постоянная авторизация или нет</param>
        User Login(string email, string password, bool isPersistent);

        /// <summary>
        ///     Выход из системы
        /// </summary>
        void LogOut();
    }
}