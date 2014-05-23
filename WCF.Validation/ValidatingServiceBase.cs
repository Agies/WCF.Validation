namespace WCF.Validation
{
    public class ValidatingServiceBase
    {
        public ModelState ModelState
        {
            get { return ModelState.Current; }
        }
    }
}