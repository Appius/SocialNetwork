#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    internal interface IFriendShipRepository
    {
        bool SentRequest(User userWhoRequest, User userWhoReceive, string msg);
    }
}