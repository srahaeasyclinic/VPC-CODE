
using System;
using VPC.Entities.WorkFlow.Engine.Email;

namespace VPC.Entities.BatchType
{
  public class BatchItemContent
    {       
        public Guid BatchItemContentId{get;set;}
        public Guid BatchItemId{get;set;}
        public string Content{get;set;}   
        public string MimeType{get;set;}
        public string Name{get;set;}  
    }

}
