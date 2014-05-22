using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace WCF.Validation
{
    public class ParameterValidationInspector : IParameterInspector
    {
        public object BeforeCall(string operationName, object[] inputs)
        {
            OperationContext.Current.Extensions.Add(new ModelState());

            //Assuming that we are using Request/Response objects
            foreach (var input in inputs)
            {
                var context = new ValidationContext(input);
                Validator.TryValidateObject(input, context, ModelState.Current.Errors, true);
                var validatable = input as IValidatableObject;
                if (validatable != null)
                {
                    foreach (var error in validatable.Validate(context))
                    {
                        ModelState.Current.Errors.Add(error);
                    }
                }
            }
            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            var result = returnValue as ResponseBase;
            if (result != null)
            {
                foreach (var validationResult in ModelState.Current.Errors)
                {
                    if (validationResult.MemberNames.Any())
                    {
                        foreach (var memberName in validationResult.MemberNames)
                        {
                            result.Errors.Add(new ValidationError
                            {
                                MemberName = memberName,
                                Message = validationResult.ErrorMessage,
                            });
                        }
                    }
                    else
                    {
                        result.Errors.Add(new ValidationError
                        {
                            MemberName = "",
                            Message = validationResult.ErrorMessage,
                        });
                    }
                }
            }
        }
    }
}
