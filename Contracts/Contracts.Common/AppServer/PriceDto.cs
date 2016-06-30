using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// ������������ ������ ���� �������
    /// </summary>
    [DataContract]
    public class PriceDto
    {
        /// <summary>
        /// ������������� ����
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// ������� ���� � ��������
        /// </summary>
        [DataMember]
        public int CurrentVal { get; set; }

        /// <summary>
        /// ������� ���� � ��������
        /// </summary>
        [DataMember]
        public int FuturePrice { get; set; }

        /// <summary>
        /// ���� ������ �������� ������� ����
        /// </summary>
        [DataMember]
        public DateTimeOffset FutureDate { get; set; }
    }
}