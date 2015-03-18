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
        ICollection<ValidationResult> Errors { get; set; }
        void AddModelError(string member, string message);
        void Attach(OperationContext owner);
        void Detach(OperationContext owner);
    }

    public class ModelState : IExtension<OperationContext>, IModelState
    {
        public ModelState()
        {
            Errors = new Collection<ValidationResult>();
        }

        public bool IsValid
        {
            get { return !Errors.Any(); }
        }

        public void AddModelError(string member, string message)
        {
            AddError(new ValidationResult(message, new[] { member }));
        }

        public void AddError(ValidationResult error)
        {
            if (Errors.Any(er => er.MemberNames.SequenceEqual(error.MemberNames) &&
                                 er.ErrorMessage == error.ErrorMessage))
                return;
            Errors.Add(error);
        }

        public ICollection<ValidationResult> Errors { get; set; }

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