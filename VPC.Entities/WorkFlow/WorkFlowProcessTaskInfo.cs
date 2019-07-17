using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowProcessTaskInfo
    {
        public Guid WorkFlowProcessTaskId { get; set; }
        public Guid WorkFlowId{get;set;}
        public Guid WorkFlowProcessId { get; set; }
        public Guid ProcessCode { get; set; }
        public string ProcessName{get;set;}
        public int SequenceCode { get; set; }

        public int ProcessType { get; set; }    
               
    }
}