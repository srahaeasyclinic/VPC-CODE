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
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }
        
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Location)]
         [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Location);

        [DisplayName("Sub types")]    
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            { "EN10006-ST01", "Standard" }
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NotNull]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[CompanyId]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.Company_Location)]
        [DisplayName("Company")]
        public Lookup<Company> CompanyId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_Location)]
        [DisplayName("Official address")]
        [NotNull]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[ContactInformationId]")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_Location)]
        [DisplayName("Contact information")]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[RegistrationNo]")]
        [DisplayName("Registration no")]
        public SmallText RegistrationNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[TimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_Location)]
        [DisplayName("Timezone")]
        public PickList<Timezone> Timezone { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Notes]")]
        [DisplayName("Notes")]
        public MediumText Notes { get; set; }
    }
}