using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[UserActivityHistory]", "[Id]")]
    //[TableProperties("[dbo].[Test]", "[Id]")]
    [Operation(Operations.Delete)]
    [DisplayName("User activity history")]
    [PluralName("User activity histories")]
    [CascadeDelete]
    public class UserActivityHistory : DetailEntity
    {
        public UserActivityHistory()
        {

        }

        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        public override Name Name { get; set; }

        public override EntityContext EntityContext => new EntityContext(InfoType.UserActivity);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10021-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[HistoryName]")]
        [NotNull]
        public SmallText HistoryName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [AdvanceSearch]
        [ColumnName("[Test]")]
        [NotNull]
        public SmallText Test { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ForeignKey("[dbo].[User]", "[Id]")]
        [ColumnName("[UserId]")]
        [NotNull]
        public InternalId UserId { get; set; }
    }
}