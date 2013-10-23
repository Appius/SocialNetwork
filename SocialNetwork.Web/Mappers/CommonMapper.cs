using System;
using AutoMapper;

// TODO: Move to Core?
namespace SocialNetwork.Web.Mappers
{
    /// <summary>
    /// Основной Mapper
    /// </summary>
    public class CommonMapper: IMapper
    {
        static CommonMapper() {}

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}