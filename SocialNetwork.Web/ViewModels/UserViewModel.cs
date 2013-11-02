#region

using System;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     День рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Город
        /// </summary>
        public string CurrentCity { get; set; }

        /// <summary>
        ///     Фото
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Заблокирован ли пользователь
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}