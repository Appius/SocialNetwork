#region

using System;
using SocialNetwork.Core.Models;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class MessageViewModel
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

        /// <summary>
        ///     Пользователь, который отправляет сообщение
        /// </summary>
        public virtual User FromUser { get; set; }

        /// <summary>
        ///     Пользователь, который принимает сообщение
        /// </summary>
        public virtual User ToUser { get; set; }

        /// <summary>
        ///     Фото отправителя / получателя
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}