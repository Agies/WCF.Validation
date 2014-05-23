using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml.Serialization;
using WCF.Validation.Contracts;

namespace WCF.Validation
{
    public class ParameterValidationInspector : IParameterInspector
    {
        private readonly IRequestValidator _requestValidator;
        public const string ErrorHeader = "Errors";

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
            if (result == null)
            {
                if (OperationContext.Current.OutgoingMessageHeaders.MessageVersion.Envelope == EnvelopeVersion.None)
                    return;
                var errors = new List<ValidationError>();
                foreach (var validationResult in ModelState.Current.Errors)
                {
                    if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                    {
                        errors.AddRange(validationResult.MemberNames.Select(memberName => new ValidationError(memberName, validationResult.ErrorMessage)));
                    }
                    else
                    {
                        errors.Add(new ValidationError("", validationResult.ErrorMessage));
                    }

                }
                var messageHeader = MessageHeader.CreateHeader(ErrorHeader, "http://WCF.Validation", errors);
                OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);
            }
            else
            {
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
            OperationContext.Current.Extensions.Remove(ModelState.Current);
        }
    }
}
