using System;
using VPC.Entities.BatchType;
using VPC.Framework.Business.BatchItems.Data;

namespace VPC.Framework.Business.BatchItems.APIs
{
    public interface IAdminBatchItem
    {    
        bool BatchItemCreate(Guid tenantId, int? itemTimeOut,BatchItem info);
        bool BatchItemUpdate(Guid tenantId,int? itemTimeOut, BatchItem info);
        bool BatchItemUpdateStatus(Guid tenantId, BatchItem info);
         bool BatchItemUpdateStartTime(Guid tenantId, Guid batchItemId);
         bool BatchItemUpdateNextRunTime(Guid tenantId,Guid batchTypeId);
        //---------------------------------------------
        bool BatchContentCreate(Guid tenantId, BatchItemContent info);   
        bool BatchHistoryCreate(Guid tenantId, BatchItemHistory info);
   
    
    }
    
    internal  class AdminBatchItem : IAdminBatchItem
    {
        private readonly DataBatchItem _data = new DataBatchItem();

        bool IAdminBatchItem.BatchItemCreate(Guid tenantId,int? itemTimeOut, BatchItem info)
        {
            return _data.BatchItemCreate(tenantId,itemTimeOut,info);
        }
        bool IAdminBatchItem.BatchItemUpdate(Guid tenantId,int? itemTimeOut, BatchItem info)
        {
            return _data.BatchItemUpdate(tenantId,itemTimeOut,info);
        }

        bool IAdminBatchItem.BatchItemUpdateStatus(Guid tenantId, BatchItem info)
        {
            return _data.BatchItemUpdateStatus(tenantId,info);
        }

         bool IAdminBatchItem.BatchItemUpdateStartTime(Guid tenantId, Guid batchItemId)
         {
               return _data.BatchItemUpdateStartTime(tenantId,batchItemId);
         }

         bool IAdminBatchItem.BatchItemUpdateNextRunTime(Guid tenantId,Guid batchTypeId)
         {
              return _data.BatchItemUpdateNextRunTime(tenantId, batchTypeId);
         }

        //---------------------------------------------------------------------

        bool IAdminBatchItem.BatchContentCreate(Guid tenantId, BatchItemContent infos)
        {
           return _data.BatchContentCreate(tenantId,infos);
        }

        bool IAdminBatchItem.BatchHistoryCreate(Guid tenantId, BatchItemHistory info)
        {
            return _data.BatchHistoryCreate(tenantId,info);
        }

      
    }
}