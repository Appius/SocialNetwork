#region

using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class AddFriendViewModel
    {
        /// <summary>
        ///     Email пользователя, кому высылается запрос
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Сообщение
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Message")]
        [StringLength(100, ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Message { get; set; }

        /// <summary>
        ///     Кому сообщение
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "ToUser")]
        public string ToUser { get; set; }
    }
}