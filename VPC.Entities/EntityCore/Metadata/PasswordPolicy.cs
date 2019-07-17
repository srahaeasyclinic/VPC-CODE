using System;
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
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]
        [NonQueryable]
        [ColumnName("[Id]")]
        public override InternalId InternalId { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[LockoutAttempt]")]
        public NumericType LockoutAttempt { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[LockoutDuration]")]
        public NumericType LockoutDuration { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[PreviousPasswordDifference]")]
        public NumericType PreviousPasswordDifference { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[ResetOnFirstLogin]")]
        public BooleanType ResetOnFirstLogin { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[IsUppercase]")]
        public BooleanType IsUppercase { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[IsLowercase]")]
        public BooleanType IsLowercase { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[IsNumber]")]
        public BooleanType IsNumber { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[IsNonAlphaNumeric]")]
        public BooleanType IsNonAlphaNumeric { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[CanUserChangeOwnPassword]")]
        public BooleanType CanUserChangeOwnPassword { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[AllowFirstLastName]")]
        public BooleanType AllowFirstLastName { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[PasswordLength]")]
        public NumericType PasswordLength { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[PasswordAge]")]
        public NumericType PasswordAge { get; set; }

        [AccessibleLayout(2)]
        [ColumnName("[AllowRecoveryViaMail]")]
        public BooleanType AllowRecoveryViaMail { get; set; }

        public override EntityContext EntityContext { get; }
    }
}