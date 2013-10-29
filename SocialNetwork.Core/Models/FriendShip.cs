#region

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     �����, ������� ��������� ������������� "����� ������"
    /// </summary>
    public class FriendShip
    {
        /// <summary>
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     ���� ���������� �������
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        ///     ��������� ��� ������� �������
        /// </summary>
        public string Message { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     ������������, ������� ������ ������ �� ������
        /// </summary>
        [ForeignKey("User1Id")]
        public virtual User FromUser { get; set; }

        /// <summary>
        ///     ������������, ������� ��������� ������ �� ������
        /// </summary>
        [ForeignKey("User2Id")]
        public virtual User ToUser { get; set; }
    }
}