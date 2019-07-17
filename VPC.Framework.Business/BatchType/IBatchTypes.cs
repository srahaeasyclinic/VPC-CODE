using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    public interface IBatchTypes
    {
        BatchTypeReturnMessage OnExecute(dynamic obj);        
    } 
}