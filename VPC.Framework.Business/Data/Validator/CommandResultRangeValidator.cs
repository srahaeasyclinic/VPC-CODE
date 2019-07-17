namespace VPC.Framework.Business.Data.Validator
{
    public class CommandResultRangeValidator : ICommandResultValidator
    {
        private readonly int[] _validResults;

        public CommandResultRangeValidator(int validResult)
        {
            _validResults = new[] {validResult};
        }

        public CommandResultRangeValidator(int validResultA, int validResultB)
        {
            _validResults = new[] {validResultA, validResultB};
        }

        public CommandResultRangeValidator(params int[] validResults)
        {
            _validResults = validResults;
        }

        #region ICommandResultValidator Members

        public bool IsValid(int resultValue)
        {
            foreach (int valid in _validResults)
            {
                if (valid == resultValue)
                    return true;
            }
            return false;
        }

        #endregion
    }
}