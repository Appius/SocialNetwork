#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        /// <param name="fromUser">Пользователь, который посылает</param>
        /// <param name="toUser">Пользователь, который принимает</param>
        /// <param name="msgText">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        public void SentMessage(User fromUser, User toUser, string msgText, string title = "")
        {
            SentMessage(new Message
            {
                User1Id = fromUser.Id,
                User2Id = toUser.Id,
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

        /// <summary>
        ///     Получения списка сообщений адресованых определенному пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        public IEnumerable<Message> GetInbox(User user)
        {
            return Db.Messages.Where(item => item.User2Id == user.Id);
        }

        /// <summary>
        ///     Получения списка сообщений принадлежащих определенному пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        public IEnumerable<Message> GetOutbox(User user)
        {
            return Db.Messages.Where(item => item.User1Id == user.Id);
        }

        /// <summary>
        ///     Получения письма по его уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        public Message Get(int id)
        {
            return Db.Messages.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        ///     Обновление сообщения
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void Update(Message msg)
        {
            Db.Entry(msg).State = EntityState.Modified;
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удаление сообщения
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void Delete(Message msg)
        {
            Db.Messages.Remove(msg);
            Db.SaveChanges();
        }

        /// <summary>
        ///     Сообщение
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        public void Delete(int id)
        {
            Delete(Get(id));
        }

    }
}