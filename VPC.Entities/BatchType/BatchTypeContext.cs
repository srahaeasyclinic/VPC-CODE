
using System;
using System.ComponentModel;
using System.Data;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;

namespace VPC.Entities.BatchType
{
  public  class BatchTypeContext: SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.BatchTypeContext);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(BatchTypeContextEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum BatchTypeContextEnum
    {
        Email = 1,
        SMS = 2,
        ExportUser = 3,
        ActiveInactiveProduct=4
    }
}