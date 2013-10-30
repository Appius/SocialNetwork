#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IFriendShipRepository
    {
        /// <summary>
        ///     Послать запрос на дружбу
        /// </summary>
        /// <param name="fromUser">Пользователь, который послал запрос</param>
        /// <param name="toUser">Пользователь, которому пришло приглашение</param>
        /// <param name="msg">Сообщение</param>
        void SentRequest(User fromUser, User toUser, string msg);

        /// <summary>
        ///     Послать запрос на дружбу
        /// </summary>
        /// <param name="friendShip">friendShip</param>
        void SentRequest(FriendShip friendShip);

        /// <summary>
        ///     Получения списка друзей для пользовеля
        /// </summary>
        /// <param name="forUser">Пользователь</param>
        IEnumerable<FriendShip> GetFriends(User forUser);

        /// <summary>
        ///     Получение списка исходящих запросов на дружбу
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <returns></returns>
        IEnumerable<FriendShip> GetOutboxRequests(User fromUser);

        /// <summary>
        ///     Получение списка входящих запросов на дружбу
        /// </summary>
        /// <param name="toUser">Какому пользователю</param>
        IEnumerable<FriendShip> GetInboxRequests(User toUser);

        /// <summary>
        ///     Подтвердить заявку на дружбу
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <param name="toUser">К пользователю</param>
        void Confirm(User fromUser, User toUser);

        /// <summary>
        ///     Удалить заявку на дружбу (или дружественную связь)
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <param name="toUser">К пользователю</param>
        void Remove(User fromUser, User toUser);

        /// <summary>
        ///     Возвращает true если пользователи друзья
        /// </summary>
        /// <param name="user1">Первый пользователь</param>
        /// <param name="user2">Второй пользователь</param>
        bool IsFriends(User user1, User user2);
    }
}