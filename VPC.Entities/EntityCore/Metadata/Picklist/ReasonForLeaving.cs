using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;
 
namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Reason for leaving")]
    [PluralName("Reasons for leaving")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    [Standard]
    public class ReasonForLeaving : SimplePicklist
    {
        [DefaultValue("10045")]
        [ColumnName("[PickListId]")]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.ReasonForLeaving);

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]

        public override Name Name { get; set; }

        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        
        [DisplayName("Key")]

        public SmallText Key { get; set; }

        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        
        [DisplayName("Text")]

        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
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

        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        
        [DisplayName("Active")]

        public PickList<Active> Active { get; set; }

        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
       
        [DisplayName("Is deleted")]

        public BooleanType IsDeletetd { get; set; }

        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        
        [DisplayName("Flagged")]

        public BooleanType Flagged { get; set; }
    }
}
