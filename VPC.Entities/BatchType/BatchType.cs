using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.DataTypes.CustomField.Server;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.CustomField.Attributes;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;
using DateTime = VPC.Metadata.Business.DataTypes.DateTime;

namespace VPC.Entities.BatchType
{

  [TableProperties("[dbo].[BatchType]", "[Id]")]
  [Operation(Operations.Create, Operations.Update, Operations.Delete,Operations.UpdateStatus)]
  [DisplayName("Batch type")]
  [PluralName("Batch types")]
  [Import(false)]
  [Export(false)]
  [SupportWorkflow(false)]
  [RunNowTask("RunNow", TaskType.BackTask, TaskVerb.Put)]
  public  class BatchType  : PrimaryEntity, IItem<Item>
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
        [Tagable]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.BatchType)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.BatchType);
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10060-ST01", "Standard"}
        };

        [ColumnName("[Context]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)]  
        [FreeTextSearch]
        [NotNull]          
        [DisplayName("Context")]
        public PickList<BatchTypeContext> Context{get;set;}

        [ColumnName("[Type]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)]  
        [FreeTextSearch]
        [NotNull]          
        [DisplayName("Type")]
        public PickList<BatchTypes> Type{get;set;}

        [ColumnName("[Priority]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)]  
        [DisplayName("Priority")]
        public NumericType Priority{get;set;}

        [ColumnName("[IdleTime]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)] 
        [NotNull]       
        [DisplayName("Idle time")]
        public NumericType IdleTime {get;set;}
                
        
        [ColumnName("[ItemTimeout]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)]   
        [NotNull]        
        [DisplayName("Item timeout")]
        public NumericType ItemTimeout {get;set;}

        [ColumnName("[ItemRetryCount]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)]  
        [NotNull]      
        [DisplayName("Item retry count")]
        public NumericType ItemRetryCount {get;set;} 

        [ColumnName("[StartDate]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)] 
        [DisplayName("Start date")]
        public DateTime StartDate{get;set;}

        [ColumnName("[EndDate]")]
        [AccessibleLayout((int)LayoutType.List, (int)LayoutType.Form, (int)LayoutType.View)] 
        [DisplayName("End date")]
        public DateTime EndDate{get;set;}

        [AccessibleLayout(1, 2, 3)]
        [InverseProperty("[Id]")]
        [ColumnName("[SchedulerId]")]        
        [DisplayName("Scheduler")]
        public BatchTypeScheduler Scheduler { get; set; }

        //-----------------------


        [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        [CustomServer (CustomServerContext.BatchItemsQueued)]
        [DisplayName("Queued")]
        public CustomServerInt Queued { get; set; }

        
        [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        [CustomServer (CustomServerContext.BatchItemsErrored)]
        [DisplayName("Errored")]
        public CustomServerInt Errored { get; set; }

        [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        [CustomServer (CustomServerContext.BatchItemsProcessed24Hours)]
        [DisplayName("Processed 24 hours")]
        public CustomServerInt Processed24Hours { get; set; }

        [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        [CustomServer (CustomServerContext.BatchItemsErrored24Hours)]
        [DisplayName("Errored 24 hours")]
        public CustomServerInt Errored24Hours { get; set; }

        // [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        // [CustomClient (CustomClientContext.BatchItemsClientQueued)]
        // public CustomClientInt QueuedClient { get; set; }

        
        // [AccessibleLayout ((int) LayoutType.List, (int) LayoutType.Form)]
        // [CustomClient (CustomClientContext.BatchItemsClientErrored)]
        // public CustomClientString ErroredClient { get; set; }

        //-------------------------
    }

}
