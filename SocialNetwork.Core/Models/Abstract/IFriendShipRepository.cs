#region

using System;

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
    }
}