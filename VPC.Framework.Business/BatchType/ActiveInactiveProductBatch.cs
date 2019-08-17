using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    [BatchType((int)BatchTypeContextEnum.ActiveInactiveProduct)]
    public partial class ActiveInactiveProductBatch : IBatchTypes
    {
        void IBatchTypes.OnExecute(dynamic obj)
        {
          // return new BatchTypeReturnMessage{NoDataFound=true} ;
        }
    }
}