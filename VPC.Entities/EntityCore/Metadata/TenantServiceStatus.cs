using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[TenantServiceStatus]", "[Id]")]
    [DisplayName("Tenant service status")]
    [PluralName("Tenant service status")]
    [CascadeDelete]
    public class TenantServiceStatus : ExtendedEntity
    {
        
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]       
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout((int)LayoutType.View,(int)LayoutType.List)]
        [ColumnName("[Status]")]       
        [Tagable]
        [DisplayName("Status")]
        public NumericType Status { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Version]")]
        [Tagable]
        [DisplayName("Version")]
        public SmallText Version { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Command]")]
        [DisplayName("Command")]
        public NumericType Command { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Param]")]
        [DisplayName("Parameter")]
        public MediumText  Parameter { get; set; }
    }
}
