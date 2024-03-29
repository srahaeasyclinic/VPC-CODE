using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NLog;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata;

namespace VPC.Framework.Business.Menu.Data
{
    internal sealed class MenuData : EntityModelData
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        internal List<MenuItem> GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize)
        {
            var menus = new List<MenuItem>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendInt("@intPageIndex", pageIndex);
                cmd.AppendInt("@intPageSize", pageSize);
                cmd.AppendMediumText("@strGroupName", groupName);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var menu = new MenuItem();
                        {
                            menu.TenantId = tenantId;
                            menu.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                            menu.GroupId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                            menu.Name = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                            menu.MenuTypeId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                            if (menu.MenuTypeId > 0)
                                menu.MenuTypeName = Enum.GetName(typeof(MenuTypeEnum), menu.MenuTypeId);

                            menu.ReferenceEntityId = reader.IsDBNull(4) ? String.Empty : reader.GetString(4);
                            menu.ActionTypeId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);

                            if (menu.ActionTypeId > 0)
                                menu.ActionTypeName = Enum.GetName(typeof(ActionTypeEnum), menu.ActionTypeId);

                            menu.ModifiedDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                            menu.ModifiedBy = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9);
                            menu.GroupName = reader.IsDBNull(10) ? String.Empty : reader.GetString(10);
                            menu.WellKnownLink = reader.IsDBNull(11) ? String.Empty : reader.GetString(11);

                            menu.ParentId = reader.IsDBNull(12) ? Guid.Empty : reader.GetGuid(12);
                            menu.SortItem = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                            menu.MenuIcon = reader.IsDBNull(14) ? String.Empty : reader.GetString(14);
                            menu.Menucode = reader.IsDBNull(15) ? String.Empty : reader.GetString(15);
                            menu.IsMenuGroup = reader.IsDBNull(16) ? false : reader.GetBoolean(16);

                        }

                        menus.Add(menu);


                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::GetMenu");
            }

            return menus;
        }

        internal MenuItem GetMenuById(Guid tenantId, Guid id)
        {
            var menu = new MenuItem();

            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_GetBy_Id");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", id);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        menu.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        menu.GroupId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        menu.Name = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                        menu.MenuTypeId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                        if (menu.MenuTypeId > 0)
                            menu.MenuTypeName = Enum.GetName(typeof(MenuTypeEnum), menu.MenuTypeId);

                        menu.ReferenceEntityId = reader.IsDBNull(4) ? String.Empty : reader.GetString(4);
                        menu.ActionTypeId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        menu.LayoutId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        menu.ModifiedDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                        menu.ModifiedBy = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9);
                        menu.WellKnownLink = reader.IsDBNull(10) ? String.Empty : reader.GetString(10);
                        menu.ParentId = reader.IsDBNull(11) ? Guid.Empty : reader.GetGuid(11);
                        menu.SortItem = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                        menu.MenuIcon = reader.IsDBNull(13) ? String.Empty : reader.GetString(13);
                        menu.Menucode = reader.IsDBNull(14) ? String.Empty : reader.GetString(14);
                        menu.IsMenuGroup = reader.IsDBNull(15) ? false : reader.GetBoolean(15);
                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::GetMenuById");
            }

            return menu;
        }

        internal void CreateMenu(Guid tenantId, MenuItem menuModel)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", menuModel.Id);
                cmd.AppendGuid("@guidGroupId", menuModel.GroupId);
                cmd.AppendMediumText("@strName", menuModel.Name);
                cmd.AppendInt("@intMenuType", (int)menuModel.MenuTypeId);
                cmd.AppendSmallText("@strReferenceEntity", menuModel.ReferenceEntityId);
                cmd.AppendInt("@intActionTypeId", (int)menuModel.ActionTypeId);
                cmd.AppendMediumText("@strWellKnownLink", menuModel.WellKnownLink);
                cmd.AppendGuid("@guidLayoutId", menuModel.LayoutId);
                cmd.AppendGuid("@guidUpdatedBy", menuModel.ModifiedBy);

                cmd.AppendGuid("@parentId", menuModel.ParentId);
                cmd.AppendInt("@sortitem", (int)menuModel.SortItem);
                cmd.AppendMediumText("@MenuIcon", menuModel.MenuIcon);
                cmd.AppendLargeText("@MenuCode", menuModel.Menucode);
                //cmd.AppendBit("@IsMenuGroup", menuModel.IsMenuGroup == null ? false : true);
                cmd.AppendBit("@IsMenuGroup", menuModel.IsMenuGroup);

                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::CreateMenu");
            }
        }

        internal void UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", menuId);
                cmd.AppendGuid("@guidGroupId", menuModel.GroupId);
                cmd.AppendMediumText("@strName", menuModel.Name);
                cmd.AppendInt("@intMenuType", (int)menuModel.MenuTypeId);
                cmd.AppendSmallText("@strReferenceEntity", menuModel.ReferenceEntityId);
                cmd.AppendInt("@intActionTypeId", (int)menuModel.ActionTypeId);
                cmd.AppendMediumText("@strWellKnownLink", menuModel.WellKnownLink);
                cmd.AppendGuid("@guidLayoutId", menuModel.LayoutId);
                cmd.AppendGuid("@guidUpdatedBy", menuModel.ModifiedBy);
                cmd.AppendGuid("@parentId", menuModel.ParentId);
                cmd.AppendInt("@sortitem", (int)menuModel.SortItem);
                cmd.AppendMediumText("@MenuIcon", menuModel.MenuIcon);
                cmd.AppendLargeText("@MenuCode", menuModel.Menucode);
                cmd.AppendBit("@IsMenuGroup", menuModel.IsMenuGroup);

                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::UpdateMenu");
            }
        }

        internal void DeleteMenu(Guid tenantId, Guid menuId)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", menuId);
                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::DeleteMenu");
            }
        }

        internal List<MenuItem> GetMenuByTenant(Guid tenantId)
        {
            var menus = new List<MenuItem>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.Menu_GetAll_NoPaging");
                cmd.AppendGuid("@guidTenantId", tenantId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var menu = new MenuItem();
                        {
                            menu.TenantId = tenantId;
                            menu.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                            menu.GroupId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                            menu.Name = reader.IsDBNull(2) ? String.Empty : reader.GetString(2);
                            menu.MenuTypeId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                            if (menu.MenuTypeId > 0)
                                menu.MenuTypeName = Enum.GetName(typeof(MenuTypeEnum), menu.MenuTypeId);

                            menu.ReferenceEntityId = reader.IsDBNull(4) ? String.Empty : reader.GetString(4);
                            menu.ActionTypeId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);

                            if (menu.ActionTypeId > 0)
                                menu.ActionTypeName = Enum.GetName(typeof(ActionTypeEnum), menu.ActionTypeId);

                            menu.ModifiedDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                            menu.ModifiedBy = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9);
                            menu.GroupName = reader.IsDBNull(10) ? String.Empty : reader.GetString(10);
                            menu.WellKnownLink = reader.IsDBNull(11) ? String.Empty : reader.GetString(11);

                            menu.ParentId = reader.IsDBNull(12) ? Guid.Empty : reader.GetGuid(12);
                            menu.SortItem = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                            menu.MenuIcon = reader.IsDBNull(14) ? String.Empty : reader.GetString(14);
                            menu.Menucode = reader.IsDBNull(15) ? String.Empty : reader.GetString(15);
                            menu.IsMenuGroup = reader.IsDBNull(16) ? false : reader.GetBoolean(16);
                            menu.GroupIdSort = reader.IsDBNull(17) ? -0 : reader.GetInt32(17);

                        }

                        menus.Add(menu);


                    }
                }
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "MenuData::GetMenu");
            }

            return menus;
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

        internal void InitializeApplicationMenu(Guid rootTenantCode, Guid initilizedTenantCode,Guid userId,short PicklistId)
        {
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.ApplicationMenu_PickListValue_Clone");
                cmd.AppendGuid("@rootTenantId", rootTenantCode);
                cmd.AppendGuid("@initilizedTenantId", initilizedTenantCode);
                cmd.AppendSmallInt("@PicklistId", PicklistId);
                cmd.AppendGuid("@guidUserId", userId);
                ExecuteCommand(cmd);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "InitializeData::ApplicationMenu_PickListValue_Clone");
            }
        }
    }
}