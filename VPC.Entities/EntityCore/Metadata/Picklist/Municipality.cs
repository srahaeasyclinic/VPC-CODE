﻿using System;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
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
    [DisplayName("Municipality")]
    [PluralName("Municipalities")]
    [CustomizeValue()]
    [Standard]
    public class Municipality : ComplexPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("20007")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        [NotNull]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Municipality);

        [NonQueryable]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        public MediumText Text { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [InverseProperty("[Id]")]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue()]
        [NonQueryable]
        [ColumnName("[UpdatedDate]")]
        [NotNull]
        public DateTime UpdatedDate { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("1")]
        [ColumnName("[Active]")]
        [NotNull]
        [SimpleSearch]
        //[DynamicPrefix(InfoPrefix.Active_Municipality)]
        public LocalPickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsDeletetd]")]
        [NotNull]
        public BooleanType IsDeletetd { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[Flagged]")]
        [NotNull]
        public BooleanType Flagged { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        public PickListValueForMunicipality PickListValueForMunicipality { get; set; }
    }
}