using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Web.Mappers
{
    interface IMapper
    {
        object Map(object source, Type sourceType, Type destinationType);
    }
}
