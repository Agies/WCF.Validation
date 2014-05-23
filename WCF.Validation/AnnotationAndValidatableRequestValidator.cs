using System.ComponentModel.DataAnnotations;

namespace WCF.Validation
{
    public class AnnotationAndValidatableRequestValidator : AnnotationRequestValidator
    {
        protected override void OnValidate(object input, ValidationContext context)
        {
            base.OnValidate(input, context);
            var validatable = input as IValidatableObject;
            if (validatable == null) return;
            foreach (var error in validatable.Validate(context))
            {
                ModelState.Current.Errors.Add(error);
            }
        }
    }
}