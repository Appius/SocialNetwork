#region

using System;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс для устанавления связи какой пользователь каким ролям принадлежит
    /// </summary>
    public class UserRole
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     Идентификатор пользователя
        /// </summary>
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