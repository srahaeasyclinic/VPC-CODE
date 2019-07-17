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
    [DisplayName("Subscription category")]
    [PluralName("Subscription categories")]
    [SupportWorkflow(false)]
    [Standard]
    public class SubscriptionCategory : SimplePicklist
    {
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.SubscriptionCategory);

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [NonQueryable]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        public string Descriptions => "Subscription category!";

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(SubscriptionCategoryEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum SubscriptionCategoryEnum
    {

    }
}
