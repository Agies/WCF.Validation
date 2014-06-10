using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel;

namespace WCF.Validation
{
    public interface IModelState
    {
        bool IsValid { get; }
        ICollection<ValidationResult> Errors { get; }
        void AddModelError(string member, string message);
        void Attach(OperationContext owner);
        void Detach(OperationContext owner);
    }

    public class ModelState : IExtension<OperationContext>, IModelState
    {
        public bool IsValid
        {
            get { return !Errors.Any(); }
        }

        public void AddModelError(string member, string message)
        {
            Errors.Add(new ValidationResult(message, new[] {member}));
        }

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