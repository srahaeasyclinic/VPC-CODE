
using System;
using VPC.Entities.WorkFlow.Engine.Email;

namespace VPC.Entities.BatchType
{
    public class BatchItemHistory
    {       
        public Guid BatchHistoryId{get;set;}
        public Guid BatchItemId{get;set;}
        public string EntityId{get;set;}
        public Guid ReferenceId{get;set;}
        public EmailEnum Status{get;set;}
        public string FailedReason{get;set;}
        public DateTime RunTime{get;set;}

        
    }

}
