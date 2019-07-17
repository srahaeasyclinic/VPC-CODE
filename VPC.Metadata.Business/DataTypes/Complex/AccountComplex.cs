using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    public class AccountComplex : ComplexBase
    {
        public AnnualIncome CorporateDueAmount { get; set; }
        public AnnualIncome Balance { get; set; }
        public DecimalType Matched { get; set; }
        public DecimalType UnMatched { get; set; }
        public BooleanType AllowCredit { get; set; }
        //public UnitBalanceInfo UnitBalance { get; set; }
        public DecimalType AccountBalances { get; set; }
        //public AuditDetail AuditDetail { get; set; }
        public DecimalType TotalDueAmount { get; set; }
        public DecimalType PatientDueAmount { get; set; }
        public DecimalType InsuranceDueAmount { get; set; }


        public AccountComplex()
        {
            //ControlType = ControlTypes.AccountBalance;
        }
    }
}
