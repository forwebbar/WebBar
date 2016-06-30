using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// ������������ ������ ������� ��������
    /// </summary>
    [DataContract]
    public class MarketDrinkListItemDto
    {
        /// <summary>
        /// ���������� ������� "����"
        /// </summary>
        [DataMember]
        public int TapCode { get; set; }

        /// <summary>
        /// ������� "������� ����"
        /// </summary>
        [DataMember]
        public DrinkDto CurDrink { get; set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        [DataMember]
        public DrinkDto FutureDrink { get; set; }

        /// <summary>
        /// ������� ���� "���� �"
        /// </summary>
        [DataMember]
        public DateTimeOffset FutureDrinkDate { get; set; }

        /// <summary>
        /// ����� ����
        /// </summary>
        [DataMember]
        public DrinkDto NewDrink { get; set; }
    }
}