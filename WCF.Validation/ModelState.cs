using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;

namespace WCF.Validation
{
    public class ModelState : IExtension<OperationContext>
    {
        public ICollection<ValidationResult> Errors { get; private set; }

        public static ModelState Current 
        {
            get { return OperationContext.Current.Extensions.Find<ModelState>(); }
        }

        public void Attach(OperationContext owner)
        {
            Errors = new Collection<ValidationResult>();
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}