#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     �����, ������� ������������� ������ �����
    /// </summary>
    public sealed class Role
    {
        /// <summary>
        ///     ����������� ��-���������� (������� ������ ������ �������������)
        /// </summary>
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        /// <summary>
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     ��� ����
        /// </summary>
        [Required]
        [Display(Name = "��� ����*")]
        public string RoleName { get; set; }

        /// <summary>
        ///     �������� ����
        /// </summary>
        [Display(Name = "�������� ����")]
        public string Description { get; set; }

        /// <summary>
        ///     ��������� �����-�������������
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}