#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IMessageRepository
    {
        void SentMessage(User userWhoSented, User userWhoReceive, string msgText, string title = "");
        void SentMessage(Message message);
    }
}