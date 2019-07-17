using VPC.Entities.EntityCore.Metadata.Picklist;
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

        [AccessibleLayout(1, 3)]
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

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Name]")]
        [BasicColumn]
        [FreeTextSearch]
        public XLargeText ItemName { get; set; }
       
        [AccessibleLayout(1,2,3)]
        [ColumnName("[Code]")]
        [FreeTextSearch]
        public XSmallText Code { get; set; }

        [AccessibleLayout(1, 3)]
        [ApplicableForFilter]
        [AdvanceSearch]
        [SimpleSearch]
        [NotNull]
        [ColumnName("[Active]")]        
        public PickList<Active> Active { get; set; }

        [AccessibleLayout(1, 3)]
        [ColumnName("[UpdatedBy]")]
        [NotNull]
        public Lookup<User> UpdatedBy { get; set; }

        [AccessibleLayout(1, 3)]
        [ColumnName("[UpdatedOn]")]
        [NotNull]        
        public DateTime UpdatedOn { get; set; }
    }
}