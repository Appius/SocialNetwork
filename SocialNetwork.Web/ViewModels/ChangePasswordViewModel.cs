#region

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        /// <summary>
        ///     Старый пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoPass")]
        [Category("Security")]
        [DataType(DataType.Password)]
        [PasswordPropertyText(true)]
        [Display(ResourceType = typeof (Resources), Name = "OldPassword")]
        public string OldPassword { get; set; }

        /// <summary>
        ///     Новый пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoPass")]
        [Category("Security")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,18}$",
            ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoValidPass")]
        [PasswordPropertyText(true)]
        [Display(ResourceType = typeof (Resources), Name = "NewPassword")]
        public string NewPassword { get; set; }

        /// <summary>
        ///     Подтверждение пароля
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoConfirmPass")]
        [Category("Security")]
        [PasswordPropertyText(true)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "ComparePasswords")]
        [Display(ResourceType = typeof (Resources), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}