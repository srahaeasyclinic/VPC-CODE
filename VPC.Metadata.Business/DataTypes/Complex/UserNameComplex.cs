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
        [DisplayName("First name")]
        public Name FirstName { get; set; }

        [ApplicableForFilter]
        [ColumnName("PAI_MName")]
        [DisplayName("Middle name")]
        public Name MiddleName { get; set; }

        [ApplicableForFilter]
        [FreeTextSearchAttribute]
        [AdvanceSearchAttribute]
        [ColumnName("PAI_LName")]
        [DisplayName("Last name")]
        public Name LastName { get; set; }

        [ApplicableForFilter]
        [FreeTextSearchAttribute]
        [AdvanceSearchAttribute]
        [ColumnName("PAI_FullName")]
        [DisplayName("Full name")]

        public Name FullName { get; set; }

        public UserNameComplex()
        {
            //ControlType = ControlTypes.Name;
        }
    }
}
