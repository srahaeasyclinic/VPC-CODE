using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
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
    [TableProperties("[dbo].[Manufacturer]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Manufacturer")]
    [PluralName("Manufacturers")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    [SendEmailTask("Send email",TaskType.FrontTask,null)]
    [SendSMSTask("Send sms",TaskType.BackTask,TaskVerb.Post)]
    [MergeTask("Merge",TaskType.BackTask,TaskVerb.Post)]
    [PrintTask("Print",TaskType.FrontTask,null)]
    public class Manufacturer : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Manufacturer);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20007-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }
    }
}