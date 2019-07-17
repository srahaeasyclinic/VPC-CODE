using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Credential;
using VPC.Entities.Role;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Credential.Data {
    internal sealed class DataCredential : EntityModelData {
        #region Review
        internal Guid GetUserName (Guid tenantId, string userName) {
            Guid userId = Guid.Empty;
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Validate_UserName");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendMediumText ("@strUserName", userName);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        userId = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                    }
                }
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential :: Credential_Validate_UserName");
            }
            return userId;
        }

        internal CredentialInfo GetPassword (Guid tenantId, string userName) {
            CredentialInfo passwordVal = null;
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Get_Password");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendMediumText ("@strUserName", userName);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                    passwordVal = new CredentialInfo {
                    PasswordHash = reader.IsDBNull (0) ? string.Empty : reader.GetString (0),
                    PasswordSalt = reader.IsDBNull (1) ? string.Empty : reader.GetString (1)
                        };
                    }
                }
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential :: Credential_Get_Password");
            }
            return passwordVal;
        }

        internal CredentialInfo GetCredential (Guid tenantId, Guid refId) {
            CredentialInfo info = null;
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Get");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidRefId", refId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        info = ReadInfo (reader);
                    }
                }
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential::Credential_Get");
            }
            return info;
        }

        private static CredentialInfo ReadInfo (SqlDataReader reader) {
            var role = new CredentialInfo {
                CredentialId = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0),
                ParentId = reader.IsDBNull (1) ? Guid.Empty : reader.GetGuid (1),
                UserName = reader.IsDBNull (2) ? string.Empty : reader.GetString (2),
                IsNew = reader.IsDBNull (3) ? false : reader.GetBoolean (3),
                InvalidAttemptCount = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                IsLocked = reader.IsDBNull (5) ? false : reader.GetBoolean (5),                
                LockedOn = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6),
                PasswordChangedOn = reader.IsDBNull (7) ? DateTime.MinValue : reader.GetDateTime (7)
            };
            return role;
        }

        #endregion

        #region Manage
        internal bool Create (Guid tenantId, CredentialInfo info) {
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Create");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidCredentialId", info.CredentialId);
                cmd.AppendGuid ("@guidParentId", info.ParentId);
                cmd.AppendMediumText ("@strUserName", info.UserName);
                cmd.AppendMediumText ("@strPasswordHash", info.PasswordHash);
                cmd.AppendMediumText ("@strPasswordSalt", info.PasswordSalt);
                cmd.AppendBit ("@bitIsNew", info.IsNew);
                cmd.AppendDateTime ("@currentdate", DateTime.UtcNow);
                ExecuteCommand (cmd);
                return true;
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential :: Credential_Create");
            }
        }

        internal bool Update (Guid tenantId, CredentialInfo info) {
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Update");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidCredentialId", info.CredentialId);
                cmd.AppendGuid ("@guidParentId", info.ParentId);
                cmd.AppendMediumText ("@strPasswordHash", info.PasswordHash);
                cmd.AppendMediumText ("@strPasswordSalt", info.PasswordSalt);
                cmd.AppendBit ("@bitIsNew", info.IsNew);
                cmd.AppendDateTime ("@currentdate", DateTime.UtcNow);
                ExecuteCommand (cmd);
                return true;
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential::Credential_Update");
            }
        }

        internal bool Delete (Guid tenantId, CredentialInfo info) {
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_Delete");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidCredentialId", info.CredentialId);
                cmd.AppendGuid ("@guidParentId", info.ParentId);
                ExecuteCommand (cmd);
                return true;
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "Credential ::Credential_Delete");
            }
        }
        internal bool SetIsNew (Guid tenantId, CredentialInfo info) {
            try {
                var cmd = CreateProcedureCommand ("dbo.Credential_SetIsNew");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidCredentialId", info.CredentialId);
                cmd.AppendGuid ("@guidParentId", info.ParentId);
                cmd.AppendBit ("@bitIsNew", info.IsNew);
                cmd.AppendDateTime ("@datecurrentdate", DateTime.UtcNow);
                ExecuteCommand (cmd);
                return true;
            } catch (SqlException e) {

                throw ReportAndTranslateException (e, "Credential ::SetIsNew");
            }
        }

        internal bool UpdateLockedStatus (Guid tenantid, Guid credentialId, bool islocked, int? attemptcount, DateTime? date) {
            try {
                var cmd = CreateProcedureCommand ("[dbo].[UserCredential_UpdateLockedStatus]");
                cmd.AppendGuid ("@guidTenantId", tenantid);
                cmd.AppendGuid ("@guidCredentialId", credentialId);
                cmd.AppendBit ("@bitIsLocked", islocked);
                cmd.AppendInt ("@intInvalidAttemptCount", (int) attemptcount);
                if (date != null) {
                    cmd.AppendDateTime ("@datetimeLockedOn", (DateTime) date);
                }

                ExecuteCommand (cmd);
                return true;
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "UserCredential::UserCredential_UpdateLockedStatus");
            }
        }

        #endregion
    }
}