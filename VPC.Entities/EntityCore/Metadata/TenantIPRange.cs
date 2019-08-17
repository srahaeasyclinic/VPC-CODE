using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
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
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

       
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [ColumnName("[Id]")]
        [NotNull]
         [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.TenantIpRange);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10008-ST01", "Standard" } };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

      
        
        [ColumnName("[ParentId]")]
        [NotNull]
        [ForeignKey("[dbo].[Tenant]", "[Id]")]
        [DisplayName("Parent Id")]
        public InternalId ParentId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [FreeTextSearch]
        [ColumnName("[StartIP]")]
        [DisplayName("Start IP")]
        public MediumText StartIP { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [FreeTextSearch]
        [ColumnName("[EndIP]")]
        [DisplayName("End IP")]
        public MediumText EndIP { get; set; }
    }
}