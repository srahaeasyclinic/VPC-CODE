using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Credential;
using VPC.Entities.Role;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Credential.Data
{
    internal sealed class DataCredentialHistory : EntityModelData
    {
        #region CredentialHistory
        internal List<CredentialHistory> GetCredentialHistory(Guid tenantId, Guid refId, int count)
        {
            var lstCredential = new List<CredentialHistory>();
            // List<CredentialHistory> lstCredential = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.CredentialHistory_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRefId", refId);
                cmd.AppendInt("@intCount", count);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstCredential.Add(ReadInfo(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "CredentialHistory::CredentialHistory_Get");
            }
            return lstCredential;
        }

        private static CredentialHistory ReadInfo(SqlDataReader reader)
        {
            var crdhistory = new CredentialHistory
            {
                CredentialHistoryId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                ParentId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                UserName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                PasswordHash = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                PasswordSalt = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                // CreatedDate = reader.IsDBNull(5) ? DateTime.UtcNow : DateTime.SpecifyKind((DateTime)reader.GetSqlDateTime(5), DateTimeKind.Utc) 
                CreatedDate = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
            };
            return crdhistory;
        }
        #endregion
    }
}
