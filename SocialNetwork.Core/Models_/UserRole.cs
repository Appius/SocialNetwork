using System;
using System.Collections.Generic;

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс для устанавления связи какой пользователь каким ролям принадлежит
    /// </summary>
    public partial class UserRole
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }

        /// <summary>
        ///     Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        ///     Пользователь
        /// </summary>
        public virtual User User { get; set; }
    }
}
