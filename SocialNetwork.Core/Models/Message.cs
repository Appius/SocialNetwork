#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс для хранения сообщений между пользователями
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Заголовок сообщения (необязательно)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Текст сообщения
        /// </summary>
        public string MsgText { get; set; }

        /// <summary>
        ///     Время посылки сообщения
        /// </summary>
        public DateTime PostedDate { get; set; }

        /// <summary>
        ///     Прочитано сообщение или нет
        /// </summary>
        public bool IsRead { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     Пользователь, который отправляет сообщение
        /// </summary>
        public virtual User FromUser { get; set; }

        /// <summary>
        ///     Пользователь, который принимает сообщение
        /// </summary>
        public virtual User ToUser { get; set; }
    }
}