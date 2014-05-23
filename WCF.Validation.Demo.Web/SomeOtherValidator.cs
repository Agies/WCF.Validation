using System.ComponentModel.DataAnnotations;

namespace WCF.Validation.Demo.Web
{
    public class SomeOtherValidator : AnnotationAndValidatableRequestValidator
    {
        protected override void OnValidate(object input, ValidationContext context)
        {
            base.OnValidate(input, context);
            ModelState.Current.AddModelError("TheOtherOne", "I'm hidden");
        }
    }
}