using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Entities.BatchType;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Framework.Business.BatchType.Data
{
    internal sealed class DataBatchType : EntityModelData
    {
        
    //     #region Manage
    //     internal bool Create(Guid tenantId, BatchTypeInfo info)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.BatchType_Create");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidBatchTypeId",info.BatchTypeId);
    //             cmd.AppendMediumText("@strContext", info.Context); 
    //             if(info.Priority.HasValue)
    //             cmd.AppendInt("@intPriority", info.Priority.Value);
    //             if(info.IdleTime.HasValue)
    //             cmd.AppendInt("@intIdleTime", info.IdleTime.Value);
    //             cmd.AppendBit("@bStatus", info.Status); 
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_Create");
    //         }
    //     } 

    //     internal bool Update(Guid tenantId, BatchTypeInfo info)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.BatchType_Update");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidBatchTypeId",info.BatchTypeId);
    //             cmd.AppendMediumText("@strContext", info.Context);
    //             if(info.Priority.HasValue)
    //             cmd.AppendInt("@intPriority", info.Priority.Value);
    //             if(info.IdleTime.HasValue && info.IdleTime>0)
    //             cmd.AppendInt("@intIdleTime", info.IdleTime.Value); 
    //             if(info.ItemTimeout.HasValue)  
    //             cmd.AppendInt("@intItemTimeout", info.ItemTimeout.Value);
    //             if(info.ItemRetryCount.HasValue)
    //             cmd.AppendInt("@intItemRetryCount", info.ItemRetryCount.Value);             
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_Update");
    //         }
    //     } 

    //     internal bool CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.BatchType_Create_Xml");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendXml("@xmlBatchTypes", DataUtility.GetXmlForBatchTypes(infos));            
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_Create_Xml");
    //         }
    //     } 

    //     internal bool Delete(Guid tenantId, Guid batchTypeId)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.BatchType_Delete");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidBatchTypeId", batchTypeId);                    
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_Delete");
    //         }
    //     }

    //     internal bool UpdateStatus(Guid tenantId, Guid batchTypeId)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.BatchType_Status");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidBatchTypeId", batchTypeId);                     
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_Status");
    //         }
    //     }


        

    //     internal List<BatchTypeInfo> GetBatchTypes(Guid tenantId)
    //     {
    //         var batches = new List<BatchTypeInfo>();

    //         try
    //         {
    //             SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetAll");
    //             cmd.AppendGuid("@guidTenantId", tenantId);            
    //             using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
    //             {
    //                while (reader.Read())
    //                     {
    //                         batches.Add(ReadInfo(reader));
    //                     } 
    //             }
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAll");
    //         }

    //         return batches;
    //     }

    //    internal List<KeyValuePair<Guid,BatchTypeInfo>>  GetEnabledBatchTypes()
    //     {
    //        var batches = new List<KeyValuePair<Guid,BatchTypeInfo>>(); 
    //        try
    //         {
    //             SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetAllEnabled");                        
    //             using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
    //             {
    //                while (reader.Read())
    //                     {                         
    //                       batches.Add(new KeyValuePair<Guid, BatchTypeInfo>(reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0), 
    //                                         new BatchTypeInfo
    //                                             {
    //                                                 BatchTypeId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
    //                                                 Context = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
    //                                                 Priority =reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
    //                                                 IdleTime = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),            
    //                                                 Status = reader.IsDBNull(5) ? false : reader.GetBoolean(5),
    //                                                 ItemTimeout=reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
    //                                                 ItemRetryCount=reader.IsDBNull(7) ? (int?)null : reader.GetInt16(7),
    //                                             }
    //                           ));   
    //                     }
    //             }
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAllEnabled");
    //         } 
    //         return batches;   
    //     }

    //      internal BatchTypeInfo GetBatchType(Guid tenantId,Guid batchTypeId)
    //     {
    //         BatchTypeInfo batches =null;
    //         try
    //         {
    //             SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetById");
    //             cmd.AppendGuid("@guidTenantId", tenantId);       
    //              cmd.AppendGuid("@guidBatchTypeId", batchTypeId);          
    //             using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
    //             {
    //                while (reader.Read())
    //                     {
    //                         batches=ReadInfo(reader);
    //                     } 
    //             }
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetById");
    //         }

    //         return batches;
    //     }
    //      private static BatchTypeInfo ReadInfo(SqlDataReader reader)
    //     {
    //         var info = new BatchTypeInfo
    //         {
    //             BatchTypeId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
    //             Context = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
    //             Priority =reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
    //             IdleTime = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),            
    //             Status = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
    //             ItemTimeout =reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
    //             ItemRetryCount = reader.IsDBNull(6) ? (int?)null : reader.GetInt16(6),
    //         };
    //         return info;
    //     }


    //  #endregion



     #region New

     internal List<VPC.Entities.BatchType.BatchType>  GetEnabledBatchType()
        {
           var batches = new List<VPC.Entities.BatchType.BatchType>(); 
           try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetAllEnabled");                        
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    //var aaa=new VPC.Entities.BatchType.BatchType();
                   // aaa.TenantId.Value="ajay";
                   while (reader.Read())
                        {                         
                          batches.Add(ReadBatchType(reader));   
                        }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAllEnabled");
            } 
            return batches;   
        }

        internal VPC.Entities.BatchType.BatchType GetBatchTypeByContext(Guid tenantId,BatchTypeContextEnum context)
        {
            VPC.Entities.BatchType.BatchType batche = null;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetByContext");
                cmd.AppendGuid("@guidTenantId", tenantId);  
                cmd.AppendSmallInt("@siContext", (short)context);           
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                           batche=ReadBatchType(reader);   
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetByContext");
            }

            return batche;
        }

        private static VPC.Entities.BatchType.BatchType ReadBatchType(SqlDataReader reader)
        {
            var info = new VPC.Entities.BatchType.BatchType();
            var tenantId=reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
            var id=reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
            BatchTypeContextEnum context  = reader.IsDBNull(2) ? BatchTypeContextEnum.Email : (BatchTypeContextEnum)reader.GetInt16(2);
            BatchTypeEnum type  = reader.IsDBNull(3) ? BatchTypeEnum.Indefinite : (BatchTypeEnum)reader.GetByte(3);
            byte? priority =reader.IsDBNull(4) ? (byte?)null : reader.GetByte(4);
            int? idleTime =reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5);
            int? itemTimeOut =reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6);
            int? itemRetryCount =reader.IsDBNull(7) ? (int?)null : reader.GetInt16(7);
            System.DateTime? startDate =reader.IsDBNull(8) ? (System.DateTime?)null : reader.GetDateTime(8);
            System.DateTime? endDate =reader.IsDBNull(9) ? (System.DateTime?)null : reader.GetDateTime(9);
            var schedulerId=reader.IsDBNull(10) ? Guid.Empty : reader.GetGuid(10);

            info.TenantId=new InternalId();
            info.TenantId.Value=tenantId.ToString();

            info.InternalId=new InternalId();
            info.InternalId.Value=id.ToString();

            info.Context=new PickList<BatchTypeContext>();
            info.Context.Value = ((int)context).ToString();

            info.Type=new PickList<BatchTypes>();
            info.Type.Value = ((int)type).ToString();

            info.Priority=new NumericType();
            info.Priority.Value =priority.HasValue ?  priority.ToString() : string.Empty; 

            info.IdleTime=new NumericType();
            info.IdleTime.Value = idleTime.HasValue ?  idleTime.ToString() : string.Empty; 

            info.ItemTimeout=new NumericType();
            info.ItemTimeout.Value = itemTimeOut.HasValue ?  itemTimeOut.ToString() : string.Empty; 

            info.ItemRetryCount=new NumericType();
            info.ItemRetryCount.Value = itemRetryCount.HasValue ?  itemRetryCount.ToString() : string.Empty; 

            info.StartDate=new VPC.Metadata.Business.DataTypes.DateTime();
            info.StartDate.Value =startDate.ToString();

            info.EndDate=new VPC.Metadata.Business.DataTypes.DateTime();
            info.EndDate.Value =endDate.ToString();

            info.Scheduler=new BatchTypeScheduler();
            info.Scheduler.InternalId=new InternalId();
            info.Scheduler.InternalId.Value =schedulerId.ToString();       
            return info;
        }

        #endregion
   
   
    }
}
