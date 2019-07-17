using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Rules;

namespace VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue
{
    [TableProperties("[dbo].[PickListValueForMunicipality]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("Pick list value for municipality")]
    [PluralName("Pick list value for municipalities")]
    [CascadeDelete]
    public class PickListValueForMunicipality : ExtendedPicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }
        public override PicklistContext PicklistContext => new PicklistContext(0);

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
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
        //[DynamicPrefix(InfoPrefix.Municipality_State)]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.CountryId_Municipality)]
        public PickList<Country> CountryId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("CountryId", MessageQueuingType.CountryState)]
        [Broadcaster(MessageQueuingType.StateMunicipality)]
        [InverseProperty("[Id]")] //situated in currency table...
        [ColumnName("[StateId]")] //one to one relation
        //[DynamicPrefix(InfoPrefix.StateId_Municipality)]
        public PickList<State> StateId { get; set; }




    }
}