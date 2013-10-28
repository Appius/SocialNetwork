#region

using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class NewMessageViewModel
    {
        /// <summary>
        ///     Заголовок сообщения (необязательно)
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        ///     Текст сообщения
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoMsgText")]
        [Display(ResourceType = typeof (Resources), Name = "Msg")]
        public string MsgText { get; set; }

        /// <summary>
        ///     Кому сообщение
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "ToUser")]
        public string ToUser { get; set; }

        /// <summary>
        ///     Кому сообщение
        /// </summary>
        public string Email { get; set; }
    }
}