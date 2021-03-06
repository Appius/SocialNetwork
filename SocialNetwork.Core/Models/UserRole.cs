#region

using System;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     ����� ��� ������������ ����� ����� ������������ ����� ����� �����������
    /// </summary>
    public class UserRole
    {
        /// <summary>
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }

        /// <summary>
        ///     ����
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        ///     ������������
        /// </summary>
        public virtual User User { get; set; }
    }
}