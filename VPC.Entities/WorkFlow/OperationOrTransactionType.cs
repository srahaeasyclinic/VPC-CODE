
using System.ComponentModel;

namespace VPC.Entities.WorkFlow
{
    public enum OperationOrTransactionType
    {
        [Description("Operation")]
        Operation = 1,

        [Description("Transaction")]
        Transaction = 2,
    }

}
