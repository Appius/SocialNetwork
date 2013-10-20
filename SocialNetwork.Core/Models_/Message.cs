using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Models
{
    /// <summary>
    /// Класс для хранения сообщений между пользователями
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок сообщения (необязательно)
        /// </summary>
        [Display(Name = "Заголовок сообщения")]
        public string Title { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [Required]
        [Display(Name = "Текст сообщения*")]
        public string MsgText { get; set; }

        /// <summary>
        /// Время посылки сообщения
        /// </summary>
        public DateTime PostedDate { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        /// Пользователь, который отправляет сообщение
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Пользователь, который принимает сообщение
        /// </summary>
        public virtual User User1 { get; set; }
    }
}
