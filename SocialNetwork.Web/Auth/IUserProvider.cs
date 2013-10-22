using System;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Web.Auth
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}
