using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Location]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Location")]
    [PluralName("Locations")]
    public class Location : PrimaryEntity, IItem<Item>
    {
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }
        
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Location)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Location);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            { "EN10006-ST01", "Standard" }
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NotNull]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[CompanyId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Company_Location)]
        public Lookup<Company> CompanyId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_Location)]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[ContactInformationId]")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_Location)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[RegistrationNo]")]
        public SmallText RegistrationNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[TimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_Location)]
        public PickList<Timezone> Timezone { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Notes]")]
        public MediumText Notes { get; set; }
    }
}