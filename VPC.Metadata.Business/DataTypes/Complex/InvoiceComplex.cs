using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    public class InvoiceComlpex: ComplexBase
    {
        public NumericType ReceiptCount { get; set; }
        public NumericType RefundCount { get; set; }
        public NumericType CreditNoteCount { get; set; }
        public NumericType InvoiceCount { get; set; }

        public InvoiceComlpex()
        {
            //ControlType = ControlTypes.Invoice;
        }
    }
}
