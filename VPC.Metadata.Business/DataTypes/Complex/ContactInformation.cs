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
        public InternalId TenantId { get; set; }

        [ColumnName("[Id]")]
        [NotNull]    
        public InternalId InternalId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalPhone1]")]
        public Phone PersonalPhone1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalPhone2]")]
        public Phone PersonalPhone2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalMobile1]")]
        public Phone PersonalMobile1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalEmail1]")]
        public Email PersonalEmail1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PersonalEmail2]")]
        public Email PersonalEmail2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhone1]")]
        public Phone WorkPhone1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhone2]")]
        public Phone WorkPhone2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkPhoneExtension]")]
        public XSmallText WorkPhoneExtension { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkMobile1]")]
        public Phone WorkMobile1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkFax1]")]
        public Phone WorkFax1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[WorkEmail1]")]
        public Email WorkEmail1 { get; set; }

        public ContactInformation()
        {
            base.ControlType = ControlType.ContactInformation;
        }
    }
}
