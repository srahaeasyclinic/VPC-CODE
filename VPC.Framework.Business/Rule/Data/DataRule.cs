using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.Common;
using VPC.Entities.Rule;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.Rule.Data
{
    internal sealed class DataRule : EntityModelData
    {
        
        
     
        #region Review
        internal List<RuleInfo> GetAllRules(Guid tenantId, string entityId = null)
        {
            List<RuleInfo> lstRules = new List<RuleInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Rule_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstRules.Add(ReadRule(reader));
                    }
                }


            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Rule::Rule_GetAll");
            }
            return lstRules;
        }
        
        internal RuleInfo GetById(Guid tenantId, Guid ruleId, string entityId = null)
        {
            RuleInfo ruleLst = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.Rule_GetById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", ruleId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        ruleLst = ReadRule(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Rule::Rule_GetById");
            }
            return ruleLst;
        }
        private static RuleInfo ReadRule(SqlDataReader reader)
        {
            try
            {
                IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
                List<SourceInfo> lstSource = reader.IsDBNull(4) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<SourceInfo>>(reader.GetString(4));
                List<TargetInfo> lstTarget = reader.IsDBNull(5) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<TargetInfo>>(reader.GetString(5));
                RuleTypeEnum ruleType = reader.IsDBNull(3) ? RuleTypeEnum.Visibility : (RuleTypeEnum)reader.GetInt32(3);
                string strRuleTypeName = ruleType.ToString();
                string entityId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                string entityName = (entityId == null) ? string.Empty : iMetadataManager.GetEntityNameByEntityContext(entityId);

                var rule = new RuleInfo
                {
                    TenantId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    RuleName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    RuleType = reader.IsDBNull(3) ? RuleTypeEnum.Visibility : (RuleTypeEnum)reader.GetInt32(3),
                    Source = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    Target = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    SourceList = lstSource,
                    TargetList = lstTarget,
                    UpdatedBy = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7),
                    RuleTypeName = strRuleTypeName,
                    EntityId = entityId,
                    EntityName = entityName

                };
                return rule;
            }
            catch(System.Exception ex)
            {
                throw ex.InnerException;
            }

            
        }

        #endregion

        #region Manage
        internal bool Create(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Rule_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", ruleInfo.Id);
                cmd.AppendMediumText("@strRuleName", ruleInfo.RuleName);
                cmd.AppendSmallInt("@intRuleType", (Int16)ruleInfo.RuleType);
                cmd.AppendXLargeText("@strSource", ruleInfo.Source);
                cmd.AppendXLargeText("@strTarget", ruleInfo.Target);
                cmd.AppendGuid("@guidUpdatedBy", ruleInfo.UpdatedBy);
                cmd.AppendXSmallText("@strEntityId", ruleInfo.EntityId);
                cmd.AppendVarChar("@strMessage",100);

                int retVal = ExecuteCommand(cmd);               

                strMsg = cmd.OutputParameterValue("@strMessage");
                if (!String.IsNullOrEmpty(strMsg))
                {                    
                    return false;
                }

                strMsg = "Data saved successfully";
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Rule::Rule_Create");
            }
        }

        internal bool Update(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Rule_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", ruleInfo.Id);
                cmd.AppendMediumText("@strRuleName", ruleInfo.RuleName);
                cmd.AppendSmallInt("@intRuleType", (Int16)ruleInfo.RuleType);
                cmd.AppendXLargeText("@strSource", ruleInfo.Source);
                cmd.AppendXLargeText("@strTarget", ruleInfo.Target);
                cmd.AppendGuid("@guidUpdatedBy", ruleInfo.UpdatedBy);
                cmd.AppendXSmallText("@strEntityId", ruleInfo.EntityId);
                cmd.AppendVarChar("@strMessage", 100);

                int retVal = ExecuteCommand(cmd);
                strMsg = cmd.OutputParameterValue("@strMessage");
                if (!String.IsNullOrEmpty(strMsg))
                {
                    return false;
                }

                strMsg = "Data updated successfully";
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Rule::Rule_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid ruleId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Rule_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", ruleId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Rule::Rule_Delete");
            }
        }


        #endregion
    }
}
