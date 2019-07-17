using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.Common;
using VPC.Entities.BatchType;

namespace VPC.Framework.Business.BatchType.Data
{
    internal sealed class DataBatchType : EntityModelData
    {
        
        #region Manage
        internal bool Create(Guid tenantId, BatchTypeInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchType_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchTypeId",info.BatchTypeId);
                cmd.AppendMediumText("@strContext", info.Context); 
                if(info.Priority.HasValue)
                cmd.AppendInt("@intPriority", info.Priority.Value);
                if(info.IdleTime.HasValue)
                cmd.AppendInt("@intIdleTime", info.IdleTime.Value);
                cmd.AppendBit("@bStatus", info.Status);                            
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_Create");
            }
        } 

        internal bool Update(Guid tenantId, BatchTypeInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchType_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchTypeId",info.BatchTypeId);
                cmd.AppendMediumText("@strContext", info.Context);
                if(info.Priority.HasValue)
                cmd.AppendInt("@intPriority", info.Priority.Value);
                if(info.IdleTime.HasValue && info.IdleTime>0)
                cmd.AppendInt("@intIdleTime", info.IdleTime.Value);                
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_Update");
            }
        } 

        internal bool CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchType_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlBatchTypes", DataUtility.GetXmlForBatchTypes(infos));            
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_Create_Xml");
            }
        } 

        internal bool Delete(Guid tenantId, Guid batchTypeId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchType_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);                    
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_Delete");
            }
        }

        internal bool UpdateStatus(Guid tenantId, Guid batchTypeId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.BatchType_Status");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);                     
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_Status");
            }
        }


        internal List<BatchTypeInfo> GetBatchTypes(Guid tenantId)
        {
            var batches = new List<BatchTypeInfo>();

            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);            
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
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAll");
            }

            return batches;
        }

       internal List<KeyValuePair<Guid,BatchTypeInfo>>  GetEnabledBatchTypes()
        {
           var batches = new List<KeyValuePair<Guid,BatchTypeInfo>>(); 
           try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetAllEnabled");                        
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {                         
                          batches.Add(new KeyValuePair<Guid, BatchTypeInfo>(reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0), 
                                            new BatchTypeInfo
                                                {
                                                    BatchTypeId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                                                    Context = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                                    Priority =reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                                    IdleTime = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),            
                                                    Status = reader.IsDBNull(5) ? false : reader.GetBoolean(5)
                                                }
                              ));   
                        }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAllEnabled");
            } 
            return batches;   
        }

         internal BatchTypeInfo GetBatchType(Guid tenantId,Guid batchTypeId)
        {
            BatchTypeInfo batches =null;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.BatchType_GetById");
                cmd.AppendGuid("@guidTenantId", tenantId);       
                 cmd.AppendGuid("@guidBatchTypeId", batchTypeId);          
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                   while (reader.Read())
                        {
                            batches=ReadInfo(reader);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::BatchType_GetAll");
            }

            return batches;
        }
         private static BatchTypeInfo ReadInfo(SqlDataReader reader)
        {
            var info = new BatchTypeInfo
            {
                BatchTypeId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                Context = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Priority =reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                IdleTime = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),            
                Status = reader.IsDBNull(4) ? false : reader.GetBoolean(4)
            };
            return info;
        }


     #endregion
   
   
    }
}
