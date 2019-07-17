using System;
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
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Username]")]
        [Tagable]
        [NotNull]
        public VPC.Metadata.Business.DataTypes.Email Username { get; set; }

        [AccessibleLayout(2)]
        [Tagable]
        //[NotNull]
        [ColumnName("[***]")]
        public Password Password { get; set; }


        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SecurityQuestion1]")]
        public MediumText SecurityQuestion1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SecurityAnswer1]")]
        public MediumText SecurityAnswer1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SecurityQuestion2]")]
        public MediumText SecurityQuestion2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SecurityAnswer2]")]
        public MediumText SecurityAnswer2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[InvalidAttemptCount]")]
        [Tagable]
        public NumericType InvalidAttemptCount { get; set; }

        [ColumnName("[IsLocked]")]
        [Tagable]
        public BooleanType IsLocked { get; set; }
        [DefaultValue("1")]
        [ColumnName("[IsNew]")]
        [Tagable]
        public BooleanType IsNew { get; set; }


        [ColumnName("[LockedOn]")]
        [Tagable]
        public VPC.Metadata.Business.DataTypes.DateTime LockedOn { get; set; }

        [ColumnName("[PasswordChangedOn]")]
        [Tagable]
        public VPC.Metadata.Business.DataTypes.DateTime PasswordChangedOn { get; set; }
    }
}