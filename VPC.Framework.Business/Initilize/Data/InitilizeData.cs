using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Initilize.Data
{
    internal class InitilizeData : EntityModelData
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        internal Guid getRootTenantCode()
        {
            var code = Guid.Empty;
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetRootTenantCode");
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        code = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitilizeData::getRootTenantCode");
            }

            return code;
        }

        internal Guid GetNewlyCreatedEntityId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string entityId)
        {
            var code = Guid.Empty;
            try
            {
                Guid value = new Guid(defaultValue.ToString());
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetNewlyCreatedEntityId");
                cmd.AppendGuid("@guidRootTenantId", rootTenantId);
                cmd.AppendGuid("@guidIntialisedTenantId", intialisedTenantId);
                cmd.AppendGuid("@guidTargetValue", value);
                cmd.AppendSmallText("@entityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        code = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitilizeData::getRootTenantCode");
            }

            return code;
        }

        internal Guid GetNewlyCreatedPickListId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string picklistId)
        {
            var code = Guid.Empty;
            try
            {
                Guid value = new Guid(defaultValue.ToString());
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetNewlyCreatedPickListId");
                cmd.AppendGuid("@guidRootTenantId", rootTenantId);
                cmd.AppendGuid("@guidIntialisedTenantId", intialisedTenantId);
                cmd.AppendGuid("@guidTargetValue", value);
                cmd.AppendSmallText("@picklistId", picklistId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        code = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitilizeData::getRootTenantCode");
            }

            return code;
        }

        internal List<LayoutModel> GetAllEntityAndPickListFormLayoutsByTenantId(Guid id)
        {
            var layouts = new List<LayoutModel>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetAllEntityAndPickListFormLayoutsByTenantId");
                cmd.AppendGuid("@guidTenantId", id);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var layoutInfo = new LayoutModel();
                        layoutInfo.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        layoutInfo.TypeId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        layoutInfo.Layout = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        layouts.Add(layoutInfo);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitilizeData::getRootTenantCode");
            }

            return layouts;
        }

        internal List<LayoutModel> GetRootTenantLayouts(Guid tenantId)
        {
            var layouts = new List<LayoutModel>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetRootTenantLayouts");
                cmd.AppendGuid("@guidTenantId", tenantId);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var layoutInfo = new LayoutModel();
                        layoutInfo.EntityId = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        layoutInfo.Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        layoutInfo.LayoutType = (LayoutType)(reader.IsDBNull(2) ? 0 : reader.GetInt32(2));
                        layoutInfo.Subtype = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        layoutInfo.Context = (LayoutContext)(reader.IsDBNull(4) ? 0 : reader.GetInt32(4));
                        layoutInfo.Layout = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        layoutInfo.ModifiedDate = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6);
                        layoutInfo.ModifiedBy = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        layoutInfo.DefaultLayout = !reader.IsDBNull(8) && reader.GetBoolean(8);
                        layoutInfo.LayoutFor = (LayoutFor)(reader.IsDBNull(9) ? 0 : reader.GetInt32(9));

                        layouts.Add(layoutInfo);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitilizeData::getRootTenantCode");
            }

            return layouts;
        }


        internal bool InitializePickListLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.PicklistLayouts_Clone");
                cmd.AppendGuid("@rootTenantId", rootTenantCode);
                cmd.AppendGuid("@targetTenantId", initilizedTenantCode);
                var xmlIds = GetXmlString(picklists);
                cmd.AppendXml("@xmlIds", xmlIds);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "LayoutData::PicklistLayouts_Clone");
            }
        }

        private string GetXmlString(List<string> picklists)
        {
            var xEle = new XElement("items");
            foreach (var item in picklists)
            {
                var it = new XElement("item");
                var att = new XAttribute("value", item);
                it.Add(att);
                xEle.Add(it);
            }

            return xEle.ToString();
        }

        internal bool InitializeMedataLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.EntityLayouts_Clone");
                cmd.AppendGuid("@rootTenantId", rootTenantCode);
                cmd.AppendGuid("@targetTenantId", initilizedTenantCode);
                var xmlIds = GetXmlString(picklists);
                cmd.AppendXml("@xmlIds", xmlIds);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "LayoutData::EntityLayouts_Clone");
            }
        }

        internal void InitializePicklistValue(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode, Guid userId)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.PickListValue_Clone");
                cmd.AppendGuid("@rootTenantId", rootTenantCode);
                cmd.AppendGuid("@targetTenantId", initilizedTenantCode);
                cmd.AppendGuid("@guidUserId", userId);
                // var xmlIds = GetXmlString(picklists);
                // cmd.AppendXml("@xmlIds", xmlIds);
                ExecuteCommand(cmd);

                // foreach (var picklist in picklists)
                // {
                //     if (picklist != null && picklist != "")
                //     {
                //         var match = true;
                //         switch (picklist)
                //         {
                //             case "20001":
                //                 spname += "_Currency_";
                //                 break;
                //             case "20002":
                //                 spname += "_Country_";
                //                 break;
                //             case "20003":
                //                 spname += "_Language_";
                //                 break;
                //             case "20004":
                //                 spname += "_Timezone_";
                //                 break;
                //             case "20005":
                //                 spname += "_State_";
                //                 break;
                //             case "20006":
                //                 spname += "_City_";
                //                 break;
                //             case "20007":
                //                 spname += "_Municipality_";
                //                 break;
                //             case "20008":
                //                 spname += "_SecurityFunction_";
                //                 break;
                //             // case "20012":
                //             //     spname += "_MenuGroup_";
                //             //     break;
                //             case "10015":
                //                 spname += "_MenuGroup_";
                //                 break;
                //             default:
                //                 Console.WriteLine("Default case");
                //                 match = false;
                //                 break;

                //         }
                //         if(!match) continue;
                //         spname += "Clone";
                //         cmd = CreateProcedureCommand(spname);
                //         cmd.AppendXSmallText("@picklistId", picklist);
                //         cmd.AppendGuid("@rootTenantId", rootTenantCode);
                //         cmd.AppendGuid("@initilizedTenantId", initilizedTenantCode);
                //         ExecuteCommand(cmd);
                //     }
                // }

                // foreach(var picklist in picklists)
                // {
                //     spname = "10001" + "_PV_Create";

                //     SqlProcedureCommand cmd = CreateProcedureCommand(spname);
                //     cmd.AppendXSmallText("@strPicklistId", "10001");
                //     cmd.AppendGuid("@rootTenantId", rootTenantCode);
                //     cmd.AppendGuid("@targetTenantId", initilizedTenantCode);                    
                //     ExecuteCommand(cmd);
                // }

                // SqlProcedureCommand cmd = CreateProcedureCommand("dbo.EntityLayouts_Clone");
                // cmd.AppendGuid("@rootTenantId", rootTenantCode);
                // cmd.AppendGuid("@targetTenantId", initilizedTenantCode);
                // var xmlIds = GetXmlString(picklists);
                // cmd.AppendXml("@xmlIds", xmlIds);
                // ExecuteCommand(cmd);
                // return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitializeData::PickList_Clone");
            }
        }

        internal void InitializeMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_Clone");
                cmd.AppendGuid("@rootTenantId", rootTenantCode);
                cmd.AppendGuid("@initilizedTenantId", initilizedTenantCode);
                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitializeData::Menu_Clone");
            }
        }
    }
}