using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;

namespace VPC.Entities.Email
{
    [TableProperties("[dbo].[Email]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete)]
    [DisplayName("Email")]
    [PluralName("Emails")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    public class Email : PrimaryEntity, IItem<Item>
    {
        //
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [Tagable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Email)]
        public override EntityContext EntityContext => new EntityContext(InfoType.Email);

        public override XSmallText SubType { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10014-ST01", "Standard"}
        };

        // [ApplicableForFilter]
        // [FreeTextSearch]  
        // [ColumnName("[EmailId]")]
        // [NotNull]
        // [BasicColumn]
        // public SmallText EmailId { get; set; }
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Date]")]
        [NotNull]
        [Tagable]
        public DateTime Date { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Recipient]")]
        [NotNull]
        [Tagable]
        [FreeTextSearch]
        public MediumText Recipient { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Sender]")]
        [NotNull]
        [Tagable]
        public MediumText Sender { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Subject]")]
        [Tagable]
        public MediumText Subject { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Body]")]
        [NotNull]
        [Tagable]
        public RichText Body { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [SimpleSearch]
        [ColumnName("[CurrentWorkFlowStep]")]
        public PickList<EmailWorkFlow> CurrentWorkFlowStep { get; set; }
    }
}
