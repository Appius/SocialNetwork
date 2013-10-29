#region

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс, который связывает пользователей "узами дружбы"
    /// </summary>
    public class FriendShip
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Дата сделанного запроса
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        ///     Сообщение при посылки запроса
        /// </summary>
        public string Message { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     Пользователь, который сделал запрос на дружбу
        /// </summary>
        [ForeignKey("User1Id")]
        public virtual User FromUser { get; set; }

        /// <summary>
        ///     Пользователь, который принимает запрос на дружбу
        /// </summary>
        [ForeignKey("User2Id")]
        public virtual User ToUser { get; set; }
    }
}