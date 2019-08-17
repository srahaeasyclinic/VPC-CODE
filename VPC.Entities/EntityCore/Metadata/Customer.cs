using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Entity.Trigger;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Customer]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Customer")]
    [PluralName("Customers")]
    [Trigger("ModifyName", new[] { ExecutionType.Create, ExecutionType.Update }, "Item", new string[] { "FirstName", "MiddleName", "LastName" })]
    public class Customer : PrimaryEntity, IItem<Item>
    {        
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
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

        [DefaultValue(InfoType.Customer)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Customer);

        [DisplayName("Sub tsypes")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10005-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[FirstName]")]
        [NotNull]
        [BasicColumn]
        [Tagable]
        [DisplayName("First name")]
        public SmallText FirstName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [FreeTextSearch]
        [Tagable]
        [ColumnName("[MiddleName]")]
        [DisplayName("Middle name")]
        public SmallText MiddleName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[LastName]")]
        [Tagable]        
        [DisplayName("Last name")]
        public SmallText LastName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("ContactInformationId")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.ContactInformation_customer)]
        [DisplayName("Contact information")]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_customer)]
        [DisplayName("Official address")]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_customer)]
        [DisplayName("Invoice address")]
        public Address InvoiceAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.PostalAddress_customer)]
        [DisplayName("Postal address")]
        
        public Address PostalAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[AvatarId]")]
        [NonQueryable]
        [DisplayName("Avatar")]
        public Image Avatar { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [NonQueryable]
        [ColumnName("[Comment]")]
        [DisplayName("Comment")]
        public XLargeText Comment { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[CustomerCredentialId]")]
        // //[DynamicPrefix(InfoPrefix.CustomerCredentialId_customer)]
        // public CustomerCredential CustomerCredential { get; set; }
        
    }
}
