using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Entities.EntityCore.Model.Storage;
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
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("EmploymentStart")]
        [DisplayName("Employment start")]
        public DateTime EmploymentStart { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("EmploymentEnd")]
        [DisplayName("Employment end")]
        public DateTime EmploymentEnd { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("EmploymentStatusId")]
        [DisplayName("Employment status")]
        public PickList<EmploymentStatus> EmploymentStatusId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("ReasonForLeavingId")]
        [DisplayName("Reason for leaving")]
        public PickList<ReasonForLeaving> ReasonForLeavingId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("DesignationId")]
        [DisplayName("Designation")]
        public PickList<Designation> DesignationId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[ReportsToUserId]")]
        [DisplayName("Reports to user")]
        public Lookup<User> ReportsToUserId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("AnnualLeaveEntitlementDays")]
        [DisplayName("Annual leave entitlement days")]
        public DecimalType AnnualLeaveEntitlementDays { get; set; }
    }
}