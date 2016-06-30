using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    [DataContract]
    public class MarketDto
    {
        /// <summary>
        /// Идентификатор магазина 
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Наименование магазина
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}