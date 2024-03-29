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
    [TableProperties("[dbo].[LockRegisterDetail]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Lock register detail")]
    [PluralName("Lock register details")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    
    
    public class LockRegisterDetail : PrimaryEntity, IItem<Item>
    {
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.LockRegisterDetail);

        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN20056-ST01", "Standard"}
        };
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("Lock code")]
        public XSmallText LockCode { get; set; }

        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Is purchase locked")]
        public BooleanType IsPurchaseLocked { get; set; }

        [DisplayName("Is sale locked")]
        public BooleanType IsSaleLocked { get; set; }

        [DisplayName("Is inventory locked")]
        public BooleanType IsInventoryLocked { get; set; }
    }
}