#region

using System;
using System.Globalization;
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
        /// <param name="newNumber">Число, которое будет отображаться справа от названия</param>
        /// <param name="id">Атрибут id</param>
        public static MvcHtmlString ActionMenuItem(this HtmlHelper helper, string linkText,
            string actionName, string controllerName, string currentController, string currentAction, int newNumber = 0,string id = "")
        {
            var tag = new TagBuilder("li");



            if (String.Compare(controllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0
                && String.Compare(actionName, currentAction, StringComparison.OrdinalIgnoreCase) == 0)
            {
                tag.AddCssClass("active");
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                var spanTag = new TagBuilder("span");
                spanTag.MergeAttribute("id", id);
                spanTag.AddCssClass("badge pull-right");
                if (newNumber != 0)
                    spanTag.InnerHtml = newNumber.ToString(CultureInfo.InvariantCulture);
                else
                    spanTag.MergeAttribute("style", "display: none;");
                linkText = linkText + " " + spanTag;
            }

            tag.InnerHtml = helper.ActionLink(linkText, actionName, controllerName).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        /// Генерация меню настроек пользователя
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="linkText">Текст ссылки</param>
        /// <param name="act">Категория настроек</param>
        /// <param name="currentAct">Текущая категория</param>
        /// <param name="controllerName">Имя контроллера</param>
        /// <param name="newNumber">Число, которое будет отображаться справа от названия</param>
        /// <param name="actionName">Имя метода</param>
        public static MvcHtmlString MenuItem(this HtmlHelper helper, string linkText, string act, string currentAct, string actionName, string controllerName, int newNumber = 0)
        {
            var tag = new TagBuilder("li");

            if ((String.Compare(act, currentAct, StringComparison.OrdinalIgnoreCase) == 0) ||
                (string.IsNullOrWhiteSpace(act) && string.IsNullOrWhiteSpace(currentAct)))
                tag.AddCssClass("active");

            if (newNumber != 0)
            {
                var spanTag = new TagBuilder("span");
                spanTag.AddCssClass("badge pull-right");
                spanTag.InnerHtml = newNumber.ToString(CultureInfo.InvariantCulture);
                linkText = linkText + spanTag;
            }

            tag.InnerHtml = helper.ActionLink(linkText, actionName, controllerName, new {act}, null).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        ///     Ссылка на зону администрирования
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static MvcHtmlString GetAdminLink(this HtmlHelper helper)
        {
            if (HttpContext.Current.User.IsInRole("admin"))
            {
                return
                    MvcHtmlString.Create(" | " +
                                         helper.ActionLink("Admin page", "Index", "Home", new {Area = "admin"}, new {}));
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     Ссылка на основную часть сайта
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static MvcHtmlString GetRootLink(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(" | " + helper.ActionLink("Root part", "Index", "Home", new { Area = "" }, new { }));
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
        ///     Картинка-ссылка с подтверждением
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="action">Метод в контроллере</param>
        /// <param name="controller">Контроллер</param>
        /// <param name="routeValues">Роуты</param>
        /// <param name="glyphName">Иконка</param>
        /// <param name="alt">Замещающий текст</param>
        /// <param name="confirm">Текст подтверждения</param>
        /// <param name="cssclass">CSS класс</param>
        /// <param name="text">Текст возле картинки</param>
        public static MvcHtmlString ActionGlyph(this HtmlHelper html, string action, string controller,
            object routeValues, string glyphName, string alt, string confirm, string cssclass="", string text="")
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("span");
            imgBuilder.MergeAttribute("class", "glyphicon " + glyphName);
            var imgHtml = imgBuilder.ToString(TagRenderMode.Normal);
            
            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            if (!String.IsNullOrEmpty(confirm))
            {
                anchorBuilder.MergeAttribute("onclick", "return confirm('" + confirm + "');");
            }
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            if (!string.IsNullOrWhiteSpace(cssclass))
                anchorBuilder.MergeAttribute("class", cssclass);

            anchorBuilder.InnerHtml = imgHtml + " " + text;

            var anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        /// <summary>
        ///     Получение номера версии приложения
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static HtmlString GetCopyrightText(this HtmlHelper helper)
        {
            string copyright = AssemblyInfo.Copyright;
            string version = AssemblyInfo.VersionFull;
            string company = AssemblyInfo.Company;
            return new HtmlString(string.Format("{0} {1} | Version: {2}", copyright, company, version));
        }
    }
}