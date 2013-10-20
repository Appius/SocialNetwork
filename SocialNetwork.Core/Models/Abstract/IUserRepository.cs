#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    internal interface IUserRepository
    {
        User Get(int id);
        IEnumerable<User> GetAll();
        void Add(User document);
        void Update(User document);
        void Delete(int id);
    }
}