using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    [DataContract]
    public class MarketDetailConfigDto
    {
        /// <summary>
        /// ������������� ��������
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// ���������� ������� ID
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// ������������ ��������
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// ����� ��������
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// ���������� ������� "���������� �"
        /// </summary>
        [DataMember]
        public string DeviceSerial { get; set; }

        /// <summary>
        /// ���� ������ �������� ���������
        /// </summary>
        [DataMember]
        public DateTimeOffset ActualDate { get; set; }

        /// <summary>
        /// ������� ��������
        /// </summary>
        [DataMember]
        public List<MarketDrinkListItemDto> Drinks { get; set; }
    }
}