#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    internal interface IMessageRepository
    {
        bool SentMessage(User userWhoSented, User userWhoReceive, string msgText, string title = "");
    }
}