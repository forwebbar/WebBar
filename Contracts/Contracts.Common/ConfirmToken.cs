using System.Runtime.Serialization;

namespace Contracts.Common
{
    [DataContract]
    public class ConfirmToken
    {

        [DataMember]
        public string Username { get; set; }
    }
}