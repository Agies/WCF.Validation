using System;
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
        protected override object CreateBehavior()
        {
            return new ValidationBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(ValidationBehavior); }
        }
    }
}