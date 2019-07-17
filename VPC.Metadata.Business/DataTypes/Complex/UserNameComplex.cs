using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    public class UserNameComplex : ComplexBase
    {
        [ApplicableForFilter]
        [FreeTextSearchAttribute]
        [AdvanceSearchAttribute]
        [ColumnName("PAI_FName")]
        public Name FirstName { get; set; }

        [ApplicableForFilter]
        [ColumnName("PAI_MName")]
        public Name MiddleName { get; set; }

        [ApplicableForFilter]
        [FreeTextSearchAttribute]
        [AdvanceSearchAttribute]
        [ColumnName("PAI_LName")]
        public Name LastName { get; set; }

        [ApplicableForFilter]
        [FreeTextSearchAttribute]
        [AdvanceSearchAttribute]
        [ColumnName("PAI_FullName")]
        public Name FullName { get; set; }

        public UserNameComplex()
        {
            //ControlType = ControlTypes.Name;
        }
    }
}
