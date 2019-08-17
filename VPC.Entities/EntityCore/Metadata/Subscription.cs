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
        [DisplayName("Tenant Id")]     
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]       
        [ColumnName("[Id]")]
        [NotNull]
        [BasicColumn]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.TenantSubscriptions);

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10009-ST01", "Standard" } };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Sub type")]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Group]")] //it should be categorId
        [DisplayName("Tenant type")]
        public PickList<TenantType> TenantType { get; set; } //SubscriptionCategory

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[RecurringPrice]")]
        [DisplayName("Recurring price")]
        public DecimalType RecurringPrice { get; set; }

        // [ColumnName("[RecurringUnit]")]
        // public PickList<RecurringUnit> RecurringUnit { get; set; } //RecurringUnit
        
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[SetupPrice]")]
        [DisplayName("Setup price")]
        public DecimalType SetupPrice { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Duration]")]
        [DisplayName("Duration")]
        public NumericType Duration { get; set; }
    }
}
