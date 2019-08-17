
using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;
using VPC.Entities.WorkFlow.Engine.Email;

namespace VPC.Entities.BatchType
{
  public class BatchItem
    {       
        public Guid BatchItemId{get;set;}
        public Guid BatchTypeId{get;set;}
        public string Name{get;set;}
        public int? Priority{get;set;}
        public int? RetryCount{get;set;}
        public DateTime NextRunTime{get;set;}
        public string EntityId{get;set;}
        public Guid ReferenceId{get;set;}
        public EmailEnum Status{get;set;}
        public string FailedReason{get;set;}
        public AuditDetail AuditDetails{get;set;}
        public DateTime? StartTime{get;set;}
        public DateTime? EndTime{get;set;}
        public List<BatchItemContent> BatchContent{get;set;}

        
    }

}
