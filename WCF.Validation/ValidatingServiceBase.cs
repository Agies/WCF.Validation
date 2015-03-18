namespace WCF.Validation
{
    public class ValidatingServiceBase
    {
        private readonly IModelStateProvider _modelStateProvider;

        public ValidatingServiceBase(IModelStateProvider modelStateProvider)
        {
            _modelStateProvider = modelStateProvider;
        }

        public IModelState ModelState
        {
            get { return _modelStateProvider.Instance; }
        }
    }
}