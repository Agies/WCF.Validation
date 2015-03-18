using System.ComponentModel.DataAnnotations;

namespace WCF.Validation
{
    public class AnnotationRequestValidator : IRequestValidator
    {
        public virtual void Validate(object[] inputs)
        {
            foreach (var input in inputs)
            {
                var context = new ValidationContext(input);
                OnValidate(input, context);
            }
        }

        protected virtual void OnValidate(object input, ValidationContext context)
        {
            Validator.TryValidateObject(input, context, ModelState.Current.Errors, true);
        }
    }
}