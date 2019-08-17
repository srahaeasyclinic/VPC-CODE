using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[UserInRole]", "[Id]")]
    [DisplayName("User in role")]
    [PluralName("User in roles")]
    [Import(false)]
    [Export(false)]
    public class  UserInRole : IntersectEntity
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

        [DefaultValue(InfoType.UserInRole)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserInRole);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[UserId]")]
        [NotNull]
        [ForeignKey("[dbo].[User]", "[Id]")]
         [DisplayName("User")]
        public Lookup<User> User { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[RoleId]")]
        [NotNull]
        [ForeignKey("[dbo].[Role]", "[Id]")]
        [DisplayName("Role")]
        public Lookup<Role> Role { get; set; }

    }
}