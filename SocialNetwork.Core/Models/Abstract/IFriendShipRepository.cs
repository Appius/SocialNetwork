#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    internal interface IFriendShipRepository
    {
        void SentRequest(User userWhoRequest, User userWhoReceive);
        void SentRequest(FriendShip friendShip);
    }
}