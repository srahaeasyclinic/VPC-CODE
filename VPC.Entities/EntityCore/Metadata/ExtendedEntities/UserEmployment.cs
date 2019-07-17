using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using DateTime = System.DateTime;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[UserEmployment]", "[Id]")]
    [DisplayName("User employment")]
    [PluralName("User employments")]
    [CascadeDelete]
    public class UserEmployment : ExtendedEntity
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
        [ColumnName("EmploymentStart")]
        public DateTime EmploymentStart { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("EmploymentEnd")]
        public DateTime EmploymentEnd { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("EmploymentStatusId")]
        public PickList<EmploymentStatus> EmploymentStatusId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("ReasonForLeavingId")]
        public PickList<ReasonForLeaving> ReasonForLeavingId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("DesignationId")]
        public PickList<Designation> DesignationId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[ReportsToUserId]")]
        public Lookup<User> ReportsToUserId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("AnnualLeaveEntitlementDays")]
        public DecimalType AnnualLeaveEntitlementDays { get; set; }
    }
}