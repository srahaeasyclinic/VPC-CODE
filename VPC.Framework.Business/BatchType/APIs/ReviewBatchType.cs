using System;
using System.Collections.Generic;
using VPC.Entities.BatchType;
using VPC.Entities.Role;
using VPC.Framework.Business.BatchType.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.BatchType.APIs
{
 public interface IReviewBatchType
    {    
        List<BatchTypeInfo> GetBatchTypes(Guid tenantId);
        BatchTypeInfo GetBatchType(Guid tenantId,Guid batchTypeId);
        List<KeyValuePair<Guid,BatchTypeInfo>>  GetEnabledBatchTypes();
    }
    
    internal  class ReviewBatchType : IReviewBatchType
    {
        private readonly DataBatchType _data = new DataBatchType();

        BatchTypeInfo IReviewBatchType.GetBatchType(Guid tenantId, Guid batchTypeId)
        {
          return _data.GetBatchType(tenantId,batchTypeId);
        }

        List<BatchTypeInfo> IReviewBatchType.GetBatchTypes(Guid tenantId)
        {
            return _data.GetBatchTypes(tenantId);
        }

        List<KeyValuePair<Guid,BatchTypeInfo>>  IReviewBatchType.GetEnabledBatchTypes()
        {
          return _data.GetEnabledBatchTypes();  
        }
    }
}