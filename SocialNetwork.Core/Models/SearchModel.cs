#region

using System;

#endregion

namespace SocialNetwork.Core.Models
{
    public class SearchModel
    {
        /// <summary>
        ///     Термин для поиска
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        ///     Поиск по городу
        /// </summary>
        public string CurrCity { get; set; }

        /// <summary>
        ///     Поиск по электронному адресу
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Фильтр по полу
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        ///     Поиск по логику skype
        /// </summary>
        public string Skype { get; set; }
    }
}