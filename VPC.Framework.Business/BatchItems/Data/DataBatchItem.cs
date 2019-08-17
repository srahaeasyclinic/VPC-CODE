using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.Data;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchItems.Data
{
    internal sealed class DataBatchItem : EntityModelData
    { 
        #region BatchItem
        internal bool BatchItemCreate(Guid tenantId,int? itemTimeOut, BatchItem info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItem_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);  
                if(itemTimeOut.HasValue)
                cmd.AppendInt("@intItemTimeOut",itemTimeOut.Value);              
                cmd.AppendGuid("@guidBatchItemId",info.BatchItemId);
                cmd.AppendGuid("@guidBatchTypeId",info.BatchTypeId);
                cmd.AppendMediumText("@strName", info.Name); 
                if(info.Priority.HasValue)               
                cmd.AppendTinyInt("@tiPriority",(byte)info.Priority.Value);
                if(info.RetryCount.HasValue)
                cmd.AppendSmallInt("@siRetryCount",(short)info.RetryCount.Value);    
                if(info.NextRunTime!=DateTime.MinValue)           
                    cmd.AppendDateTime("@dtNextRunTime", info.NextRunTime); 
                cmd.AppendXSmallText("@strEntityId", info.EntityId); 
                cmd.AppendGuid("@guidReferenceId",info.ReferenceId);
                cmd.AppendTinyInt("@tiStatus", (byte)info.Status);
                cmd.AppendXLargeText("@strFailedReason", info.FailedReason); 
                if(info.AuditDetails!=null)
                {
                    if(info.AuditDetails.CreatedBy!=Guid.Empty)
                       cmd.AppendGuid("@guidCreatedBy", info.AuditDetails.CreatedBy);
                }
                if(info.StartTime.HasValue && info.StartTime!=DateTime.MinValue)
                    cmd.AppendDateTime("@dtStartTime", info.StartTime.Value);
                if(info.EndTime.HasValue && info.EndTime!=DateTime.MinValue)
                   cmd.AppendDateTime("@dtEndTime", info.EndTime.Value);
                                           
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_Create");
            }
        } 

         internal bool BatchItemUpdate(Guid tenantId,int? itemTimeOut, BatchItem info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItem_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);  
                if(itemTimeOut.HasValue)
                   cmd.AppendInt("@intItemTimeOut",itemTimeOut.Value);               
                cmd.AppendGuid("@guidBatchItemId",info.BatchItemId);
                cmd.AppendTinyInt("@tiStatus", (byte)info.Status);
                cmd.AppendXLargeText("@strFailedReason", info.FailedReason);                                
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_Update");
            }
        }

        internal bool BatchItemUpdateStatus(Guid tenantId, BatchItem info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItem_UpdateStatus");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidBatchItemId",info.BatchItemId); 
                cmd.AppendTinyInt("@tiStatus", (byte)info.Status);                                          
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_UpdateStatus");
            }
        }

        internal bool BatchItemUpdateStartTime(Guid tenantId, Guid batchItemId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItem_UpdateStartTime");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidBatchItemId",batchItemId);                                                
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_UpdateStartTime");
            }
        }

        internal bool BatchItemUpdateNextRunTime(Guid tenantId,Guid batchTypeId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItem_UpdateNextRunTime");
                cmd.AppendGuid("@guidTenantId", tenantId); 
                cmd.AppendGuid("@guidBatchTypeId",batchTypeId);
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_UpdateNextRunTime");
            }
        }

        internal int BatchItemByStatus(Guid tenantId,Guid batchTypeId,BatchItemTypeEnum status)
        {
            var itemCount=0;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchItem_GetByStatus");
                cmd.AppendGuid("@guidTenantId", tenantId);  
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);  
                cmd.AppendTinyInt("@tiStatus", (byte)status);           
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                            itemCount=reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_GetByStatus");
            }

            return itemCount;
        }
       
        internal List<BatchItem> GetBatchItems(Guid tenantId,Guid batchTypeId,int? retryCount)
        {
            List<BatchItem> batches =new List<BatchItem>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchItem_Get");
                cmd.AppendGuid("@guidTenantId", tenantId); 
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);   
                if(retryCount.HasValue)     
                 cmd.AppendInt("@intRetryCount", retryCount.Value);          
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                            batches.Add(ReadInfo(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_Get");
            }

            return batches;
        }

        internal List<BatchItem> GetBatchItemListByStatus(Guid tenantId,Guid batchTypeId,BatchItemTypeEnum status)
        {
            List<BatchItem> batches =new List<BatchItem>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchItem_GetList_ByStatus");
                cmd.AppendGuid("@guidTenantId", tenantId);  
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);  
                cmd.AppendTinyInt("@tiStatus", (byte)status);         
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                            batches.Add(ReadInfo(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItem_GetList_ByStatus");
            }

            return batches;
        }
        private static BatchItem ReadInfo(SqlDataReader reader)
        {
            var info = new BatchItem
            {
                BatchItemId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                BatchTypeId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                Priority =reader.IsDBNull(3) ? 0 : reader.GetByte(3),
                RetryCount = reader.IsDBNull(4) ? 0 : reader.GetInt16(4),   
                NextRunTime = reader.IsDBNull(5) ? DateTime.UtcNow : reader.GetDateTime(5),
                EntityId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),  
                ReferenceId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7),            
                Status = reader.IsDBNull(8) ? EmailEnum.ReadyToSend : (EmailEnum)reader.GetByte(8),
                FailedReason = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                AuditDetails=new Entities.Common.AuditDetail{
                     CreatedBy = reader.IsDBNull(10) ? Guid.Empty : reader.GetGuid(10),
                     CreationDate = reader.IsDBNull(11) ? DateTime.UtcNow : reader.GetDateTime(11),
                },               
                StartTime = reader.IsDBNull(12) ? DateTime.UtcNow : reader.GetDateTime(12),
                EndTime = reader.IsDBNull(13) ? DateTime.UtcNow : reader.GetDateTime(13)
            };
            return info;
        }
       
        #endregion 

        #region BatchContent

       internal bool BatchContentCreate(Guid tenantId, BatchItemContent info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchItemContent_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchItemContentId",info.BatchItemContentId);
                cmd.AppendGuid("@guidBatchItemId",info.BatchItemId);
                cmd.AppendXLargeText("@strContent", info.Content);     
                cmd.AppendMediumText("@strName", info.Name); 
                cmd.AppendSmallText("@strMimeType", info.MimeType);                                       
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItemContent_Create");
            }
        }

      

         internal List<BatchItemContent> GetBatchContents(Guid tenantId,Guid batchItemId)
        {
            List<BatchItemContent> batches =new List<BatchItemContent>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchItemContent_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);       
                cmd.AppendGuid("@guidBatchItemId", batchItemId);           
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                            batches.Add(ReadBatchContent(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchItemContent_Get");
            }

            return batches;
        } 

        private static BatchItemContent ReadBatchContent(SqlDataReader reader)
        {
            var info = new BatchItemContent
            {
                BatchItemContentId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                BatchItemId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                Content = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),                            
                Name = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), 
                MimeType = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
            };
            return info;
        }
        

        #endregion

        #region History
        internal bool BatchHistoryCreate(Guid tenantId, BatchItemHistory info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchHistory_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchHistoryId",info.BatchHistoryId);
                cmd.AppendGuid("@guidBatchItemId",info.BatchItemId);
                cmd.AppendXSmallText("@strEntityId", info.EntityId); 
                cmd.AppendGuid("@guidReferenceId",info.ReferenceId);
                cmd.AppendTinyInt("@tinyintStatus", (byte)info.Status);
                if(!string.IsNullOrEmpty(info.FailedReason))
                    cmd.AppendXLargeText("@strFailedReason", info.FailedReason); 
                cmd.AppendDateTime("@dtRunTime", info.RunTime);                            
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchItem::BatchHistory_Create");
            }
        } 
        
        #endregion

   
   
    }
}
