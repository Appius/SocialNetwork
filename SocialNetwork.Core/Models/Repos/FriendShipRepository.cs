#region

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="msg">Сообщение</param>
        public void SentRequest(User fromUser, User toUser, string msg)
        {
            SentRequest(new FriendShip
            {
                RequestDate = DateTime.Now,
                User1Id = fromUser.Id,
                User2Id = toUser.Id,
                Message = msg
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

        /// <summary>
        ///     Получение списка входящих запросов на дружбу
        /// </summary>
        /// <param name="toUser">Какому пользователю</param>
        public IEnumerable<FriendShip> GetInboxRequests(User toUser)
        {
            return Db.FriendShips.Where(item => item.User2Id == toUser.Id && !item.IsConfirmed);
        }

        /// <summary>
        ///     Получение списка исходящих запросов на дружбу
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <returns></returns>
        public IEnumerable<FriendShip> GetOutboxRequests(User fromUser)
        {
            return Db.FriendShips.Where(item => item.User1Id == fromUser.Id && !item.IsConfirmed);
        }

        /// <summary>
        ///     Получения списка друзей для пользовеля
        /// </summary>
        /// <param name="forUser">Пользователь</param>
        public IEnumerable<FriendShip> GetFriends(User forUser)
        {
            return
                Db.FriendShips.Where(
                    item =>
                        ((item.User2Id == forUser.Id && item.IsConfirmed) ||
                         (item.User1Id == forUser.Id && item.IsConfirmed)));
        }

        /// <summary>
        ///     Подтвердить заявку на дружбу
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <param name="toUser">К пользователю</param>
        public void Confirm(User fromUser, User toUser)
        {
            var friendship =
                Db.FriendShips.FirstOrDefault(item => item.User1Id == fromUser.Id && item.User2Id == toUser.Id);
            if (friendship == null) return;

            friendship.IsConfirmed = true;
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удалить заявку на дружбу
        /// </summary>
        /// <param name="fromUser">От пользователя</param>
        /// <param name="toUser">К пользователю</param>
        public void Remove(User fromUser, User toUser)
        {
            var friendship =
                Db.FriendShips.FirstOrDefault(item => item.User1Id == fromUser.Id && item.User2Id == toUser.Id);
            if (friendship == null) return;

            Db.FriendShips.Remove(friendship);
            Db.SaveChanges();
        }
    }
}