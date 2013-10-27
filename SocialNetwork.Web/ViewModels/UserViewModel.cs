#region

using System;

#endregion

namespace SocialNetwork.Web.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        ///     Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     Возраст
        /// </summary>
        public int Old { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Город
        /// </summary>
        public string CurrentCity { get; set; }
    }
}