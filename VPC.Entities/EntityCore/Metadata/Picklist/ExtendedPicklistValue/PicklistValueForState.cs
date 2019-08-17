using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForState]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for state")]
    [PluralName("Pick list value for states")]
    [CascadeDelete]
    public class PicklistValueForState : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(0);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

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
        [Broadcaster(MessageQueuingType.CountryState)]
        [InverseProperty("[Id]")] //situated in contact table...
        [ColumnName("[CountryId]")] //TODO : need discussion
        [NotNull]
        //[DynamicPrefix(InfoPrefix.CountryId_State)]    
        [DisplayName("Country")]  
        public PickList<Country> CountryId { get; set; }

        

    }
}