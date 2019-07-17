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
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Room)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Room);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            { "EN10007-ST01", "Standard" }
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NotNull]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[LocationId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.LocationId_Room)]
        public Lookup<Location> LocationId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[RoomNo]")]
        public NumericType RoomNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[RoomTypeId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.RoomTypeId_Room)]
        public PickList<RoomType> RoomTypeId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Floor]")]
        public NumericType Floor { get; set; }
    }
}