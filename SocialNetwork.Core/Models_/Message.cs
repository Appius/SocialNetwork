using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Models
{
    /// <summary>
    /// ����� ��� �������� ��������� ����� ��������������
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// ���������� �������������
        /// </summary>
        public int Id { get; set; }

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

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        /// <summary>
        /// ������������, ������� ���������� ���������
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// ������������, ������� ��������� ���������
        /// </summary>
        public virtual User User1 { get; set; }
    }
}
