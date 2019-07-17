// using VPC.Entities.EntityCore.Metadata.Picklist;
// using VPC.Metadata.Business.DataAnnotations;
// using VPC.Metadata.Business.DataTypes;
// using VPC.Metadata.Business.Entity.Configuration;
// using VPC.Metadata.Business.SearchFilter;

// namespace VPC.Entities.EntityCore.Metadata.Runtime {

//     [TableProperties("[dbo].[Relation]", "[Id]")]
//     public class Relation {


//         [NonQueryableAttribute]
//         [ColumnName ("[TenantCode]")]
//         [NotNull]
//         public XSmallText TenantCode { get; set; }

//         [NonQueryableAttribute]
//         [ColumnName ("Id")]
//         [NotNull]
//         InternalId Id { get; set; }

//         [NonQueryableAttribute]
//         [ColumnName ("ParentGuid")]
//         [NotNull]
//         InternalId ParentGuid { get; set; }


//         [NonQueryableAttribute]
//         [ColumnName ("ChildGuid")]
//         [NotNull]
//         InternalId ChildGuid { get; set; }


//         [NonQueryableAttribute]
//         [ColumnName ("RelationType")]
//         [NotNull]
//         InternalId RelationType { get; set; }
        
//     }
// }