using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata.IntersectionEntity
{
    [TableProperties("[dbo].[UserDepartment]", "[Id]")]
    [DisplayName("User department")]
    [PluralName("User departments")]
    public class UserDepartment : IntersectEntity
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

        public override Name Name { get; set; }

        [DefaultValue(InfoType.UserDepartment)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserDepartment);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[UserId]")]
        [NotNull]
        [ForeignKey("[dbo].[User]", "[Id]")]
        //[DynamicPrefix(InfoPrefix.User_UserDepartment)]
        public Lookup<User> UserId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[DepartmentId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Department_UserDepartment)]
        public PickList<Department> DepartmentId { get; set; }
    }
}
