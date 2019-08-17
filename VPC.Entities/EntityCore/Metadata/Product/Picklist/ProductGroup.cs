using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;

namespace VPC.Entities.EntityCore.Metadata.Product.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] {Operations.Create,Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Product group")]
    [PluralName("Product groups")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    [Standard]
    public class ProductGroup : SimplePicklist //HierarchyPicklist<ProductGroup>
    {
        [DefaultValue("10030")]
        [ColumnName("[PickListId]")]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.ProductGroup);

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [BasicColumn]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [BasicColumn]
        [NotNull]
        [DisplayName("Key")]
        
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        [BasicColumn]
        [DisplayName("Text")]
        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        [DisplayName("Updated by")]
        //[DynamicPrefix(InfoPrefix.ProductGroup_UpdatedBy)]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        [DisplayName("Updated date")]
        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [DisplayName("Active")]
        //[DynamicPrefix(InfoPrefix.ProductGroup_Active)]
        public PickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        [DisplayName("Is deleted")]
        public BooleanType IsDeleted { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        [DisplayName("Flagged")]
        public BooleanType Flagged { get; set; }


        // [InverseProperty("[Id]")]
        // [ColumnName("[ParentId]")]
        // [BasicColumn]
        // public override InternalId ParentId { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[ParentId]")]
        // [HierarchyDisplay("OnlyParents")]
        // //[DynamicPrefix(InfoPrefix.ProductGroup_ParentPicklist)]
        // public override PickList<ProductGroup> ParentPicklist { get; set; }
    }
}
