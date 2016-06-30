using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Траспортный объект суммарных продаж
    /// </summary>
    [DataContract]
    public class SellSummaryDto
    {
        /// <summary>
        /// Сумма в копейках
        /// </summary>
        [DataMember]
        public uint Money { get; set; }

        /// <summary>
        /// Изменение суммы в процентах (знаковая величина)
        /// </summary>
        [DataMember]
        public int MoneyShift { get; set; }

        /// <summary>
        /// Картинка с изменениями
        /// </summary>
        [DataMember]
        public byte[] Img { get; set; }

        /// <summary>
        /// Пролив в мл
        /// </summary>
        [DataMember]
        public uint Fill { get; set; }

        /// <summary>
        /// Изменение пролива в процентах (знаковая величина)
        /// </summary>
        [DataMember]
        public int FillShift { get; set; }

        /// <summary>
        /// Расхождение в процентах
        /// </summary>
        [DataMember]
        public uint MoneyDelta { get; set; }
    }
}