using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.Counter;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.Role;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.MetadataManager.Contracts;
namespace VPC.Framework.Business.Counter.Data
{
    internal sealed class DataCounter : EntityModelData
    {

        #region Manage
        internal bool Create(Guid tenantId, CounterInfo info, string entityId)
        {
            try
            {


                var cmd = CreateProcedureCommand("dbo.Counter_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidCounterId", info.CounterId);
                cmd.AppendMediumText("@strText", info.Text);
                cmd.AppendMediumText("@strDescription", info.Description);
                cmd.AppendInt("@intCounterN", info.CounterN.Value);
                cmd.AppendInt("@intCounterO", info.CounterO.Value);
                cmd.AppendInt("@intCounterP", info.CounterP.Value);
                cmd.AppendInt("@intResetCounterN", info.ResetCounterN.Value);
                cmd.AppendInt("@intResetCounterO", info.ResetCounterO.Value);
                cmd.AppendInt("@intResetCounterP", info.ResetCounterP.Value);
                cmd.AppendDateTime("@dateUpdatedOn", DateTime.UtcNow);
                cmd.AppendGuid("@guidUpdatedBy", info.UpdatedBy);
                cmd.AppendMediumText("@strEntityId", entityId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataCounter::Counter_Create");
            }
        }

        internal bool Update(Guid tenantId, CounterInfo info, string entityId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Counter_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidCounterId", info.CounterId);
                cmd.AppendMediumText("@strText", info.Text);
                cmd.AppendMediumText("@strDescription", info.Description);
                cmd.AppendInt("@intCounterN", info.CounterN.Value);
                cmd.AppendInt("@intCounterO", info.CounterO.Value);
                cmd.AppendInt("@intCounterP", info.CounterP.Value);
                cmd.AppendInt("@intResetCounterN", info.ResetCounterN.Value);
                cmd.AppendInt("@intResetCounterO", info.ResetCounterO.Value);
                cmd.AppendInt("@intResetCounterP", info.ResetCounterP.Value);
                cmd.AppendDateTime("@dateUpdatedOn", DateTime.UtcNow);
                cmd.AppendGuid("@guidUpdatedBy", info.UpdatedBy);
                cmd.AppendMediumText("@strEntityId", entityId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataBatchType::Counter_Update");
            }

            // return true;
        }

        internal bool Delete(Guid tenantId, Guid counterId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Counter_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidCounterId", counterId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataCounter::Counter_Delete");
            }

            // return true;
        }

        internal CounterInfo GetCounter(Guid tenantId, Guid counterId)
        {
            CounterInfo counters = null;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Counter_GetById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidCounterId", counterId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        counters = ReadInfo(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataCounter::Counter_GetById");
            }

            return counters;
        }
        internal CounterInfo GetCounters(Guid tenantId, string entityId)
        {
            //  List<CounterInfo> counters = null;
             CounterInfo counters = null;

            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Counter_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendMediumText("@guidEntityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        counters=ReadInfo(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataCounter::Counter_GetById");
            }

            return counters;
        }
        private static CounterInfo ReadInfo(SqlDataReader reader)
        {
            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            var entityId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
            var entityName = _iMetadataManager.GetEntityNameByEntityContext(entityId.ToString());
            var info = new CounterInfo
            {
                CounterId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                Text = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                CounterN = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                CounterO = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                CounterP = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                ResetCounterN = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                ResetCounterO = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                ResetCounterP = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                UpdatedOn = reader.IsDBNull(9) ? DateTime.MinValue : reader.GetDateTime(9),
                UpdatedBy = reader.IsDBNull(10) ? Guid.Empty : reader.GetGuid(10),
                EntityName = entityName,
            };
            return info;
        }

        #endregion

    }
}