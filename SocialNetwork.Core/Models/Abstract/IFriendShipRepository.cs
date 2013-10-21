#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IFriendShipRepository
    {
        void SentRequest(User userWhoRequest, User userWhoReceive);
        void SentRequest(FriendShip friendShip);
    }
}