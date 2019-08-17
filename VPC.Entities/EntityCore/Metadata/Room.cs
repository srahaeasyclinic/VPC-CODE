using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Room]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Room")]
    [PluralName("Rooms")]
    public class Room : PrimaryEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Room)]
         [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Room);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            { "EN10007-ST01", "Standard" }
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NotNull]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[LocationId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.LocationId_Room)]
        [DisplayName("Location")]
        public Lookup<Location> LocationId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[RoomNo]")]
        [DisplayName("Room no")]
        public NumericType RoomNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[RoomTypeId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.RoomTypeId_Room)]
        [DisplayName("Room type")]
        public PickList<RoomType> RoomTypeId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Floor]")]
        [DisplayName("Floor")]
        public NumericType Floor { get; set; }
    }
}