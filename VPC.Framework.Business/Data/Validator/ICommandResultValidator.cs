namespace VPC.Framework.Business.Data.Validator
{
    public interface ICommandResultValidator
    {
        bool IsValid(int resultValue);
    }
}