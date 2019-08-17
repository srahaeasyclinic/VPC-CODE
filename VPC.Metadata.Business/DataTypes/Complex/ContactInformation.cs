using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    [TableProperties("[dbo].[ContactInformation]", "[Id]")]
    [CascadeDelete]
    public class ContactInformation : ComplexBase
    {        
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public InternalId InternalId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalPhone1]")]
        [DisplayName("Personal phone 1")]
        public Phone PersonalPhone1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalPhone2]")]
        [DisplayName("Personal phone 2")]
        public Phone PersonalPhone2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalMobile1]")]
        [DisplayName("Personal mobile 1")]
        public Phone PersonalMobile1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalEmail1]")]
        [DisplayName("Personal email 1")]
        public Email PersonalEmail1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalEmail2]")]
        [DisplayName("Personal email 2")]
        public Email PersonalEmail2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhone1]")]
        [DisplayName("Work phone 1")]
        public Phone WorkPhone1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhone2]")]
        [DisplayName("Work phone 2")]
        public Phone WorkPhone2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhoneExtension]")]
        [DisplayName("Work phone extension")]
        public XSmallText WorkPhoneExtension { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkMobile1]")]
        [DisplayName("Work mobile 1")]
        public Phone WorkMobile1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkFax1]")]
        [DisplayName("Work fax 1")]
        public Phone WorkFax1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkEmail1]")]
        [DisplayName("Work email 1")]
        public Email WorkEmail1 { get; set; }

        public ContactInformation()
        {
            base.ControlType = ControlType.ContactInformation;
        }
    }
}
