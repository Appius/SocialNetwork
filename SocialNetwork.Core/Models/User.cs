#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Класс пользователя
    /// </summary>
    public class User
    {
        protected bool Equals(User other)
        {
            return Id == other.Id && string.Equals(Email, other.Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id*397) ^ (Email != null ? Email.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Конструктор по-умолчанию (создает пустые коллекции)
        /// </summary>
        public User()
        {
            RequestedFriends = new List<FriendShip>();
            ReceivedFriends = new List<FriendShip>();
            InboxMessages = new List<Message>();
            SentMessages = new List<Message>();
            UserRoles = new List<UserRole>();
        }

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
        public byte[] Avatar { get; set; }

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
        ///     Заблокирован ли пользователь
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        ///     Список друзей, от которых получены приглашения на дружбу
        /// </summary>
        public ICollection<FriendShip> RequestedFriends { get; set; }

        /// <summary>
        ///     Список друзей, которым выслано приглашения
        /// </summary>
        public ICollection<FriendShip> ReceivedFriends { get; set; }

        /// <summary>
        ///     Входящие сообщения
        /// </summary>
        public ICollection<Message> InboxMessages { get; set; }

        /// <summary>
        ///     Исходящие сообщения
        /// </summary>
        public ICollection<Message> SentMessages { get; set; }

        /// <summary>
        ///     Роли пользователя
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}