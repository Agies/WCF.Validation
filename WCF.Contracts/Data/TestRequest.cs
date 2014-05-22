using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WCF.Validation;

namespace WCF.Contracts.Data
{
    [DataContract]
    public class TestRequest : RequestBase
    {
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}