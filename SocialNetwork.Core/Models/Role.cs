#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс, который предоставляет список ролей
    /// </summary>
    public sealed class Role
    {
        /// <summary>
        ///     Конструктор по-умолчяанию (создает пустой список пользователей)
        /// </summary>
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя роли
        /// </summary>
        [Required]
        [Display(Name = "Имя роли*")]
        public string RoleName { get; set; }

        /// <summary>
        ///     Описание роли
        /// </summary>
        [Display(Name = "Описание роли")]
        public string Description { get; set; }

        /// <summary>
        ///     Коллекция ролей-пользователей
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}