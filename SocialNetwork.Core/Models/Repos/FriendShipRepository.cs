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
        /// <param name="fromUser">Пользователь, который послал запрос</param>
        /// <param name="toUser">Пользователь, которому пришло приглашение</param>
        public void SentRequest(User fromUser, User toUser)
        {
            SentRequest(new FriendShip
            {
                RequestDate = DateTime.Now,
                FromUser = fromUser,
                ToUser = toUser
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