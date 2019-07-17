using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    [BatchTypeAttribute("SMS",BatchTypeContext.SMS,(int)BatchTypes.Recurrence)]
    public partial class SmsBatch : IBatchTypes
    {
        BatchTypeReturnMessage IBatchTypes.OnExecute(dynamic obj)
        {
              return new BatchTypeReturnMessage{Success=true} ;
        }
    }
}