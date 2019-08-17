using System;
using System.Collections.Generic;
using VPC.Entities.BatchType;
using VPC.Entities.WorkFlow.Engine.Email;

namespace VPC.Framework.Business.BatchItems.APIs {
    public interface IManagerBatchItem {
        #region Admin
        bool BatchItemCreate (Guid tenantId, int? itemTimeOut, BatchItem info);
        bool BatchItemUpdate (Guid tenantId, int? itemTimeOut, BatchItem info);
        bool BatchItemUpdateStatus (Guid tenantId, BatchItem info);
        bool BatchItemUpdateStartTime (Guid tenantId, Guid batchItemId);
        bool BatchItemUpdateNextRunTime (Guid tenantId, Guid batchTypeId);

        //-------------------------------------------------------------------------------------------------------------------

        bool BatchContentCreate (Guid tenantId, BatchItemContent info);

        //---------------------------------------------------------------------------------------------------------------------
        bool BatchHistoryCreate (Guid tenantId, BatchItemHistory info);
        #endregion

        #region Review
        List<BatchItem> GetBatchItems (Guid tenantId, Guid batchTypeId, int? retryCount);
        List<BatchItemContent> GetBatchContents (Guid tenantId, Guid batchItemId);
        int BatchItemByStatus (Guid tenantId, Guid batchTypeId, BatchItemTypeEnum status);
        List<BatchItem> GetBatchItemListByStatus (Guid tenantId, Guid batchTypeId, BatchItemTypeEnum status);
        #endregion

    }

    public class ManagerBatchItem : IManagerBatchItem {
        private readonly IReviewBatchItem _review = new ReviewBatchItem ();
        private readonly IAdminBatchItem _admin = new AdminBatchItem ();

        #region Admin
        bool IManagerBatchItem.BatchItemCreate (Guid tenantId, int? itemTimeOut, BatchItem info) {
            return _admin.BatchItemCreate (tenantId, itemTimeOut, info);
        }

        bool IManagerBatchItem.BatchItemUpdate (Guid tenantId, int? itemTimeOut, BatchItem info) {
            return _admin.BatchItemUpdate (tenantId, itemTimeOut, info);
        }

        bool IManagerBatchItem.BatchItemUpdateStatus (Guid tenantId, BatchItem info) {
            return _admin.BatchItemUpdateStatus (tenantId, info);
        }

        bool IManagerBatchItem.BatchItemUpdateStartTime (Guid tenantId, Guid batchItemId) {
            return _admin.BatchItemUpdateStartTime (tenantId, batchItemId);
        }

        bool IManagerBatchItem.BatchItemUpdateNextRunTime (Guid tenantId, Guid batchTypeId) {
            return _admin.BatchItemUpdateNextRunTime (tenantId, batchTypeId);
        }

        //-------------------------------------------------------------------------------------------------------------------

        bool IManagerBatchItem.BatchContentCreate (Guid tenantId, BatchItemContent info) {
            return _admin.BatchContentCreate (tenantId, info);
        }

        //--------------------------------------------------------------------------------------------------------------------

        bool IManagerBatchItem.BatchHistoryCreate (Guid tenantId, BatchItemHistory info) {
            return _admin.BatchHistoryCreate (tenantId, info);
        }
        #endregion  

        #region Review  

        List<BatchItem> IManagerBatchItem.GetBatchItems (Guid tenantId, Guid batchTypeId, int? retryCount) {
            return _review.GetBatchItems (tenantId, batchTypeId, retryCount);
        }

        List<BatchItemContent> IManagerBatchItem.GetBatchContents (Guid tenantId, Guid batchItemId) {
            return _review.GetBatchContents (tenantId, batchItemId);
        }

        int IManagerBatchItem.BatchItemByStatus (Guid tenantId, Guid batchTypeId, BatchItemTypeEnum status) {
            return _review.BatchItemByStatus (tenantId, batchTypeId, status);
        }

        List<BatchItem> IManagerBatchItem.GetBatchItemListByStatus (Guid tenantId, Guid batchTypeId, BatchItemTypeEnum status) {
            return _review.GetBatchItemListByStatus (tenantId, batchTypeId, status);
        }

        #endregion
    }
}