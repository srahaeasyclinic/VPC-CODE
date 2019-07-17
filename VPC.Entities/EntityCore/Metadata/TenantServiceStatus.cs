using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[TenantServiceStatus]", "[Id]")]
    [DisplayName("Tenant service status")]
    [PluralName("Tenant service status")]
    [CascadeDelete]
    public class TenantServiceStatus : ExtendedEntity
    {
        
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]       
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout(1,3)]
        [ColumnName("[Status]")]       
        [Tagable]
        public NumericType Status { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Version]")]
        [Tagable]
        public SmallText Version { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Command]")]
        public NumericType Command { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Param]")]
        public MediumText  Parameter { get; set; }
    }
}
