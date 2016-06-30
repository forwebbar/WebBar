using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// ������������ ������ ���������� � �������
    /// </summary>
    [DataContract]
    public class SellDto
    {
        /// <summary>
        /// �������������
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [DataMember]
        public DateTimeOffset Ts { get; set; }

        /// <summary>
        /// ����� � ��������
        /// </summary>
        [DataMember]
        public uint Money { get; set; }

        /// <summary>
        /// ���� � ��������
        /// </summary>
        [DataMember]
        public uint Price { get; set; }

        /// <summary>
        /// ����� � ��
        /// </summary>
        [DataMember]
        public uint Fill { get; set; }

        /// <summary>
        /// ����������� � ��������
        /// </summary>
        [DataMember]
        public uint MoneyDelta { get; set; }

        /// <summary>
        /// ����������� �������
        /// </summary>
        [DataMember]
        public bool IsMoneyDeltaRed { get; set; }

        /// <summary>
        /// ������� �������� true = ��������
        /// </summary>
        [DataMember]
        public bool IsCleaning { get; set; }
    }
}