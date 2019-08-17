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
    public  class EmailStatus: SimplePicklist
    {
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.EmailStatus);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(EmailEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum EmailEnum
    {
        ReadyToSend = 1,
        Send = 2,
        Fail = 3
    }

    
   
}