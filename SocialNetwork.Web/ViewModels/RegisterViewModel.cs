using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

namespace SocialNetwork.Web.ViewModels
{
    /// <summary>
    ///     Моделька на представление регистрации
    /// </summary>
    public class RegisterViewModel
    {
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
        [Display(ResourceType = typeof(Resources), Name = "MiddleName")]
        public string MiddleName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        [EmailAddress]
//        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoValidEmail")]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof (Resources),
            ErrorMessageResourceName = "AllowedLengthEmail")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoEmail")]
        [Display(ResourceType = typeof(Resources), Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoSex")]
        [Display(ResourceType = typeof(Resources), Name = "Sex")]
        public bool Sex { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
//        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoPass")]
        [Category("Security")]
//        [ValidPassword(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NoValidPass")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,18}$",
            ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoValidPass")]
        [PasswordPropertyText]
        [Display(ResourceType = typeof (Resources), Name = "Password")]
        public bool Password { get; set; }

        /// <summary>
        ///     Подтверждение пароля
        /// </summary>
//        [Required]
        [Category("Security")]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ComparePasswords")]
        [Display(ResourceType = typeof(Resources), Name = "ConfirmPassword")]
        public bool ConfirmPassword { get; set; }

        /// <summary>
        ///     День рожденье
        /// </summary>
        [Required(ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoBirthday")]
        [Display(ResourceType = typeof (Resources), Name = "Birthday")]
        [RegularExpression(@"^(3[01]|[12][0-9]|0?[1-9])\.(1[0-2]|0?[1-9])\.(?:[0-9]{2})?[0-9]{2}$",
            ErrorMessageResourceType = typeof (Resources), ErrorMessageResourceName = "NoValidDate")]
        public string Birthday { get; set; }
    }
}