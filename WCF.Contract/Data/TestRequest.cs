using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WCF.Validation;

namespace WCF.Contracts.Data
{
    [DataContract]
    public class TestRequest : RequestBase, IValidatableObject
    {
        [Required]
        [DataMember]
        public string Name { get; set; }

        public string CrappyLand { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new ValidationResult("CrappyLand is always wrong!");
        }
    }
}