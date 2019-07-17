using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.WorkFlow.Engine.Product
{
    [DisplayName("Product workflow")]
    [PluralName("Product workflows")]
    [FixedValue]
    public class ProductWorkFlow: SimplePicklist
    {
        // [NonQueryable]
        // [ColumnName("[TenantId]")]
        // [NotNull]
        public override InternalId TenantId { get; set; }

        // [BasicColumn]
        // [NonQueryable]
        // [ColumnName("[Id]")]
        // [NotNull]
        public override InternalId InternalId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Active);

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListWorkFlowData(InfoType.Product);
            return PickListHelper.GetValues(lists);
        }
    }
}
