namespace WCF.Validation
{
    public interface IHaveValidationErrors
    {
        void AddValidationError(string memberName, string errorMessage);
    }
}