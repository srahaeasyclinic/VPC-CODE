using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Credential]", "[Id]")]
    [DisplayName("User credential")]
    [PluralName("User credentials")]
    [CascadeDelete]
    public class UserCredential : ExtendedEntity
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout(21)]
        [ColumnName("[Username]")]
        [Tagable]
        [NotNull]
        //public VPC.Metadata.Business.DataTypes.Email Username { get; set; }
        [DisplayName("User name")]
        public VPC.Metadata.Business.DataTypes.Email Username { get; set; }

        [AccessibleLayout(21)]
        [Tagable]
        [ColumnName("[***]")]
        [DisplayName("Password")]
        [NotNull]
        public Password Password { get; set; }


        [AccessibleLayout(21)]
        [ColumnName("[SecurityQuestion1]")]
        [DisplayName("Security question 1")]
        public MediumText SecurityQuestion1 { get; set; }

        [AccessibleLayout(21)]
        [ColumnName("[SecurityAnswer1]")]
        [DisplayName("Security answer 1")]
        public MediumText SecurityAnswer1 { get; set; }

        [AccessibleLayout(21)]
        [ColumnName("[SecurityQuestion2]")]
        [DisplayName("Security question 2")]
        public MediumText SecurityQuestion2 { get; set; }

        [AccessibleLayout(21)]
        [ColumnName("[SecurityAnswer2]")]
        [DisplayName("Security answer 2")]
        public MediumText SecurityAnswer2 { get; set; }

        [AccessibleLayout(21)]
        [ColumnName("[InvalidAttemptCount]")]
        [Tagable]
        [DisplayName("Invalid attempt count")]
        public NumericType InvalidAttemptCount { get; set; }

        [ColumnName("[IsLocked]")]
        [Tagable]
        [DisplayName("Is locked")]
        public BooleanType IsLocked { get; set; }
        [DefaultValue("1")]
        [ColumnName("[IsNew]")]
        [Tagable]
        [DisplayName("Is new")]
        public BooleanType IsNew { get; set; }


        [ColumnName("[LockedOn]")]
        [Tagable]
        [DisplayName("Locked on")]
        public VPC.Metadata.Business.DataTypes.DateTime LockedOn { get; set; }

        [ColumnName("[PasswordChangedOn]")]
        [Tagable]
        [DisplayName("Password changed on")]
        public VPC.Metadata.Business.DataTypes.DateTime PasswordChangedOn { get; set; }
    }
}