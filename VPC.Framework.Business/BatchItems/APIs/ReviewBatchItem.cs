using System;
using System.Collections.Generic;
using VPC.Entities.BatchType;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.Data;

namespace VPC.Framework.Business.BatchItems.APIs
{
    public interface IReviewBatchItem
    {   
        List<BatchItem> GetBatchItems(Guid tenantId,Guid batchTypeId,int? retryCount);    
        List<BatchItemContent> GetBatchContents(Guid tenantId,Guid batchItemId);
        int BatchItemByStatus(Guid tenantId,Guid batchTypeId,BatchItemTypeEnum status);
        List<BatchItem> GetBatchItemListByStatus(Guid tenantId,Guid batchTypeId,BatchItemTypeEnum status);
    }
    
    internal  class ReviewBatchItem : IReviewBatchItem
    {
        private readonly DataBatchItem _data = new DataBatchItem();       

        List<BatchItemContent> IReviewBatchItem.GetBatchContents(Guid tenantId, Guid batchItemId)
        {
             return _data.GetBatchContents(tenantId,batchItemId);
        }

        List<BatchItem> IReviewBatchItem.GetBatchItems(Guid tenantId,Guid batchTypeId, int? retryCount)
        {
            return _data.GetBatchItems(tenantId,batchTypeId,retryCount);
        }  

        int IReviewBatchItem.BatchItemByStatus(Guid tenantId, Guid batchTypeId, BatchItemTypeEnum status)
        {
            return _data.BatchItemByStatus(tenantId,batchTypeId,status);
        }

        List<BatchItem> IReviewBatchItem.GetBatchItemListByStatus(Guid tenantId,Guid batchTypeId,BatchItemTypeEnum status)
        {
           return _data.GetBatchItemListByStatus(tenantId,batchTypeId,status);
        }
    }
}