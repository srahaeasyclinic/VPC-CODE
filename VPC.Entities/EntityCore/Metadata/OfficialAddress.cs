using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata {

    [TableProperties("[dbo].[Address]", "[Id]")]
    [CascadeDelete]
    public class OfficialAddress : ComplexBase
    {
              [NonQueryableAttribute]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [ForeignKey("[dbo].[User]", "[OfficialAddressId]")]
        [ColumnName("[Id]")]
        [NotNull]
        public InternalId InternalId { get; set; }

        [ColumnName("[AddressLine1]")]
        [NotNull]
        public MediumText AddressLine1 { get; set; }

        [ColumnName("[AddressType]")]
        public XSmallText AddressType { get; set; }

        public OfficialAddress()
        {
            base.ControlType = ControlType.Address;
        }
    
    }
}