using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Транспортный объект информации о продаже
    /// </summary>
    [DataContract]
    public class SellDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Временная метка
        /// </summary>
        [DataMember]
        public DateTimeOffset Ts { get; set; }

        /// <summary>
        /// Сумма в копейках
        /// </summary>
        [DataMember]
        public uint Money { get; set; }

        /// <summary>
        /// Цена в копейках
        /// </summary>
        [DataMember]
        public uint Price { get; set; }

        /// <summary>
        /// Объем в мл
        /// </summary>
        [DataMember]
        public uint Fill { get; set; }

        /// <summary>
        /// Расхождение в копейках
        /// </summary>
        [DataMember]
        public uint MoneyDelta { get; set; }

        /// <summary>
        /// Расхождение красное
        /// </summary>
        [DataMember]
        public bool IsMoneyDeltaRed { get; set; }

        /// <summary>
        /// Признак промывки true = Промывка
        /// </summary>
        [DataMember]
        public bool IsCleaning { get; set; }
    }
}