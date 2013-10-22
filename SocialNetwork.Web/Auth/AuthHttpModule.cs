#region

using System;
using System.Web;
using System.Web.Mvc;

#endregion

namespace SocialNetwork.Web.Auth
{
    /// <summary>
    ///     Класс, реализирующий IHttpModule
    /// </summary>
    public class AuthHttpModule : IHttpModule
    {
        /// <summary>
        ///     Иницилизация HttpModule
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Authenticate;
        }

        /// <summary>
        ///     Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Проверка подлинности
        /// </summary>
        /// <param name="source">Ресурс</param>
        /// <param name="e">Парметры</param>
        private static void Authenticate(Object source, EventArgs e)
        {
            var app = (HttpApplication) source;
            HttpContext context = app.Context;

            var auth = DependencyResolver.Current.GetService<IAuthentication>();
            auth.HttpContext = context;

            context.User = auth.CurrentUser;
        }
    }
}