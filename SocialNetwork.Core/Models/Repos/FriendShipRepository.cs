#region

using System;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    /// <summary>
    ///     Класс, который контролирует друзей пользователей
    /// </summary>
    public class FriendShipRepository : RepositoryBase, IFriendShipRepository
    {
        /// <summary>
        ///     Послать запрос на дружбу
        /// </summary>
        /// <param name="userWhoRequest">Пользователь, который послал запрос</param>
        /// <param name="userWhoReceive">Пользователь, которому пришло приглашение</param>
        public void SentRequest(User userWhoRequest, User userWhoReceive)
        {
            SentRequest(new FriendShip
            {
                RequestDate = DateTime.Now,
                FromUser = userWhoReceive,
                ToUser = userWhoRequest
            });
        }

        /// <summary>
        ///     Послать запрос на дружбу
        /// </summary>
        /// <param name="friendShip">friendShip</param>
        public void SentRequest(FriendShip friendShip)
        {
            Db.FriendShips.Add(friendShip);
            Db.SaveChanges();
        }
    }
}