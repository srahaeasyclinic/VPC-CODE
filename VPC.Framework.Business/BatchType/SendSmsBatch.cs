using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    [BatchType((int)BatchTypeContextEnum.SMS)]
    public partial class SendSmsBatch : IBatchTypes
    {
        void IBatchTypes.OnExecute(dynamic obj)
        {
              //return new BatchTypeReturnMessage{Success=true} ;
        }
    }
}