using System;
using System.Collections.Generic;

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     �����, ������� ��������� ������������� "����� ������"
    /// </summary>
    public partial class FriendShip
    {
        /// <summary>
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     ���� ���������� �������
        /// </summary>
        public DateTime RequestDate { get; set; }


        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     ������������, ������� ������ ������ �� ������
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     ������������, ������� ��������� ������ �� ������
        /// </summary>
        public virtual User User1 { get; set; }
    }
}
