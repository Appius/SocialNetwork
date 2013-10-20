#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    /// ����� ��� �������� ��������� ����� ��������������
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ���������� �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ������������� ������������, ������ �������� ���������
        /// </summary>
        public int User1Id { get; set; }

        /// <summary>
        /// ������������� ������������, ������ �������� ���������
        /// </summary>
        public int User2Id { get; set; }

        /// <summary>
        /// ��������� ��������� (�������������)
        /// </summary>
        [Display(Name = "��������� ���������")]
        public string Title { get; set; }

        /// <summary>
        /// ����� ���������
        /// </summary>
        [Required]
        [Display(Name = "����� ���������*")]
        public string MsgText { get; set; }

        /// <summary>
        /// ����� ������� ���������
        /// </summary>
        public DateTime PostedDate { get; set; }

        /// <summary>
        /// ������������, ������� ���������� ���������
        /// </summary>
        [Required]
        public virtual User UserWhoSent { get; set; }

        /// <summary>
        /// ������������, ������� ��������� ���������
        /// </summary>
        [Required]
        public virtual User UserWhoReceive { get; set; }
    }
}