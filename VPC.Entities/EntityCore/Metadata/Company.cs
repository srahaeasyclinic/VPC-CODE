using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Company]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Company")]
    [PluralName("Companies")]
    public class Company : PrimaryEntity, IItem<Item>
    {        
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [DisplayName("Tenant Id")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [DisplayName("Company Id")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [DefaultValue(InfoType.Company)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Company);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10002-ST01", "Standard" } };

        [AccessibleLayout((int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("IsLegalEntity")]
        [DisplayName("Is Legal Entity")]
        [NotNull]
        public BooleanType IsLegalEntity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("Description")]
        [DisplayName("Description")]
        [FreeTextSearch]
        public LargeText Description { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("ContactInformationId")]
        [DisplayName("Contact Information")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.ContactInformation_Company)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        [DisplayName("Official Address")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_Company)]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [DisplayName("Invoice Address")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_Company)]
        public Address InvoiceAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        [DisplayName("Postal Address")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.PostalAddress_Company)]
        public Address PostalAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[AvatarId]")]
        [DisplayName("Avatar")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.Avater_Company)]
        public Image Avatar { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[TimezoneId]")]
        [DisplayName("Timezone")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.Timezone_Company)]
        public PickList<Timezone> TimezoneId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[PreferredLanguageId]")]
        [DisplayName("Preferred Language")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.PreferredLanguage_Company)]
        public PickList<Language> PreferredLanguageId { get; set; }
    }
}
