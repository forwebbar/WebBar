using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Contracts.Retail.Domain;

namespace Contracts.Retail.Communication
{
    [DataContract]
    public class StoreDto
    {
        [DataMember]
        public long Id { get; set; }

        public List<RackDto> RackDtos { get; set; } 
    }
}
