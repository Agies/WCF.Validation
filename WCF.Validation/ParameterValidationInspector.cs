using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
                Validator.TryValidateObject(input, new ValidationContext(input), ModelState.Current.Errors);
            }
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
