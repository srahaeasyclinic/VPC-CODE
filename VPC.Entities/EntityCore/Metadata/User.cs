using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Entities.WorkFlow.Engine.User;
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
    [ResetPasswordTask("ResetPassword", TaskType.BackTask, TaskVerb.Post)]
    [ChangePasswordTask("ChangePassword", TaskType.FrontTask, "", TaskDisplay.Full)]
    [RoleTask("Role", TaskType.FrontTask, "", TaskDisplay.PopUp)]
    [CascadeDelete]
    public class User : PrimaryEntity, IItem<Item>
    {
       
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]
        [BasicColumn]     
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [Tagable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.User)]
        public override EntityContext EntityContext => new EntityContext(InfoType.User);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10003-ST01", "Employee" },
            { "EN10003-ST02", "Consultant" }
        };

        [AccessibleLayout(1, 3)]
        public override XSmallText SubType { get; set; }

        // [AccessibleLayout(1, 2, 3)]
        // [Tagable]
        // public DOBDate DobDate { get; set; }


         // [Receiver(typeof(DateTime), typeof(AgeCalculation))]
        [Receiver("Date", "AgeCalculation")]
        public ComputedType Age { get; set; }
        
        [Tagable]
         [AccessibleLayout(1, 2, 3)]
        [Broadcaster(typeof(AgeCalculation))]
        [ColumnName("[DOB]")]
        public VPC.Metadata.Business.DataTypes.DateTime Date { get; set; }


        [ColumnName("[DOBIsApproximate]")]
        [AccessibleLayout(1, 2, 3)]
        public BooleanType DOBIsApproximate { get; set; }

        //normal casses
        // [AccessibleLayout(2)]
        // //[DynamicPrefix(InfoPrefix.UserActivityHisotory_User)]
        // public OneToMany<UserActivityHistory> UserActivityHistory { get; set; }

        //normal casses
        [IntersectColumn("[RoleId]")] //RoleId of UserInRole....
        //[DynamicPrefix(InfoPrefix.UserInRole_User)]
        public ManyToMany<UserInRole, Role> Roles { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [DisplayName("First name")]
        [ColumnName("[FirstName]")]
        [NotNull]
        [BasicColumn]
        [Tagable]
        public SmallText FirstName { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [FreeTextSearch]
        [Tagable]
        [ColumnName("[MiddleName]")]
        public SmallText MiddleName { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[LastName]")]
        [Tagable]
        [NotNull]
        public SmallText LastName { get; set; }

        [AccessibleLayout(1, 2, 3)]
        //[NotNull]
        [InverseProperty("[Id]")] //situated in contact table...
        [ColumnName("[ContactInformationId]")]
        //[DynamicPrefix(InfoPrefix.ContactInformation_User)]
        public ContactInformation ContactInformation { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[OfficialAddressId]")]
        //[DynamicPrefix(InfoPrefix.OfficialAddress_User)]
        public Address OfficialAddress { get; set; }

        [InverseProperty("[Id]")]
        [ColumnName("[InvoiceAddressId]")]
        [AccessibleLayout(1, 2, 3)]
        //[DynamicPrefix(InfoPrefix.InvoiceAddress_User)]
        public Address InvoiceAddress { get; set; }

        [InverseProperty("[Id]")]
        [ColumnName("[PostalAddressId]")]
        [AccessibleLayout(1, 2, 3)]
        //[DynamicPrefix(InfoPrefix.PostalAddress_User)]
        public Address PostalAddress { get; set; }

        /// type of image>>>>>>>>>>>>>>>>>>>
        // [AccessibleLayout(1, 2, 3)]
        // [InverseProperty("[Id]")]
        // [ColumnName("[AvatarId]")]
        // public Image Avatar { get; set; }

        // [ColumnName("[OrgUnitId]")]
        // public Lookup<OrganizationUnit> OrgUnitId { get; set; }
        [InverseProperty("[Id]")]
        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[TimezoneId]")]
        //[DynamicPrefix(InfoPrefix.Timezone_User)]
        public PickList<Timezone> Timezone { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[PreferredLanguageId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.Language_User)]
        public PickList<Language> PreferredLanguageId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ApplicableForFilter]
        [SimpleSearch]
        [AdvanceSearch]
        [ColumnName("[Gender]")]
        [Tagable]
     
        public PickList<Gender> Gender { get; set; }


        [InverseProperty("[Id]")]
        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[NationalityId]")]
        [Tagable]
        //[DynamicPrefix(InfoPrefix.Nationality_User)]
        public PickList<Country> Nationality { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[CategoryId]")]
        //[DynamicPrefix(InfoPrefix.Category_User)]
        public Lookup<User> Category { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[CrewId]")]
        //[DynamicPrefix(InfoPrefix.Crew_User)]
        public Lookup<User> Crew { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[HourlyRate]")]
        [Tagable]
        public DecimalType HourlyRate { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[UserGroupId]")]
        //[DynamicPrefix(InfoPrefix.UserGroup_User)]
        public PickList<UserGroup> UserGroup { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Comments]")]
        [Tagable]
        public LargeText Comments { get; set; }

        //need to check
        [Tagable]
        public Guid UserId { get; set; }

        //need to check
        [Tagable]
        public string UserName { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[CurrentWorkFlowStep]")]
        //[DynamicPrefix(InfoPrefix.UserWorkFlow_User)]
        public PickList<UserWorkFlow> CurrentWorkFlowStep { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[UserCredentialId]")]
        //[DynamicPrefix(InfoPrefix.UserCredential_User)]
        public UserCredential UserCredential { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[UserEmploymentId]")]
        //[DynamicPrefix(InfoPrefix.UserEmployment_User)]
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