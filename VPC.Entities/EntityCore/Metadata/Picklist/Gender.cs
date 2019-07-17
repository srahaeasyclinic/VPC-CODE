using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
   
    public class Gender : SimplePicklist
    {
       
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Gender);

     
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(GenderEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum GenderEnum
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}