using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    public interface IBatchTypes
    {
        void OnExecute(dynamic obj);        
    } 
}