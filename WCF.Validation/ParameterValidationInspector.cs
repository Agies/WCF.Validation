using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace WCF.Validation
{
    public class ParameterValidationInspector : IParameterInspector
    {
        private readonly IRequestValidator _requestValidator;

        public ParameterValidationInspector(IRequestValidator requestValidator)
        {
            _requestValidator = requestValidator;
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            OperationContext.Current.Extensions.Add(new ModelState());
            _requestValidator.Validate(inputs);
            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            var result = returnValue as IHaveValidationErrors;
            if (result == null) return;

            foreach (var validationResult in ModelState.Current.Errors)
            {
                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        result.AddValidationError(memberName, validationResult.ErrorMessage);
                    }
                }
                else
                {
                    result.AddValidationError("", validationResult.ErrorMessage);
                }
                    
            }
        }
    }

    public interface IRequestValidator
    {
        void Validate(object[] inputs);
    }

    public class AnnotationRequestValidator : IRequestValidator
    {
        public void Validate(object[] inputs)
        {
            foreach (var input in inputs)
            {
                Validator.TryValidateObject(input, new ValidationContext(input), ModelState.Current.Errors);
            }
        }
    }

    public class AnnotationAndValidatableRequestValidator : IRequestValidator
    {
        public void Validate(object[] inputs)
        {
            foreach (var input in inputs)
            {
                var context = new ValidationContext(input);
                Validator.TryValidateObject(input, context, ModelState.Current.Errors);
                var validatable = input as IValidatableObject;
                if (validatable == null) continue;
                foreach (var error in validatable.Validate(context))
                {
                    ModelState.Current.Errors.Add(error);
                }
            }
        }
    }
}
