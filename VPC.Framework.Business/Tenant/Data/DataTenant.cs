using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VPC.Entities.Tenant;
using VPC.Framework.Business.Data;

namespace VPC.Framework.Business.Rule.Data
{
    internal sealed class DataTenant : EntityModelData
    {
        

        internal List<TenantInfo> GetTenantInfo(Guid tenantId)
        {
            List<TenantInfo> lstTenantInfo = new List<TenantInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Tenant_GetDefaultLanguageDetails");
                cmd.AppendGuid("@guidTenantId", tenantId);
                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTenantInfo.Add(ReadRule(reader));
                    }
                }


            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Tenant::GetTenantInfo");
            }
            return lstTenantInfo;
        }
        private static TenantInfo ReadRule(SqlDataReader reader)
        {
            try
            {
                
                var tenantCode = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                var tenent_Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                var orgNo = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                var pickListValue_Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                var pickListId = reader.IsDBNull(4) ? 0 : reader.GetInt16(4);
                var key = reader.IsDBNull(5) ? String.Empty : reader.GetString(5);
                var text = reader.IsDBNull(6) ? String.Empty : reader.GetString(6);
                var languageId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);

                var tenantInfo = new TenantInfo
                {
                    TenantCode = tenantCode
                    ,TenantId = tenent_Id
                    ,OrgNo = orgNo
                    ,PickListValue_Id = pickListValue_Id
                    ,PickListId = pickListId
                    ,Key = key
                    ,Text = text
                    ,LanguageId = languageId
                    ,DefaultLanguage = text
                };

                return tenantInfo;
            }
            catch (System.Exception ex)
            {
                throw ex.InnerException;
            }


        }
    }
}
