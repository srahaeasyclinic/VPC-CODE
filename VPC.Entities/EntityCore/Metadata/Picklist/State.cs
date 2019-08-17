using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;
using VPC.Entities.EntityCore.Model.Storage;


namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("State")]
    [PluralName("States")]
    [CustomizeValue()]
    [Standard]
    public class State : ComplexPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("20005")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        [NotNull]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.State);

        [NonQueryable]
        [DisplayName("Name")]

        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        [DisplayName("Key")]

        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        [DisplayName("Text")]

        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [InverseProperty("[Id]")]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        [DisplayName("Updated by")]

        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        [DisplayName("Updated date")]

        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [DisplayName("Active")]

        //[DynamicPrefix(InfoPrefix.Active_State)]
        public PickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [DisplayName("Is deleted")]

        public BooleanType IsDeletetd { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        [DisplayName("Flagged")]

        public BooleanType Flagged { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        public PicklistValueForState PickListValueForState { get; set; }
    }
}