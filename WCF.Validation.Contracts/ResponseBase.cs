using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCF.Validation.Contracts
{
    [DataContract]
    public class ResponseBase : IHaveValidationErrors
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

        public void AddValidationError(string memberName, string errorMessage)
        {
            Errors.Add(new ValidationError
                       {
                           MemberName = memberName,
                           Message = errorMessage,
                       });
        }
    }
}