using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[CustomerCredential]", "[Id]")]
    [DisplayName("Customer credential")]
    [PluralName("Customer credentials")]
    [CascadeDelete]
    public class CustomerCredential : ExtendedEntity
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CustomerName]")]
        [Tagable]
        [NotNull]
        public SmallText CustomerName { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [Tagable]
        [ColumnName("[Password]")]
        [Receiver("Password", "EncriptPassword")]
        [NotNull]
        public Password Password { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SecurityQuestion1]")]
        public SmallText SecurityQuestion1 { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SecurityAnswer1]")]
        public SmallText SecurityAnswer1 { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SecurityQuestion2]")]
        public SmallText SecurityQuestion2 { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[SecurityAnswer2]")]
        public SmallText SecurityAnswer2 { get; set; }

        
    }
}