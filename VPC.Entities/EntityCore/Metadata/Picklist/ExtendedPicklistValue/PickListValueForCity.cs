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
        public override InternalId TenantId { get; set; }

        public override PicklistContext PicklistContext => new PicklistContext(0);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ForeignKey("[dbo].[PickListValue]", "[Id]")]
        [ColumnName("[PickListValueId]")]
        [NotNull]
        public InternalId PicklistValueId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Broadcaster(MessageQueuingType.CountryState)]
        [Broadcaster(MessageQueuingType.CountryMunicipality)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[CountryId]")] //one to one relation
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Country_PVCity)]
        public PickList<Country> CountryId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("CountryId", MessageQueuingType.CountryState)]
        [Broadcaster(MessageQueuingType.StateMunicipality)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[StateId]")] //one to one relation
        //[DynamicPrefix(InfoPrefix.State_PVCity)]
        public PickList<State> StateId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("CountryId", MessageQueuingType.CountryMunicipality)]
        [Receiver("StateId", MessageQueuingType.StateMunicipality)]
        // [Dependency(new string[] {"[CountryId]", "[StateId]"})] 
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[MunicipalityId]")] //one to one relation
        //[DynamicPrefix(InfoPrefix.Municipality_PVCity)]
        public PickList<Municipality> MunicipalityId { get; set; }

    }
}