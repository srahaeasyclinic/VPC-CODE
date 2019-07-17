using System;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using DateTime = System.DateTime;

namespace VPC.Entities.EntityCore.Metadata.IntersectionEntity
{
    [TableProperties("[dbo].[UserLeave]", "[Id]")]
    [DisplayName("User leave")]
    [PluralName("User leaves")]
    public class UserLeave : IntersectEntity
    {
        public override Name Name { get; set; }

        [DefaultValue(InfoType.UserLeave)]
        public override EntityContext EntityContext => new EntityContext(InfoType.UserLeave);

        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public PickList<LeaveCategory> LeaveCategory { get; set; }
       





     }

  
}
