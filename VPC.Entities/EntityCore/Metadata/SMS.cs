using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[SMS]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("SMS")]
    [PluralName("SMS")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(true)]
    public class SMS : PrimaryEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.SMS)]
         [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.SMS);

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub Type")]
        public override XSmallText SubType { get; set; }

         [DisplayName("Sub Types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10015-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Date]")]
        [NotNull]
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Recipient]")]
        [NotNull]
        [FreeTextSearch]
        [DisplayName("Recipient")]
        public SmallText Recipient { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Sender]")]
        [NotNull]
        [DisplayName("Sender")]
        public SmallText Sender { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[Message]")]
        [NotNull]
        [DisplayName("Message")]
        public LargeText Message { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[CommunicationDirectionId]")]
        [NotNull]
        [DisplayName("Direction")]
        public PickList<CommunicationDirection> Direction { get; set; }
    }
}
