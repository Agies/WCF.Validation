namespace WCF.Validation
{
    public class OperationContextModelStateProvider : IModelStateProvider
    {
        public IModelState Instance
        {
            get { return ModelState.Current; }
        }
    }

    public interface IModelStateProvider
    {
        IModelState Instance { get; }
    }
}