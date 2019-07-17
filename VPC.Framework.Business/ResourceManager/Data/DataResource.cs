using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NLog;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.ResourceManager.Data
{
    internal class DataResource : EntityModelData
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private int _totalRowCount = 0;

        #region Review

        internal List<Resource> GetResources(Guid tenantId, string language = null)
        {
            var resources = new List<Resource>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strLanguage", language);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        resources.Add(ReadResourceInfo(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::GetResources");

            }
            _totalRowCount = resources.Count;

            return resources;
        }


        internal List<Resource> GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language)
        {
            var resources = new List<Resource>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_GetByKeyAndLanguage");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendMediumText("@strKey", key);
                cmd.AppendXSmallText("@strLanguage", language);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var resource = new Resource
                        {
                            Id = reader.GetGuid(0),
                            Key = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            Value = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Language = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            LanguageName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                        };
                        resources.Add(resource);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::GetResourcesByKeyAndLanguage");

            }
            return resources;
        }

        internal List<Resource> GetResourcesDetails(Guid tenantId, int pageIndex, int pageSize, string orderBy, string language = null)
        {
            var resources = new List<Resource>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_GetALL");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendInt("@intPageIndex", pageIndex);
                cmd.AppendInt("@intPageSize", pageSize);
                cmd.AppendText("@strOrderBy", orderBy);
                cmd.AppendXSmallText("@strLanguage", language);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        resources.Add(ReadResourceInfo(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::GetResources");

            }
            return resources;
        }

        internal List<Resource> GetResourcesDetails(Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount, string language = null)
        {
            var resources = new List<Resource>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_GetALL");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendInt("@intPageIndex", pageIndex);
                cmd.AppendInt("@intPageSize", pageSize);
                cmd.AppendText("@strOrderBy", orderBy);
                cmd.AppendXSmallText("@strLanguage", language);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var resource = new Resource
                        {
                            Id = reader.GetGuid(0),
                            Key = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            Value = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Language = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            LanguageName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                        };
                        resources.Add(resource);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::GetResources");

            }

            totalRowCount = (_totalRowCount > 0) ? _totalRowCount : this.GetResources(tenantId).Count;

            return resources;
        }

        internal void CopyResources(Guid rootTenantId, Guid toTenantId)
        {

            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_Clone");
                cmd.AppendGuid("@guidRootTenantId", rootTenantId);
                cmd.AppendGuid("@guidToTenantId", toTenantId);
                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::copyResources");
            }
        }


        internal string GetKeyFromLanguage(Guid tenantId, string language)
        {
            string retKey = string.Empty;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_GetKeyFromLanguage");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strLanguage", language);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        retKey = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);

                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::GetKeyFromLanguage");

            }
            return retKey;
        }

        internal List<Resource> GetDuplicateResourceKey(Guid tenantId, string key, string language)
        {
            var resources = new List<Resource>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Resource_CheckDuplicateKey");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendMediumText("@strKey", key.Trim());
                cmd.AppendXSmallText("@strLanguage", language.Trim());

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        resources.Add(ReadResourceInfo(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "DataResource::Resource_CheckDuplicateKey");

            }
            return resources;
        }


        #endregion

        #region Manage

        internal bool Create(Guid tenantId, Resource resource, ref string strMsg)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Resource_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", resource.Id);
                cmd.AppendMediumText("@strKey", resource.Key);
                cmd.AppendXLargeText("@strValue", resource.Value);
                cmd.AppendXSmallText("@strLanguage", resource.Language);
                cmd.AppendVarChar("@strMessage", 100);
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
                throw ReportAndTranslateException(e, "DataResource::Create");
            }
        }

        internal bool CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Resource_Create_Xml");
                cmd.AppendGuid("@guidrootTenantId", rootTenantId);
                cmd.AppendXml("@XmlForResources", DataUtility.GetXmlForResourceCreate(currentTenantId, resources));
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataResource::dbo.Resource_Create_Xml");
            }
        }

        internal bool Update(Guid resourceId, Guid tenantId, Resource resource, ref string strMsg)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Resource_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendMediumText("@strKey", resource.Key);
                cmd.AppendXLargeText("@strValue", resource.Value);
                cmd.AppendXSmallText("@strLanguage", resource.Language);
                cmd.AppendGuid("@guidId", resourceId);
                cmd.AppendVarChar("@strMessage", 100);
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
                throw ReportAndTranslateException(e, "DataResource::Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid resourceId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Resource_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", resourceId);

                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataResource::Delete");
            }
        }

        internal bool DeleteByKey(Guid tenantId, string resourceKey)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Resource_DeleteByKey");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendMediumText("@strKey", resourceKey);

                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataResource::DeleteByKey");
            }
        }

        #endregion

        private static Resource ReadResourceInfo(SqlDataReader reader)
        {
            return new Resource
            {
                Id = reader.GetGuid(0),
                Key = reader.GetString(1),
                Value = reader.GetString(2),
                Language = reader.GetString(3)
            };
        }
    }
}