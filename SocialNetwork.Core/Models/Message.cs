#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     ����� ��� �������� ��������� ����� ��������������
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     ��������� ��������� (�������������)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     ����� ���������
        /// </summary>
        public string MsgText { get; set; }

        /// <summary>
        ///     ����� ������� ���������
        /// </summary>
        public DateTime PostedDate { get; set; }

        /// <summary>
        ///     ��������� ��������� ��� ���
        /// </summary>
        public bool IsRead { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        ///     ������������, ������� ���������� ���������
        /// </summary>
        public virtual User FromUser { get; set; }

        /// <summary>
        ///     ������������, ������� ��������� ���������
        /// </summary>
        public virtual User ToUser { get; set; }
    }
}