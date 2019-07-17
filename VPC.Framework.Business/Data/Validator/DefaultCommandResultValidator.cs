namespace VPC.Framework.Business.Data.Validator
{
    internal class DefaultCommandResultValidator : ICommandResultValidator
    {
        #region ICommandResultValidator Members

        public bool IsValid(int resultValue)
        {
            return resultValue == 0;
        }

        #endregion
    }
}