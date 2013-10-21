#region

using System;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    /// <summary>
    ///     Класс, который реализует систему отправки/приема сообщений
    /// </summary>
    public class MessageRepository : RepositoryBase, IMessageRepository
    {
        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        /// <param name="userWhoSented">Пользователь, который посылает</param>
        /// <param name="userWhoReceive">Пользователь, который принимает</param>
        /// <param name="msgText">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        public void SentMessage(User fromUser, User toUser, string msgText, string title = "")
        {
            SentMessage(new Message
            {
                FromUser = fromUser,
                ToUser = toUser,
                MsgText = msgText,
                Title = title,
                PostedDate = DateTime.Now
            });
        }

        /// <summary>
        ///     Отправка сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SentMessage(Message message)
        {
            Db.Messages.Add(message);
            Db.SaveChanges();
        }
    }
}