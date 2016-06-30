using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts.Retail.Communication
{
    [DataContract]
    public class RackDto
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public List<ShelfDto> ShelfDtos { get; set; } 
    }
}