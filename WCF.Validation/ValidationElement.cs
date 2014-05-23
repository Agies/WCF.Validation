using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.ServiceModel.Configuration;

namespace WCF.Validation
{
    /// <summary>
    /// <example>
    /// <system.serviceModel>
    ///     <extensions>
    ///         <behaviorExtensions>
    ///             <add name="Validator" type="WCF.Validation.ValidationElement, WCF.Validation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
    ///         </behaviorExtensions>
    ///     </extensions>
    /// <system.serviceModel>
    /// </example>
    /// </summary>
    public class ValidationElement : BehaviorExtensionElement
    {
        private const string requestValidator = "requestValidator";
        
        [ConfigurationProperty(requestValidator)]
        public string RequestValidator
        {
            get { return (string)base[requestValidator]; }
            set { base[requestValidator] = requestValidator; }
        }

        protected override object CreateBehavior()
        {
            IRequestValidator validator;
            if (!string.IsNullOrWhiteSpace(RequestValidator))
            {
                validator = Activator.CreateInstance(Type.GetType(RequestValidator)) as IRequestValidator;
            }
            else
            {
                validator = new AnnotationAndValidatableRequestValidator();
            }

            return new ValidationBehavior(validator);
        }

        public override Type BehaviorType
        {
            get { return typeof(ValidationBehavior); }
        }
    }
}