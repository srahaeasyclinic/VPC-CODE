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
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Customer]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Customer")]
    [PluralName("Customers")]
    public class Customer : PrimaryEntity, IItem<Item>
    {        
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Customer)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Customer);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10005-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[FirstName]")]
        [NotNull]
        [BasicColumn]
        [Tagable]
        public SmallText FirstName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [FreeTextSearch]
        [Tagable]
        [ColumnName("[MiddleName]")]
        public SmallText MiddleName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[LastName]")]
        [Tagable]        
        public SmallText LastName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("ContactInformationId")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.ContactInformation_customer)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_customer)]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_customer)]
        public Address InvoiceAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.PostalAddress_customer)]
        public Address PostalAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [InverseProperty("[Id]")]
        [ColumnName("[AvatarId]")]
        [NonQueryable]
        public Image Avatar { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [NonQueryable]
        [ColumnName("[Comment]")]
        public XLargeText Comment { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[CustomerCredentialId]")]
        // //[DynamicPrefix(InfoPrefix.CustomerCredentialId_customer)]
        // public CustomerCredential CustomerCredential { get; set; }
        
    }
}
