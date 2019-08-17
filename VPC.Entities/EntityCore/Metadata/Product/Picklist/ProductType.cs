﻿using System;
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
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus })]
    [DisplayName("Product type")]
    [PluralName("Product types")]
    [SupportWorkflow(false)]
    [CustomizeValue()]
    [Standard]
    public class ProductType : SimplePicklist //HierarchyPicklist<ProductType>
    {
        [DefaultValue("10031")]
        [ColumnName("[PickListId]")]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.ProductType);

        [NonQueryable]

        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [BasicColumn]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]

        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        [BasicColumn]
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
        [DisplayName("Updated by")]

        [NotNull]
        //[DynamicPrefix(InfoPrefix.ProductType_UpdatedBy)]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [DisplayName("Updated date")]

        [NotNull]
        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        [DisplayName("Active")]

        //[DynamicPrefix(InfoPrefix.ProductType_Active)]
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
        // [ColumnName("[ParentId]")]
        // [InverseProperty("[Id]")]
        // [HierarchyDisplay("OnlyParents")]
        // //[DynamicPrefix(InfoPrefix.ProductType_ParentPicklist)]
        // public override PickList<ProductType> ParentPicklist { get; set; }
    }
}
