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
    [TableProperties("[dbo].[ProductionOrderOperationCompetence]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Production order operation competence")]
    [PluralName("Production order operation competences")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
   
    public class ProductionOrderOperationCompetence : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.ProductionOrderOperationCompetence);

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
            {"EN20048-ST01", "Standard"}
        };

        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }
       
       [DisplayName("Operation Id")]
        public Guid OperationId { get; set; }
        
        [DisplayName("Competence Id")]
        public PickList<Competence> CompetenceId { get; set; }
    }
}