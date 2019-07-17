using System;
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
    [DisplayName("Rule type")]
    [PluralName("Rule types")]
    [FixedValue]
    public class RuleType : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [BasicColumn]
        [DefaultValue("10040")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.RuleType);

        [AccessibleLayout(1, 3)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [NonQueryable]
        [ColumnName("[Name]")]
        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(RuleTypeEnum));
            return PickListHelper.GetValues(lists);

        }
    }
    public enum RuleTypeEnum
    {
        Visibility = 1

    }
    //public class RuleType : ComplexPicklist
    //{
    //    [DefaultValue("10031")]
    //    [ColumnName("[PickListId]")]
    //    public override PicklistContext PicklistContext => new PicklistContext(PicklistType.RuleType);

    //    [NonQueryable]
    //    [ColumnName("[TenantId]")]
    //    [NotNull]
    //    public InternalId TenantId { get; set; }

    //    [NonQueryable]
    //    [ColumnName("[Id]")]
    //    [NotNull]
    //    public override InternalId InternalId { get; set; }

    //    [NonQueryable]
    //    public override Name Name { get; set; }

    //    [ColumnName("[Key]")]
    //    [FreeTextSearch]
    //    [NotNull]
    //    public SmallText Key { get; set; }

    //    [ColumnName("[Text]")]
    //    [FreeTextSearch]
    //    [NotNull]
    //    public MediumText Text { get; set; }

    //    [DefaultValue()]
    //    [NonQueryable]
    //    [ColumnName("[UpdatedBy]")]
    //    [NotNull]
    //    public Lookup<User> UpdatedBy { get; set; }

    //    [DefaultValue()]
    //    [NonQueryable]
    //    [ColumnName("[UpdatedDate]")]
    //    [NotNull]
    //    public DateTime UpdatedDate { get; set; }

    //    [DefaultValue("1")]
    //    [ColumnName("[Active]")]
    //    [NotNull]
    //    [SimpleSearch]
    //    public PickList<Active> Active { get; set; }

    //    [DefaultValue("0")]
    //    [ColumnName("[IsDeletetd]")]
    //    [NotNull]
    //    public BooleanType IsDeletetd { get; set; }

    //    [DefaultValue("0")]
    //    [ColumnName("[Flagged]")]
    //    [NotNull]
    //    public BooleanType Flagged { get; set; }
    //}
}
