using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    public class VisitComplex : ComplexBase
    {
        public DataTypes.DateTime LastVisitDate { get; set; }
        public StringType LastVisitLocation { get; set; }
        public BooleanType PreviousVisitOverduePresent { get; set; }
        public NumericType VisitCount { get; set; }
        public BooleanType HasCurrentVisitManagePermission { get; set; }

        public VisitComplex()
        {
            //ControlType = ControlTypes.Visit;
        }
    }
}
