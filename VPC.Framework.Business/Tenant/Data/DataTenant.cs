using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VPC.Entities.Tenant;
using VPC.Framework.Business.Data;

namespace VPC.Framework.Business.Rule.Data
{
    internal sealed class DataTenant : EntityModelData
    {
        

        internal TenantLanguageInfo GetTenantLanguageInfo(Guid tenantId)
        {
            TenantLanguageInfo lstTenantInfo = new TenantLanguageInfo();
            try
            {
                var cmd = CreateProcedureCommand("Tenant_GetDefaultLanguageDetails");
                cmd.AppendGuid("@guidTenantId", tenantId);
                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //lstTenantInfo.Add(ReadRule(reader));
                            lstTenantInfo = ReadRule(reader);
                        }
                    }
                    else
                    {
                        var tenantLanguageInfo = new TenantLanguageInfo
                        {
                               Key = Configuration.AlternativeLanguage.GetAltLanguageKey()
                              ,Text = Configuration.AlternativeLanguage.GetAltLanguage()
                        };
                        lstTenantInfo = tenantLanguageInfo;
                    }
                      
                }


            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Tenant::GetTenantLanguageInfo");
            }
            return lstTenantInfo;
        }
        private static TenantLanguageInfo ReadRule(SqlDataReader reader)
        {
            try
            {
                
                //var tenantCode = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                var key = reader.IsDBNull(0) ? String.Empty : reader.GetString(0);
                var text = reader.IsDBNull(1) ? String.Empty : reader.GetString(1);

                if(text == String.Empty || key ==String.Empty)
                {
                    key=Configuration.AlternativeLanguage.GetAltLanguage();
                    text=Configuration.AlternativeLanguage.GetAltLanguage();
                }

                var tenantLanguageInfo = new TenantLanguageInfo
                {

                     Key = key
                    ,Text = text
                };

                return tenantLanguageInfo;
            }
            catch (System.Exception ex)
            {
                throw ex.InnerException;
            }


        }
    }
}
