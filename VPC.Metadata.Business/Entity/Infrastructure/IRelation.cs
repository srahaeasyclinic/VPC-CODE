// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using VPC.Metadata.Business.DataAnnotations;
// using VPC.Metadata.Business.DataTypes;
// using VPC.Metadata.Business.Entity.Configuration;
// using VPC.Metadata.Business.Operations;
// using VPC.Metadata.Business.SearchFilter;

// namespace VPC.Metadata.Business.Entity.Infrastructure
// {
//     public interface IRelation<T>
//     {
//     }
   
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Metadata.Business.Entity.Infrastructure
{
    public abstract class Relation
    {
        [NonQueryableAttribute]
        [ColumnName ("[TenantCode]")]
        [NotNull]
        public XSmallText TenantCode { get; set; }

        [NonQueryableAttribute]
        [ColumnName ("Id")]
        [NotNull]
        public InternalId Id { get; set; }

        [NonQueryableAttribute]
        [ColumnName ("ParentGuid")]
        [NotNull]
        public virtual InternalId ParentGuid { get; set; }


        [NonQueryableAttribute]
        [ColumnName ("ChildGuid")]
        [NotNull]
        public virtual InternalId ChildGuid { get; set; }


        [NonQueryableAttribute]
        [ColumnName ("RelationType")]
        [NotNull]
        public abstract InternalId RelationType { get; set; }
    }
}
