// using System;
// using System.Collections.Generic;
// using VPC.Entities.EntityCore.Metadata.Picklist;
// using VPC.Entities.EntityCore.Metadata.Runtime;
// using VPC.Metadata.Business.DataAnnotations;
// using VPC.Metadata.Business.DataTypes;
// using VPC.Metadata.Business.DataTypes.Complex;
// using VPC.Metadata.Business.Entity;
// using VPC.Metadata.Business.Entity.Configuration;
// using VPC.Metadata.Business.Entity.Infrastructure;
// using VPC.Metadata.Business.Operations;
// using VPC.Metadata.Business.SearchFilter;
// using VPC.Metadata.Business.Tasks;

// namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
// {
//     [TableProperties("[dbo].[Workcenter]", "[Id]")]
//     [Operation(Operations.Create, Operations.Update, Operations.Delete)]
//     [DisplayName("Workcenter")]
//     [PluralName("Workcenters")]
//     [Import(false)]
//     [Export(false)]
//     [SupportWorkflow(true)]
   
//     public class Workcenter : PrimaryEntity, IItem<Item>
//     {
//         public override EntityContext EntityContext => new EntityContext(InfoType.Workcenter);

//         [NonQueryable]
//         [ColumnName("[Id]")]
//         [NotNull]
//         public override InternalId InternalId { get; set; }

//         [NonQueryable]
//         [Tagable]
//         public override Name Name { get; set; }

//         public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
//         {
//             {"EN20012-ST01", "Standard"}
//         };

//         public override XSmallText SubType { get; set; }
        
//         public Guid AvatarId { get; set; }
//         [Tagable]
//         public PickList<WorkcenterType> WorkcenterType { get; set; }
//         [Tagable]
//         public  PickList<WorkcenterSubType> WorkcenterSubType { get; set; }
//         [Tagable]
//         public PickList<OperationType> OperationTypeId { get; set; }
//         [Tagable]
//         public PickList<Department> DepartmentId { get; set; }
//         [Tagable]
//         public PickList<ProductionLine> ProductionLineId { get; set; }
//         [Tagable]
//         public Lookup<PlanningGroup> PlanningGroupCode { get; set; }
      
//         public BooleanType IsObsolete { get; set; }
//         [Tagable]
//         public Lookup<Vendor> VendorCode { get; set; }
//         [Tagable]
//         public Lookup<Product> ProductCode { get; set; }
      
//         public BooleanType IsDefault { get; set; }
//        // CalendarId




//     }
// }