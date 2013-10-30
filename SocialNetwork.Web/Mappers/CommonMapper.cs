using System;
using AutoMapper;
// TODO: Move to Core?
using SocialNetwork.Core.Models;
using SocialNetwork.Web.ViewModels;

namespace SocialNetwork.Web.Mappers
{
    /// <summary>
    /// Основной Mapper
    /// </summary>
    public class CommonMapper: IMapper
    {
        static CommonMapper()
        {
            Mapper.CreateMap<RegisterViewModel, User>();
            Mapper.CreateMap<User, AdvancedInfoViewModel>();
            Mapper.CreateMap<User, GeneralInfoViewModel>();
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<Message, MessageViewModel>();
            Mapper.CreateMap<User, FriendShipRequestViewModel>();
            Mapper.CreateMap<User, UserFullInfoViewModel>();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}