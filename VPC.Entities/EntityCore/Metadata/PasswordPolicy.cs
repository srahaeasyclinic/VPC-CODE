using System;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[PasswordPolicy]", "[Id]")]
    [DisplayName("Password policy")]
    [PluralName("Password policies")]
    [CascadeDelete]
    public class PasswordPolicy : ExtendedEntity
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[LockoutAttempt]")]
        [DisplayName("Lockout attempt")]
        public NumericType LockoutAttempt { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[LockoutDuration]")]
        [DisplayName("Lockout duration")]
        public NumericType LockoutDuration { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[PreviousPasswordDifference]")]
        [DisplayName("Previous password difference")]
        public NumericType PreviousPasswordDifference { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[ResetOnFirstLogin]")]
        [DisplayName("Reset on first login")]
        public BooleanType ResetOnFirstLogin { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[IsUppercase]")]
        [DisplayName("Is uppercase")]
        public BooleanType IsUppercase { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[IsLowercase]")]
        [DisplayName("Is lowercase")]
        public BooleanType IsLowercase { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[IsNumber]")]
        [DisplayName("Is number")]
        public BooleanType IsNumber { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[IsNonAlphaNumeric]")]
        [DisplayName("Is non alpha numeric")]
        public BooleanType IsNonAlphaNumeric { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[CanUserChangeOwnPassword]")]
        [DisplayName("Can user change own password")]
        public BooleanType CanUserChangeOwnPassword { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[AllowFirstLastName]")]
        [DisplayName("Allow first last name")]
        public BooleanType AllowFirstLastName { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[PasswordLength]")]
        [DisplayName("Password length")]
        public NumericType PasswordLength { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[PasswordAge]")]
        [DisplayName("Password age")]
        public NumericType PasswordAge { get; set; }

        [AccessibleLayout((int)LayoutType.Form)]
        [ColumnName("[AllowRecoveryViaMail]")]
        [DisplayName("Allow recovery via mail")]
        public BooleanType AllowRecoveryViaMail { get; set; }

        public override EntityContext EntityContext { get; }
    }
}