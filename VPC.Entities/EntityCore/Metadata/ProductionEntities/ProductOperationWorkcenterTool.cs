using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata.ProductionEntities
{
    [TableProperties("[dbo].[ProductOperationWorkcenterTool]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Product operation workcenter tool")]
    [PluralName("Product operation workcenter tools")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    public class ProductOperationWorkcenterTool : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductOperationWorkcenterTool);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20032-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }
        
        [DisplayName("Operation workcenter Id")]
        public Guid OperationWorkcenterId { get; set; }

        [Tagable]
        [DisplayName("Tool code")]
        public PickList<Tool> ToolCode { get; set; }
    }
}