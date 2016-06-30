using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// ������������ ������ �������� �����
    /// </summary>
    [DataContract]
    public class DrinkConfigDto
    {
        /// <summary>
        /// ������������� �����
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
        /// ������������� ����������
        /// </summary>
        [DataMember]
        public int IdProducer { get; set; }

        /// <summary>
        /// �������� ������� "���������"
        /// </summary>
        [DataMember]
        public string ProducerName { get; set; }
    }
}