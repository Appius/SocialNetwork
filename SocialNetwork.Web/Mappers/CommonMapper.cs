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
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}