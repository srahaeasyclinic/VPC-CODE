using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Bank account type")]
    [PluralName("Bank account types")]
    [Standard]
    public class BankAccountType : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }
        public override InternalId InternalId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.BankAccountType);
        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(BankAccountEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum BankAccountEnum
    {
        Savings = 1,
        Current = 2
    }
}