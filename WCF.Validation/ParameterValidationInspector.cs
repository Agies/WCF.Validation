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
}
