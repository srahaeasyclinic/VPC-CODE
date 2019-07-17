using System;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType
{
    [BatchTypeAttribute("Product",BatchTypeContext.Product,(int)BatchTypes.Scheduled)]
    public partial class ProductBatch : IBatchTypes
    {
        BatchTypeReturnMessage IBatchTypes.OnExecute(dynamic obj)
        {
           return new BatchTypeReturnMessage{NoDataFound=true} ;
        }
    }
}