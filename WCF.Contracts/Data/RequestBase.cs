using System;
using System.Runtime.Serialization;

namespace WCF.Validation
{
    [DataContract]
    public class RequestBase
    {
        [DataMember]
        public Guid MessageId { get; set; }

        public RequestBase()
        {
            MessageId = Guid.NewGuid();
        }
    }
}