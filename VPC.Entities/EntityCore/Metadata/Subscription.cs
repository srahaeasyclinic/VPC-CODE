using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[TenantSubscription]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Subscription")]
    [PluralName("Subscriptions")]
    [Import(false)]
    [Export(false)]
    [SupportWorkflow(false)]
    [SendEmailTask("Send email", TaskType.FrontTask, null)]
    [SendSMSTask("Send sms", TaskType.BackTask, TaskVerb.Post)]
    [MergeTask("Merge", TaskType.BackTask, TaskVerb.Post)]
    [PrintTask("Print", TaskType.FrontTask, null)]
    public class Subscription : PrimaryEntity, IItem<Item>
    {
        
        [ColumnName("[TenantId]")]
        [NotNull]     
        public InternalId TenantId { get; set; }

        [AccessibleLayout(1, 3)]       
        [ColumnName("[Id]")]
        [NotNull]
        [BasicColumn]
        public override InternalId InternalId { get; set; }
        
        public override Name Name { get; set; }

        public override EntityContext EntityContext => new EntityContext(InfoType.Subscription);

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10009-ST01", "Standard" } };

        [AccessibleLayout(1, 3)]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Group]")] //it should be categorId
        public PickList<TenantType> TenantType { get; set; } //SubscriptionCategory

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[RecurringPrice]")]
        public DecimalType RecurringPrice { get; set; }

        // [ColumnName("[RecurringUnit]")]
        // public PickList<RecurringUnit> RecurringUnit { get; set; } //RecurringUnit
        
        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[SetupPrice]")]
        public DecimalType SetupPrice { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [ColumnName("[Duration]")]
        public NumericType Duration { get; set; }
    }
}
