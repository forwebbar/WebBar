using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// ������������ ������ ����������
    /// </summary>
    [DataContract]
    public class ProducerDto
    {
        /// <summary>
        /// ������������� ����������
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// �������� ������� ID
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [DataMember]
        public string Inn { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [DataMember]
        public string Kpp { get; set; }

        /// <summary>
        /// ����(��)
        /// </summary>
        [DataMember]
        public string Ogrn { get; set; }

        /// <summary>
        /// �/�
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [DataMember]
        public string Bik { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [DataMember]
        public string Bank { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [DataMember]
        public DateTimeOffset? ActualDate { get; set; }

        /// <summary>
        /// ����������� ������
        /// </summary>
        [DataMember]
        public string LawAdress { get; set; }

        /// <summary>
        /// ����������� ������
        /// </summary>
        [DataMember]
        public string ActualAdress { get; set; }
    }
}