#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    /// Класс для хранения сообщений между пользователями
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, коорый посылает сообщение
        /// </summary>
        public int User1Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, коорый принмает сообщение
        /// </summary>
        public int User2Id { get; set; }

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

        /// <summary>
        /// Пользователь, который отправляет сообщение
        /// </summary>
        [Required]
        public virtual User UserWhoSent { get; set; }

        /// <summary>
        /// Пользователь, который принимает сообщение
        /// </summary>
        [Required]
        public virtual User UserWhoReceive { get; set; }
    }
}