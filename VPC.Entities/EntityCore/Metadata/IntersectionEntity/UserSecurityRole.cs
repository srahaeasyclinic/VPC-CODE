using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata.IntersectionEntity
{
    [TableProperties("[dbo].[UserSecurityRole]", "[Id]")]
    [DisplayName("User security role")]
    [PluralName("User security roles")]
    public class UserSecurityRole : IntersectEntity
    {
        public override Name Name { get; set; }

        [DefaultValue(InfoType.UserSecurityRole)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserSecurityRole);

        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }
        [DisplayName("User Id")]
        public Lookup<User> UserId { get; set; }
        [DisplayName("Role Id")]
        public Lookup<SecurityRole> RoleId { get; set; }
    }

    public class SecurityRole
    {

    }
}
