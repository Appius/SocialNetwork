#region

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class GeneralInfoViewModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoName")]
        [Display(ResourceType = typeof (Resources), Name = "Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "AllowedLengthName")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoLastName")]
        [Display(ResourceType = typeof (Resources), Name = "LastName")]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "AllowedLengthLastName")]
        public string LastName { get; set; }

        /// <summary>
        ///     Отчество пользователя
        /// </summary>
        [StringLength(100, ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "AllowedLengthMiddleName")]
        [Display(ResourceType = typeof (Resources), Name = "MiddleName")]
        public string MiddleName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        [EmailAddress(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoValidEmail",
            ErrorMessage = null)]
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoEmail")]
        [Display(ResourceType = typeof (Resources), Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Текущий город
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "CurrentCity")]
        [StringLength(50, ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string CurrentCity { get; set; }

        /// <summary>
        ///     Мобильный телефон пользователя
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Mobile")]
        [StringLength(20, ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Mobile { get; set; }

        /// <summary>
        ///     Skype
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Skype")]
        [StringLength(50, ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Skype { get; set; }

        /// <summary>
        ///     Website
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Website")]
        [StringLength(100, ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Website { get; set; }

        /// <summary>
        ///     Аватар пользователя
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "Avatar")]
        [StringLength(255, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string AvatarUrl { get; set; }
    }
}