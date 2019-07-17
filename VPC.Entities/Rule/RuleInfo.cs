using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.Rule
{
    public class RuleInfo
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }        
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string RuleName { get; set; }
        public RuleTypeEnum RuleType { get; set; }
        public string RuleTypeName {get;set;}
        public List<SourceInfo> SourceList { get; set; }  
        public List<TargetInfo> TargetList { get; set; }
        [JsonIgnore]
        public string Source { get; set; } //To store and get JSON string of SourceList
        [JsonIgnore]
        public string Target { get; set; } //To store and get JSON string of TargetList
        public Guid UpdatedBy { get; set; }
       
    }
    
    public class SourceInfo
    {
        public string Name { get; set; }
        public string ControlType { get; set; }
        public dynamic Value { get; set; }

    }
    public class TargetInfo
    {
        public string Name { get; set; }
        public string ControlType { get; set; }
        public dynamic Value { get; set; }
    }

   

}
