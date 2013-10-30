#region

using System;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class FriendShipRequestViewModel
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
        ///     Фото
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Дата подачи запроса
        /// </summary>
        public DateTime RequestDate { get; set; }

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
    }
}