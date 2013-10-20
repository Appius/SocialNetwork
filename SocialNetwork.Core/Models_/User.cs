#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     ����� ������������
    /// </summary>
    public partial class User
    {
        /// <summary>
        ///     ����������� ��-��������� (������� ������ ���������)
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
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Email ������������
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     ������ ������������
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     ��� ������������
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        ///     ������� ������������
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     ���-���� ������������
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     ���� �������� ������������
        /// </summary>
        [Required]
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     ������ ������������
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        ///     ��������� ������� ������������
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     ��� ������������ (0 - male, 1 - female)
        /// </summary>
        [Required]
        public bool Sex { get; set; }

        /// <summary>
        ///     ������� ������������
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        ///     ����� � �����
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        ///     ������� ����� ������������
        /// </summary>
        public string CurrentCity { get; set; }

        /// <summary>
        ///     ������������ ������������
        /// </summary>
        public string Activies { get; set; }

        /// <summary>
        ///     �������� ������������
        /// </summary>
        public string Interests { get; set; }

        /// <summary>
        ///     ������
        /// </summary>
        public string FavoriteMusic { get; set; }

        /// <summary>
        ///     ������
        /// </summary>
        public string FavoriteMovies { get; set; }

        /// <summary>
        ///     �����
        /// </summary>
        public string FavoriteBooks { get; set; }

        /// <summary>
        ///     ����
        /// </summary>
        public string FavoriteGames { get; set; }

        /// <summary>
        ///     ������
        /// </summary>
        public string FavoriteQuotes { get; set; }

        /// <summary>
        ///     ���� "� ����"
        /// </summary>
        public string AboutMe { get; set; }

        /// <summary>
        ///     ������ ������, �� ������� �������� ����������� �� ������
        /// </summary>
        public ICollection<FriendShip> RequestedFriends { get; set; }

        /// <summary>
        ///     ������ ������, ������� ������� �����������
        /// </summary>
        public ICollection<FriendShip> ReceivedFriends { get; set; }

        /// <summary>
        ///     �������� ���������
        /// </summary>
        public ICollection<Message> InboxMessages { get; set; }

        /// <summary>
        ///     ��������� ���������
        /// </summary>
        public ICollection<Message> SentMessages { get; set; }

        /// <summary>
        ///     ���� ������������
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}