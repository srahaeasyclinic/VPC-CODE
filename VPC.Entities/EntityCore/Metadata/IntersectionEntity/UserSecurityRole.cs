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
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }
        public Lookup<User> UserId { get; set; }
        public Lookup<SecurityRole> RoleId { get; set; }
    }

    public class SecurityRole
    {

    }
}
