using System.Runtime.Serialization;

namespace Contracts.Retail.Communication
{
    [DataContract]
    public class ShelfDto
    {
        [DataMember]
        public long Id { get; set; }
    }
}