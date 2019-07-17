using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata {

    [TableProperties("[dbo].[Address]", "[Id]")]
    [CascadeDelete]
    public class PostalAddress : ComplexBase
    {
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [ForeignKey("[dbo].[User]", "[PostalAddressId]")]
        [ColumnName("[Id]")]
        [NotNull]
        public InternalId InternalId { get; set; }

        [ColumnName("[AddressLine1]")]
        [NotNull]
        public MediumText AddressLine1 { get; set; }

        [ColumnName("[AddressType]")]
        public XSmallText AddressType { get; set; }

        public PostalAddress()
        {
            base.ControlType = ControlType.Address;
        }
    
    }
}