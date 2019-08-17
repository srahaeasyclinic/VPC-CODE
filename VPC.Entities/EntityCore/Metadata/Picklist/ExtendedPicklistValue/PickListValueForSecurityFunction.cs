using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForSecurityFunction]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for security function")]
    [PluralName("Pick list value for security functions")]
    [CascadeDelete]
    public class PickListValueForSecurityFunction : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(0);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ForeignKey("[dbo].[PickListValue]", "[Id]")]
        [ColumnName("[PickListValueId]")]
        [NotNull]
        [DisplayName("Picklist value")]
        public InternalId PicklistValueId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[scopeEntityId]")]  
        [NotNull]
        [DisplayName("Scope entity")]
        public NumericType scopeEntityId { get; set; }

        
    }
}
