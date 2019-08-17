using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata.IntersectionEntity
{
    [TableProperties("[dbo].[UserLocation]", "[Id]")]
    [DisplayName("User location")]
    [PluralName("User locations")]
    public class UserLocation : IntersectEntity
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

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.UserLocation)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserLocation);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[UserId]")]
        [NotNull]
        [ForeignKey("[dbo].[User]", "[Id]")]
        //[DynamicPrefix(InfoPrefix.User_UserLocation)]
        [DisplayName("User Id")]
        public Lookup<User> UserId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]     
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[LocationId]")]
        [NotNull]
        [ForeignKey("[dbo].[Location]", "[Id]")]
        //[DynamicPrefix(InfoPrefix.Location_UserLocation)]
        [DisplayName("Location Id")]
        public Lookup<Location> LocationId { get; set; }
    }
}
