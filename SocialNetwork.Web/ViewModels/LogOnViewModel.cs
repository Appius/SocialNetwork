#region

using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    /// <summary>
    ///     Моделька на представление входа
    /// </summary>
    public class LogOnViewModel
    {
        /// <summary>
        ///     Логин
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoEmail")]
        [Display(ResourceType = typeof (Resources), Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoPass")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resources), Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Запомнить данные
        /// </summary>
        [Display(ResourceType = typeof (Resources), Name = "IsRemember")]
        public bool RememberMe { get; set; }
    }
}