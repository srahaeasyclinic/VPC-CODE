
using System;
using System.ComponentModel;
using VPC.Entities.Common;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.BatchType
{
  public  class BatchTypeInfo
    {   
        public string Name{get;set;}    
        public Guid BatchTypeId{get;set;} 
        public string Context{get;set;}
        public int? Priority{get;set;}
        public int? IdleTime {get;set;}
        public bool Status{get;set;}
        public ItemNameInt RunningType{get;set;}      
        
    }

}
