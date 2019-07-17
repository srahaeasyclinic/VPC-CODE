using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("Recurring unit")]
    [PluralName("Recurring units")]
    [SupportWorkflow(false)]
    public class RecurringUnit : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.RecurringUnit);

        public override InternalId InternalId { get; set; }
        public override Name Name { get; set; }
        public string Descriptions => "Recurring unit!";

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(RecurringUnitEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum RecurringUnitEnum
    {

         Weekly = 1,


         Monthly = 2,


         Yearly = 3,
    }
}
