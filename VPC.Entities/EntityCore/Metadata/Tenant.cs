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
using VPC.Metadata.Business.Relations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Tenant]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus)]
    [DisplayName("Tenant")]
    [PluralName("Tenants")]
    [SupportWorkflow(true)]
    public class Tenant : PrimaryEntity, IItem<Item>
    {
        // , IActivityEntity<User>   
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Tenant)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Tenant);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10001-ST01", "Standard" } };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[ContactInformationId]")]
        [DisplayName("Contact information")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_Tenant)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[PasswordPolicyId]")]
        [DisplayName("Password policy")]
        //[DynamicPrefix(InfoPrefix.PasswordPolicy_Tenant)]
        public PasswordPolicy PasswordPolicy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DefaultValue("0")]
        [ColumnName("[IsOrganization]")]
        [DisplayName("Is organization")]
        public BooleanType IsOrganization { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[OrgNo]")]
        [FreeTextSearch]
        [DisplayName("Org no")]
        public XSmallText OrgNo { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[IsLegalEntity]")]
        [DisplayName("Is legal entity")]
        public BooleanType IsLegalEntity { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[TenantType]")]
        [NotNull]
        [DisplayName("Tenant type")]
        //[DynamicPrefix(InfoPrefix.TenantType_Tenant)]
        public PickList<TenantType> TenantType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        [DisplayName("Official address")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_Tenant)]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [DisplayName("Invoice address")]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_Tenant)]
        public Address InvoiceAddress { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        //[DynamicPrefix(InfoPrefix.PostalAddress_Tenant)]
        [DisplayName("Postal address")]
        public Address PostalAddress { get; set; }

        [ColumnName("[IsSystemRoot]")]
        [DisplayName("Is system root")]
        public BooleanType IsSystemRoot { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[PreferredLanguageId]")]
        //[DynamicPrefix(InfoPrefix.Language_Tenant)]
        [DisplayName("Preferred language")]
        public PickList<Language> PreferredLanguageId { get; set; }

        [AccessibleLayout(21, 22)]
        [ColumnName("[SuperAdminId]")]
        [QuickAddAttribute(21)]
        //[DynamicPrefix(InfoPrefix.SuperAdminId_Tenant)]
        [DisplayName("Super admin")]
        public Lookup<User> SuperAdminId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[SubscriptionId]")]
        //[DynamicPrefix(InfoPrefix.Subscription_Tenant)]
        [NotNull]
        [DisplayName("Tenant subscription")]
        public Lookup<Subscription> TenantSubscription { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        //[DynamicPrefix(InfoPrefix.TenantIPRange_Tenant)]
        [DisplayName("Tenant IP range")]
        public OneToMany<TenantIPRange> TenantIPRange { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[TenantServiceStatusId]")]
        //[DynamicPrefix(InfoPrefix.TenantServiceStatus_Tenant)]
        [DisplayName("Tenant service status")]
        public TenantServiceStatus TenantServiceStatus { get; set; }

        [InverseProperty("[Id]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[DefaultTimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_Tenant)]
        [DisplayName("Timezone")]
        public PickList<Timezone> TimezoneId { get; set; }

    }
}