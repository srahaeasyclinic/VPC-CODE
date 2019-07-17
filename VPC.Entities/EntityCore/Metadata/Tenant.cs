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
using VPC.Metadata.Business.Relations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Tenant]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Tenant")]
    [PluralName("Tenants")]    
    [SupportWorkflow(true)]   
    public class Tenant : PrimaryEntity, IItem<Item>, IActivityEntity<User>
    {        
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]
        [BasicColumn]    
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        [DefaultValue(InfoType.Tenant)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Tenant);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10001-ST01", "Standard" } };

        [AccessibleLayout(1, 3)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout(1, 2, 3)]        
        [InverseProperty("[Id]")]
        [ColumnName("[ContactInformationId]")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_Tenant)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[PasswordPolicyId]")]
        //[DynamicPrefix(InfoPrefix.PasswordPolicy_Tenant)]
        public PasswordPolicy PasswordPolicy { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [DefaultValue("0")]
        [ColumnName("[IsOrganization]")]
        public BooleanType IsOrganization { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[OrgNo]")]
        [FreeTextSearch]
        public XSmallText OrgNo { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[IsLegalEntity]")]
        public BooleanType IsLegalEntity { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[TenantType]")]
        [NotNull]
        //[DynamicPrefix(InfoPrefix.TenantType_Tenant)]
        public PickList<TenantType> TenantType { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_Tenant)]
        public Address OfficialAddress { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_Tenant)]
        public Address InvoiceAddress { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        //[DynamicPrefix(InfoPrefix.PostalAddress_Tenant)]
        public Address PostalAddress { get; set; }

        [ColumnName("[IsSystemRoot]")]
        public BooleanType IsSystemRoot { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[PreferredLanguageId]")]
        //[DynamicPrefix(InfoPrefix.Language_Tenant)]
        public PickList<Language> PreferredLanguageId { get; set; }

        [ColumnName("[SuperAdminId]")]
        //[DynamicPrefix(InfoPrefix.SuperAdminId_Tenant)]
        public Lookup<User> SuperAdminId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SubscriptionId]")]
        //[DynamicPrefix(InfoPrefix.Subscription_Tenant)]
        [NotNull]
        public Lookup<Subscription> TenantSubscription { get; set; }

        [AccessibleLayout(2)]
        //[DynamicPrefix(InfoPrefix.TenantIPRange_Tenant)]
        public OneToMany<TenantIPRange> TenantIPRange { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[TenantServiceStatusId]")]
        //[DynamicPrefix(InfoPrefix.TenantServiceStatus_Tenant)]
        public TenantServiceStatus TenantServiceStatus { get; set; }

        [InverseProperty("[Id]")]
        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[DefaultTimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_Tenant)]
        public PickList<Timezone> TimezoneId { get; set; }

    }
}