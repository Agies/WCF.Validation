namespace WCF.Validation
{
    public class ValidatingServiceBase
    {
        private readonly IModelState _modelState;

        public ValidatingServiceBase(IModelState modelState)
        {
            _modelState = modelState;
        }

        public IModelState ModelState
        {
            get { return _modelState; }
        }
    }
}