using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata.Runtime
{

    [TableProperties("[dbo].[Item]", "[Id]")]
    public class Item
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [NotNull]
        [ColumnName("Id")]
        [BasicColumn]
        InternalId Id { get; set; }

        [ColumnName("[EntityCode]")]
        [NotNull]
        public StringType EntityCode { get; set; }

        [NonQueryable]
        [ColumnName("[EntitySubTypeCode]")]
        [NotNull]
        public StringType EntitySubTypeCode { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Name]")]
        [BasicColumn]
        [FreeTextSearch]
        public XLargeText ItemName { get; set; }
       
        [AccessibleLayout((int)LayoutType.View,(int)LayoutType.Form,(int)LayoutType.List)]
        [ColumnName("[Code]")]
        [FreeTextSearch]
        public XSmallText Code { get; set; }

        [AccessibleLayout((int)LayoutType.View,(int)LayoutType.Form, (int)LayoutType.List)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [SimpleSearch]
       // [NotNull]
        [ColumnName("[Active]")]        
        public PickList<Active> Active { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [ColumnName("[UpdatedOn]")]
        [NotNull]        
        public DateTime UpdatedOn { get; set; }
    }
}