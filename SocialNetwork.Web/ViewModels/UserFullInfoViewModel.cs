using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.ViewModels
{
    public class UserFullInfoViewModel
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Ник-нейм пользователя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     День рождения пользователя
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     Аватар пользователя
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Мобильный телефон пользователя
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     Пол пользователя (0 - male, 1 - female)
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        ///     Вебсайт пользователя
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        ///     Логин в Скайп
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        ///     Текущий город пользователя
        /// </summary>
        public string CurrentCity { get; set; }

        /// <summary>
        ///     Деятельность пользователя
        /// </summary>
        public string Activies { get; set; }

        /// <summary>
        ///     Интересы пользователя
        /// </summary>
        public string Interests { get; set; }

        /// <summary>
        ///     Музыка
        /// </summary>
        public string FavoriteMusic { get; set; }

        /// <summary>
        ///     Фильмы
        /// </summary>
        public string FavoriteMovies { get; set; }

        /// <summary>
        ///     Книги
        /// </summary>
        public string FavoriteBooks { get; set; }

        /// <summary>
        ///     Игры
        /// </summary>
        public string FavoriteGames { get; set; }

        /// <summary>
        ///     Цитаты
        /// </summary>
        public string FavoriteQuotes { get; set; }

        /// <summary>
        ///     Блок "О себе"
        /// </summary>
        public string AboutMe { get; set; }

        /// <summary>
        /// Друг?
        /// </summary>
        public bool IsFriend { get; set; }
    }
}