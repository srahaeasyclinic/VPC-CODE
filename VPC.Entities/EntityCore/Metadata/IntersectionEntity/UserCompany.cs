using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata.IntersectionEntity
{
    [TableProperties("[dbo].[UserCompany]", "[Id]")]
    [DisplayName("User company")]
    [PluralName("User companies")]
    public class UserCompany : IntersectEntity
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

        [DefaultValue(InfoType.UserCompany)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserCompany);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[UserId]")]
        [NotNull]
        [ForeignKey("[dbo].[User]", "[Id]")]
        //[DynamicPrefix(InfoPrefix.User_UserCompany)]
        [DisplayName("User Id")]
        public Lookup<User> UserId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[CompanyId]")]
        [NotNull]
        [ForeignKey("[dbo].[Company]", "[Id]")]
        //[DynamicPrefix(InfoPrefix.Company_UserCompany)]
        [DisplayName("Company Id")]
        public Lookup<Company> CompanyId { get; set; }
    }
}
