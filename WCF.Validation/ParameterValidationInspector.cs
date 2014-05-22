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
            var result = returnValue as ResponseBase;
            if (result != null)
            {
                result.Errors.AddRange(ModelState.Current.Errors.SelectMany(t => t.MemberNames.Select(m => new ValidationError
                {
                    MemberName = m,
                    Message = t.ErrorMessage,
                })));
            }
        }
    }
}
