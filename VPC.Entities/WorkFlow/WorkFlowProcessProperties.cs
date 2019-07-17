using System;
using System.Collections.Generic;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowProcessProperties
    { 
        public string EntityName { get; set; }     
        public Guid UserId { get; set; }     
        public bool IsSuperAdmin { get; set; }           
        public string  SubTypeCode { get; set; }
        public Guid resultId{get;set;}

        public Guid CurrentTransitionTypeId{get;set;}
    }
}