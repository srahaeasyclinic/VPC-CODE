using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForCity]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for city")]
    [PluralName("Pick list value for cities")]
    [CascadeDelete]
    
    public class PickListValueForCity : ExtendedPicklist
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
        [Broadcaster(MessageQueuingType.CountryMunicipality)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[CountryId]")] //one to one relation
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Country_PVCity)]
        [DisplayName("Country")]
        public PickList<Country> CountryId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("CountryId", MessageQueuingType.CountryState)]
        [Broadcaster(MessageQueuingType.StateMunicipality)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[StateId]")] //one to one relation
        //[DynamicPrefix(InfoPrefix.State_PVCity)]
        [DisplayName("State")]
        public PickList<State> StateId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("CountryId", MessageQueuingType.CountryState)]
        [Receiver("StateId", MessageQueuingType.StateMunicipality)]
        // [Dependency(new string[] {"[CountryId]", "[StateId]"})] 
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[MunicipalityId]")] //one to one relation
        //[DynamicPrefix(InfoPrefix.Municipality_PVCity)]
        [DisplayName("Municipality")]
        public PickList<Municipality> MunicipalityId { get; set; }

    }
}