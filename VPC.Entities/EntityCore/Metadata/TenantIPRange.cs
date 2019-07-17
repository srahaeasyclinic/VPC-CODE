using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[TenantIPRange]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Tenant IP range")]
    [PluralName("Tenant IP ranges")]
    public class TenantIPRange : DetailEntity, IItem<Item>
    {
        
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

       
        [AccessibleLayout(1, 3)]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        
        public override Name Name { get; set; }

        public override EntityContext EntityContext => new EntityContext(InfoType.TenantIpRange);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10008", "Standard" } };

        [AccessibleLayout(1, 3)]
        public override XSmallText SubType { get; set; }

      
        
        [ColumnName("[ParentId]")]
        [NotNull]
        [ForeignKey("[dbo].[Tenant]", "[Id]")]
        public InternalId ParentId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [FreeTextSearch]
        [ColumnName("[StartIP]")]
        public MediumText StartIP { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [FreeTextSearch]
        [ColumnName("[EndIP]")]
        public MediumText EndIP { get; set; }
    }
}