using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Role]", "[Id]")]

    [DisplayName("Role")]
    [PluralName("Roles")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    public class Role : PrimaryEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [ColumnName("[Name]")]
        [NotNull]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Role)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Role);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10004-ST01", "Standard"}
        };

        [AccessibleLayout(1, 3)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Type]")]
        public NumericType Type { get; set; }
    }
}