using System;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Web.App_GlobalResources;

namespace SocialNetwork.Web.ViewModels
{
    public class AdvancedInfoViewModel
    {
        /// <summary>
        ///     Деятельность пользователя
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "Activies")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Activies { get; set; }

        /// <summary>
        ///     Интересы пользователя
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "Interests")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string Interests { get; set; }

        /// <summary>
        ///     Музыка
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "FavoriteMusic")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string FavoriteMusic { get; set; }

        /// <summary>
        ///     Фильмы
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "FavoriteMovies")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string FavoriteMovies { get; set; }

        /// <summary>
        ///     Книги
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "FavoriteBooks")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string FavoriteBooks { get; set; }

        /// <summary>
        ///     Игры
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "FavoriteGames")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string FavoriteGames { get; set; }

        /// <summary>
        ///     Цитаты
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "FavoriteQuotes")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string FavoriteQuotes { get; set; }

        /// <summary>
        ///     Блок "О себе"
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = "AboutMe")]
        [StringLength(2000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "TooManySymbols")]
        public string AboutMe { get; set; }
    }
}