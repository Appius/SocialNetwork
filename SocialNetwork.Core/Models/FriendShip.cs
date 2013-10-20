#region

using System;

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
        ///     Идентификатор пользователя, который послал запрос
        /// </summary>
        public int User1Id { get; set; }

        /// <summary>
        ///     Идентификатор пользователя, который принимет запрос
        /// </summary>
        public int User2Id { get; set; }

        /// <summary>
        ///     Дата сделанного запроса
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        ///     Пользователь, который сделал запрос на дружбу
        /// </summary>
        public virtual User UserWhoRequest { get; set; }

        /// <summary>
        ///     Пользователь, который принимает запрос на дружбу
        /// </summary>
        public virtual User UserWhoReceive { get; set; }
    }
}