using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCF.Validation
{
    [DataContract]
    public abstract class ResponseBase
    {
        protected ResponseBase(RequestBase requestBase) : this()
        {
            RequestId = requestBase.MessageId;
        }

        [DataMember]
        public Guid RequestId { get; set; }

        public ResponseBase()
        {
            Errors = new List<ValidationError>();
        }

        [DataMember]
        public List<ValidationError> Errors { get; set; }
    }
}