#region

using System;

#endregion

namespace SocialNetwork.Web.Mappers
{
    internal interface IMapper
    {
        object Map(object source, Type sourceType, Type destinationType);
    }
}