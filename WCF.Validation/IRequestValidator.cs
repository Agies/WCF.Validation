namespace WCF.Validation
{
    public interface IRequestValidator
    {
        void Validate(object[] inputs);
    }
}