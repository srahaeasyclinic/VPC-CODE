using System;
using System.Data;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.WorkFlow.Engine.Email
{
    [DisplayName("Email workflow")]
    [PluralName("Email workflows")]
    [FixedValue]
    public  class EmailWorkFlow: SimplePicklist
    {
        [NonQueryable]
       
        [NotNull]
        public override InternalId TenantId { get; set; }

        [BasicColumn]
        [NonQueryable]
       
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Active);

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListWorkFlowData(InfoType.Email);
            return PickListHelper.GetValues(lists);
        }
    }
}
