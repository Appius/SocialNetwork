#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IRoleRepository
    {
        Role Get(int id);
        IEnumerable<Role> GetAll();
        void Add(Role document);
        void Update(Role document);
        void Delete(Role role);
        void Delete(int id);
    }
}