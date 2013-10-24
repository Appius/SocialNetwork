#region

using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

#endregion

namespace SocialNetwork.Web.Core
{
    /// <summary>
    ///     HTML расширения
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        ///     Генерация еденицы меню в соответствии с Bootstrap
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="linkText">Текст ссылки</param>
        /// <param name="actionName">Метод в контроллере</param>
        /// <param name="controllerName">Контроллер, куда попадем</param>
        /// <param name="currentController">Текущий контроллер</param>
        /// <param name="currentAction">Текущее действие</param>
        public static MvcHtmlString ActionMenuItem(this HtmlHelper helper, string linkText,
            string actionName, string controllerName, string currentController, string currentAction)
        {
            var tag = new TagBuilder("li");

            if (String.Compare(controllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0
                && String.Compare(actionName, currentAction, StringComparison.OrdinalIgnoreCase) == 0)
            {
                tag.AddCssClass("active");
            }

            tag.InnerHtml = helper.ActionLink(linkText, actionName, controllerName).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        ///     Ссылка на зону администрирования
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static MvcHtmlString GetAdminLink(this HtmlHelper helper)
        {
            /*var user = HttpContext.Current.User.Identity.Name;
            var adminUsers = ConfigurationManager.AppSettings["App.Admins"].Replace(" ", "").Split(',');
            var userIsAdmin = Array.Exists(adminUsers, s => s == user);
            if (userIsAdmin)
            {
                return
                    MvcHtmlString.Create(" | " +
                                         helper.ActionLink("Admin page", "Index", "Home", new {Area = "admin"}, new {}));
            }*/
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     Картинка-ссылка с подтверждением
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="action">Метод в контроллере</param>
        /// <param name="controller">Контроллер</param>
        /// <param name="routeValues">Роуты</param>
        /// <param name="imagePath">Путь к картинке</param>
        /// <param name="alt">Замещающий текст</param>
        /// <param name="confirm">Текст подтверждения</param>
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controller,
            object routeValues, string imagePath, string alt, string confirm)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("title", alt);
            var imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            if (!String.IsNullOrEmpty(confirm))
            {
                anchorBuilder.MergeAttribute("onclick", "return confirm('" + confirm + "');");
            }
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            var anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        /// <summary>
        ///     Получение номера версии приложения
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static HtmlString ApplicationVersion(this HtmlHelper helper)
        {
            var asm = Assembly.GetExecutingAssembly();
            var version = asm.GetName().Version;
            var product =
                asm.GetCustomAttributes(typeof (AssemblyProductAttribute), true).FirstOrDefault() as
                    AssemblyProductAttribute;

            if (version != null && product != null)
                return
                    new HtmlString(string.Format("<span>v{0}.{1}.{2}.{3}</span>", version.Major,
                        version.Minor, version.Build, version.Revision));
            return new HtmlString("");
        }
    }
}