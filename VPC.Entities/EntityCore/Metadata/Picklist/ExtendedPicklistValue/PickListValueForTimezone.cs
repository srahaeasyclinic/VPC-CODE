﻿using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForTimeZone]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for timezone")]
    [PluralName("Pick list value for timezone")]
    [CascadeDelete]
    public class PickListValueForTimeZone : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(0);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ForeignKey("[dbo].[PickListValue]", "[Id]")]
        [ColumnName("[PickListValueId]")]
        [NotNull]
        [DisplayName("Picklist value")]
        public InternalId PicklistValueId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[GmtDeviation]")]
        [NotNull]
        [DisplayName("GMT deviation")]
        public DecimalType GmtDeviation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SummerTimeStart]")]
        [DisplayName("Summer time start")]
        public XLargeText SummerTimeStart { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[WinterTimeStart]")]
        [DisplayName("Winter time start")]
        public XLargeText WinterTimeStart { get; set; }
    }
}