using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    [BatchTypeAttribute("User",BatchTypeContext.User,(int)BatchTypes.OneTime)]
    public partial class UserBatch : IBatchTypes
    {
        BatchTypeReturnMessage IBatchTypes.OnExecute(dynamic obj)
        {
            return new BatchTypeReturnMessage{Success=true} ;
        }
    }
} 