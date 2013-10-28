#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IMessageRepository
    {
        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        /// <param name="userWhoSented">Пользователь, который посылает</param>
        /// <param name="userWhoReceive">Пользователь, который принимает</param>
        /// <param name="msgText">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        void SentMessage(User userWhoSented, User userWhoReceive, string msgText, string title = "");

        /// <summary>
        ///     Отправка сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        void SentMessage(Message message);

        /// <summary>
        ///     Получения списка сообщений адресованых определенному пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        IEnumerable<Message> GetInbox(User user);

        /// <summary>
        ///     Получения списка сообщений принадлежащих определенному пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        IEnumerable<Message> GetOutbox(User user);

        /// <summary>
        ///     Получения письма по его уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        Message Get(int id);

        /// <summary>
        ///     Обновление сообщения
        /// </summary>
        /// <param name="msg">Сообщение</param>
        void Update(Message msg);

        /// <summary>
        ///     Удаление сообщения
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        void Delete(int id);

        /// <summary>
        ///     Удаление сообщения
        /// </summary>
        /// <param name="msg">Сообщение</param>
        void Delete(Message msg);
    }
}