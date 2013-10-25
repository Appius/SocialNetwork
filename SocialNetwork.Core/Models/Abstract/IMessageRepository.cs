#region

using System;

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
    }
}