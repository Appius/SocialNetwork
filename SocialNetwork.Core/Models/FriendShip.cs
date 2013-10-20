#region

using System;

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
        ///     ������������� ������������, ������� ������ ������
        /// </summary>
        public int User1Id { get; set; }

        /// <summary>
        ///     ������������� ������������, ������� �������� ������
        /// </summary>
        public int User2Id { get; set; }

        /// <summary>
        ///     ���� ���������� �������
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        ///     ������������, ������� ������ ������ �� ������
        /// </summary>
        public virtual User UserWhoRequest { get; set; }

        /// <summary>
        ///     ������������, ������� ��������� ������ �� ������
        /// </summary>
        public virtual User UserWhoReceive { get; set; }
    }
}