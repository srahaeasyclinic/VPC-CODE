using System;
using System.Data;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.WorkFlow.Engine.User
{
    [DisplayName("User workflow")]
    [PluralName("User workflows")]
    [FixedValue]
    public class UserWorkFlow : SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override InternalId InternalId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Active);

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListWorkFlowData(InfoType.User);
            return PickListHelper.GetValues(lists);
        }
    }
}
