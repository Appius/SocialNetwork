#region

using System;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Core.Models.Repos;
using SocialNetwork.Web.Auth;

#endregion

namespace SocialNetwork.Web
{
    public class DependencyInjection
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<FriendShipRepository>().As<IFriendShipRepository>().InstancePerHttpRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerHttpRequest();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>().InstancePerHttpRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerHttpRequest();
            builder.RegisterType<CustomAuthentication>().As<IAuthentication>().InstancePerHttpRequest();
//            kernel.Bind<IAuthentication>().To<CustomAuthentication>().InRequestScope();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}