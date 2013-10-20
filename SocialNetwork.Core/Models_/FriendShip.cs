using System;
using System.Collections.Generic;

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс, который связывает пользователей "узами дружбы"
    /// </summary>
    public partial class FriendShip
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Дата сделанного запроса
        /// </summary>
        public DateTime RequestDate { get; set; }


        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     Пользователь, который сделал запрос на дружбу
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     Пользователь, который принимает запрос на дружбу
        /// </summary>
        public virtual User User1 { get; set; }
    }
}
