using System.Runtime.Serialization;

namespace WCF.Validation.Contracts
{
    [DataContract]
    public class ValidationError
    {
        public ValidationError()
        {
            
        }

        public ValidationError(string memberName, string message)
        {
            MemberName = memberName;
            Message = message;
        }

        [DataMember]
        public string MemberName { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}