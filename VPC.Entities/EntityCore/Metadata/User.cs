using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Entities.WorkFlow.Engine.User;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Entity.Trigger;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Relations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[User]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("User")]
    [PluralName("Users")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    [SendEmailTask("SendEmail", TaskType.FrontTask, null)]
    [SendSMSTask("SendSms", TaskType.BackTask, TaskVerb.Post)]
    [MergeTask("Merge", TaskType.BackTask, TaskVerb.Post)]
    [PrintTask("Print", TaskType.FrontTask, null)]
    [ResetPasswordTask("ResetPassword", TaskType.BackTask, TaskVerb.Put)]
    [ChangePasswordTask("ChangePassword", TaskType.FrontTask, "", TaskDisplay.Full)]
    [RoleTask("Role", TaskType.FrontTask, "", TaskDisplay.PopUp)]
    [UserExportTask("UserExport", TaskType.BackTask, TaskVerb.Put)]


    //
    [Trigger("ModifyName", new[] { ExecutionType.Create, ExecutionType.Update }, "Item", new string[] { "FirstName", "MiddleName", "LastName" })]

    [CascadeDelete]
    public class User : PrimaryEntity, IItem<Item>
    {

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

        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.User)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.User);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10003-ST01", "Employee" },
            { "EN10003-ST02", "Consultant" }
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        // [AccessibleLayout(1, 2, 3)]
        // [Tagable]
        // public DOBDate DobDate { get; set; }

        // [Receiver(typeof(DateTime), typeof(AgeCalculation))]
        [Receiver("Date", "AgeCalculation")]
        [DisplayName("Age")]
        public ComputedType Age { get; set; }

        [Tagable]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [Broadcaster(typeof(AgeCalculation))]
        [ColumnName("[DOB]")]
        [DisplayName("Date of birth")]
        public VPC.Metadata.Business.DataTypes.DateTime Date { get; set; }

        [ColumnName("[DOBIsApproximate]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [DisplayName("DOB is approximate")]
        public BooleanType DOBIsApproximate { get; set; }

        //normal casses
        // [AccessibleLayout(2)]
        // //[DynamicPrefix(InfoPrefix.UserActivityHisotory_User)]
        // public OneToMany<UserActivityHistory> UserActivityHistory { get; set; }

        //normal casses
        [IntersectColumn("[RoleId]")] //RoleId of UserInRole....
        //[DynamicPrefix(InfoPrefix.UserInRole_User)]
        [DisplayName("Roles")]
        public ManyToMany<UserInRole, Role> Roles { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [DisplayName("First name")]
        [ColumnName("[FirstName]")]
        [NotNull]
        [BasicColumn]
        [Tagable]
        public SmallText FirstName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [FreeTextSearch]
        [Tagable]
        [ColumnName("[MiddleName]")]
        [DisplayName("Middle name")]
        public SmallText MiddleName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[LastName]")]
        [Tagable]
        [NotNull]
        [DisplayName("Last name")]
        public SmallText LastName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        //[NotNull]
        [InverseProperty("[Id]")] //situated in contact table...
        [ColumnName("[ContactInformationId]")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_User)]
        [DisplayName("Contact information")]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_User)]
        [DisplayName("Official address")]
        public Address OfficialAddress { get; set; }

        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_User)]
        [DisplayName("Invoice address")]
        public Address InvoiceAddress { get; set; }

        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        //[DynamicPrefix(InfoPrefix.PostalAddress_User)]
        [DisplayName("Postal address")]
        public Address PostalAddress { get; set; }

        /// type of image>>>>>>>>>>>>>>>>>>>
        // [AccessibleLayout(1, 2, 3)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[AvatarId]")]
        // public Image Avatar { get; set; }

        // [ColumnName("[OrgUnitId]")]
        // public Lookup<OrganizationUnit> OrgUnitId { get; set; }
        [InverseProperty("[Id]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[TimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_User)]
        [DisplayName("Timezone")]
        public PickList<Timezone> Timezone { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[PreferredLanguageId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.Language_User)]
        [DisplayName("Preferred language")]
        public PickList<Language> PreferredLanguageId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [SimpleSearch]
        [AdvanceSearch]
        [ColumnName("[Gender]")]
        [Tagable]
        [DisplayName("Gender")]
        public PickList<Gender> Gender { get; set; }

        [InverseProperty("[Id]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[NationalityId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.Nationality_User)]
        [DisplayName("Nationality")]
        public PickList<Country> Nationality { get; set; }

        // [AccessibleLayout(1, 2, 3)]
        // [ColumnName("[CategoryId]")]
        // //[DynamicPrefix(InfoPrefix.Category_User)]
        // public Lookup<User> Category { get; set; }

        [InverseProperty("[Id]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[CategoryId]")]
        //[DynamicPrefix(InfoPrefix.Category_User)]
         [DisplayName("Category")]
        public PickList<UserCategory> Category { get; set; }

        // [AccessibleLayout(1, 2, 3)]
        // [ColumnName("[CrewId]")]
        // //[DynamicPrefix(InfoPrefix.Crew_User)]
        // public Lookup<User> Crew { get; set; }

        [InverseProperty("[Id]")]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[CrewId]")]
        //[DynamicPrefix(InfoPrefix.Crew_User)]
        [DisplayName("Crew")]
        public PickList<Crew> Crew { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[HourlyRate]")]
        [Tagable]
        [DisplayName("Hourly rate")]
        public DecimalType HourlyRate { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[UserGroupId]")]
        //[DynamicPrefix(InfoPrefix.UserGroup_User)]
        [DisplayName("User group")]
        public PickList<UserGroup> UserGroup { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Comments]")]
        [Tagable]
        [DisplayName("Comments")]
        public LargeText Comments { get; set; }

        //need to check
        [Tagable]
        [DisplayName("User Id")]
        public Guid UserId { get; set; }

        //need to check
        [Tagable]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[CurrentWorkFlowStep]")]
        //[DynamicPrefix(InfoPrefix.UserWorkFlow_User)]
        [DisplayName("Current workflow step")]
        public PickList<UserWorkFlow> CurrentWorkFlowStep { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[UserCredentialId]")]
        //[DynamicPrefix(InfoPrefix.UserCredential_User)]
        [DisplayName("User credential")]
        public UserCredential UserCredential { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [InverseProperty("[Id]")]
        [ColumnName("[UserEmploymentId]")]
        //[DynamicPrefix(InfoPrefix.UserEmployment_User)]
        [DisplayName("User employment")]
        public UserEmployment UserEmployment { get; set; }

        // [AccessibleLayout(1, 3)]
        // [ColumnName("[InvalidAttemptCount]")]
        // [Tagable]
        // public NumericType InvalidAttemptCount { get; set; }

        // [AccessibleLayout(1, 3)]
        // [ColumnName("[IsLocked]")]
        // [Tagable]
        // public BooleanType IsLocked { get; set; }

        // [AccessibleLayout(1, 3)]
        // [ColumnName("[LockedOn]")]
        // [Tagable]
        // public VPC.Metadata.Business.DataTypes.DateTime LockedOn { get; set; }



        public User()
        {

        }
    }
}