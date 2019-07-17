using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata {

    [TableProperties("[dbo].[Address]", "[Id]")]
    [CascadeDelete]
    public class InvoiceAddress : ComplexBase
    {
        [NonQueryableAttribute]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }


        [ForeignKey("[dbo].[User]", "[InvoiceAddressId]")]
        [ColumnName("[Id]")]
        [NotNull]
        [NonQueryableAttribute]
        public InternalId InternalId { get; set; }

        [Tagable]
        [ColumnName("[AddressLine1]")]
        [NotNull]
        public MediumText AddressLine1 { get; set; }

        
        [ColumnName("[AddressType]")]
        public XSmallText AddressType { get; set; }

        public InvoiceAddress()
        {
            base.ControlType = ControlType.Address;
        }
    }
}