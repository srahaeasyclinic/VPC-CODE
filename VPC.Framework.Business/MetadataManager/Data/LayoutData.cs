using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NLog;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.Role;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.MetadataManager.Data {
    internal class LayoutData : EntityModelData {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();

        internal List<LayoutModel> GetLayoutsByEntityName (Guid tenantId, string entityId) {
            var layouts = new List<LayoutModel> ();
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_GetBy_EntityId");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendXSmallText ("@strEntityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        var layoutInfo = new LayoutModel (); {
                            layoutInfo.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                            //layoutInfo.Type = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                            layoutInfo.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
                            layoutInfo.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
                            var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
                            var layouttypename = "";
                            // if (layouttype > 0)
                            // {
                            //     if (layouttype == 1)
                            //         layouttypename = "View";
                            //     else if (layouttype == 2)
                            //         layouttypename = "Form";
                            //     else if (layouttype == 3)
                            //         layouttypename = "List";
                            //     else if (layouttype == 4)
                            //         layouttypename = "Edit";
                            // }

                            if (layouttype > 0)
                                layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

                            layoutInfo.LayoutTypeName = layouttypename;

                            var subtype = reader.IsDBNull (4) ? "" : reader.GetString (4);
                            // var subtypename = "";
                            // if (subtype != "")
                            // {
                            //     if (subtype == "EN10003-ST01")
                            //         subtypename = "Employee";
                            //     else if (subtype == "EN10003-ST02")
                            //         subtypename = "Consultant";
                            //     else if (subtype == "EN10001-ST01")
                            //         subtypename = "Standard";
                            //     else if (subtype == "EN10002-ST01")
                            //         subtypename = "Department";
                            //      else if (subtype == "EN10002-ST02")
                            //         subtypename = "Company";
                            // }
                            layoutInfo.Subtype = subtype;
                            //layoutInfo.SubtypeeName = subtypename;

                            var context = reader.IsDBNull (5) ? 0 : reader.GetInt32 (5);
                            var contextname = "";
                            // if (context > 0)
                            // {
                            //     if (context == 1)
                            //     {
                            //         contextname = "New";
                            //         layoutInfo.Context = LayoutContext.New;
                            //     }
                            //     else if (context == 2)
                            //     {
                            //         contextname = "Edit";
                            //         layoutInfo.Context = LayoutContext.Edit;
                            //     }
                            // }
                            if (context > 0)
                                contextname = Enum.GetName (typeof (LayoutContext), context);

                            layoutInfo.Context = (LayoutContext) context;
                            layoutInfo.ContextName = contextname;
                            layoutInfo.ModifiedDate = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6);
                            layoutInfo.ModifiedByName = reader.IsDBNull (7) ? string.Empty : reader.GetString (7);
                            layoutInfo.Layout = reader.IsDBNull (8) ? string.Empty : reader.GetString (8);
                            layoutInfo.DefaultLayout = !reader.IsDBNull (9) && reader.GetBoolean (9);
                            layoutInfo.LayoutType = (LayoutType) layouttype;
                            layoutInfo.ShowDefault = reader.IsDBNull (9) ? "false" : reader.GetBoolean (9) ? "true" : "false";
                        };
                        layouts.Add (layoutInfo);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsByEntityName");
            }

            return layouts;
        }

        internal List<LayoutModel> GetLayoutsByPicklistId (Guid tenantId, string picklistId) {
            var layouts = new List<LayoutModel> ();
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_GetBy_PicklistId");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendXSmallText ("@strPicklistId", picklistId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        var layoutInfo = new LayoutModel (); {
                            layoutInfo.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                            //layoutInfo.Type = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                            layoutInfo.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
                            layoutInfo.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
                            var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
                            var layouttypename = "";
                            // if (layouttype > 0)
                            // {
                            //     if (layouttype == 1)
                            //         layouttypename = "View";
                            //     else if (layouttype == 2)
                            //         layouttypename = "Form";
                            //     else if (layouttype == 3)
                            //         layouttypename = "List";
                            //     else if (layouttype == 4)
                            //         layouttypename = "Edit";
                            // }
                            if (layouttype > 0)
                                layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

                            layoutInfo.LayoutTypeName = layouttypename;

                            var context = reader.IsDBNull (4) ? 0 : reader.GetInt32 (4);
                            var contextname = "";
                            // if (context > 0)
                            // {
                            //     if (context == 1)
                            //         contextname = "New";
                            //     else if (context == 2)
                            //         contextname = "Edit";
                            // }
                            if (context > 0)
                                contextname = Enum.GetName (typeof (LayoutContext), context);

                            layoutInfo.Context = (LayoutContext) context;
                            layoutInfo.ContextName = contextname;
                            layoutInfo.ModifiedDate = reader.IsDBNull (5) ? DateTime.MinValue : reader.GetDateTime (5);
                            layoutInfo.ModifiedByName = reader.IsDBNull (6) ? string.Empty : reader.GetString (6);
                            layoutInfo.Layout = reader.IsDBNull (7) ? string.Empty : reader.GetString (7);
                            layoutInfo.DefaultLayout = !reader.IsDBNull (8) && reader.GetBoolean (8);
                            layoutInfo.LayoutType = (LayoutType) layouttype;
                            layoutInfo.ShowDefault = reader.IsDBNull (8) ? "false" : reader.GetBoolean (8) ? "true" : "false";
                        };
                        layouts.Add (layoutInfo);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsByPicklistId");
            }

            return layouts;
        }

        internal LayoutModel GetLayoutsDetailsById (Guid tenantId, Guid id) {
            var layout = new LayoutModel ();

            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_GetBy_Id");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", id);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        layout = ReadLayout (reader);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsDetailsById");
            }

            return layout;
        }

        //internal LayoutModel GetLayoutsDetailsById(string tenantCode, string entityId, int type, string subtype, int context)
        //{
        //    var layout = new LayoutModel();

        //    try
        //    {
        //        SqlProcedureCommand cmd = CreateProcedureCommand("dbo.EntityLayout_GetBy_Type");
        //        cmd.AppendXSmallText("@strTenantcode", tenantCode);
        //        cmd.AppendXSmallText("@strEntityId", entityId);
        //        cmd.AppendInt("@intType", type);
        //        cmd.AppendSmallText("@strSubType", subtype);
        //        cmd.AppendInt("@intLayoutContext", context);

        //        using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
        //        {
        //            while (reader.Read())
        //            {
        //                layout = ReadLayout(reader);
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        _log.Error(e);
        //        throw ReportAndTranslateException(e, "LayoutData::GetLayoutsDetailsById");
        //    }

        //    return layout;
        //}

        internal static LayoutModel ReadLayout (SqlDataReader reader) {
            var layoutInfo = new LayoutModel ();

            layoutInfo.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
            layoutInfo.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
            layoutInfo.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
            var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
            var layouttypename = "";
            // if (layouttype > 0)
            // {
            //     if (layouttype == 1)
            //         layouttypename = "View";
            //     else if (layouttype == 2)
            //         layouttypename = "Form";
            //     else if (layouttype == 3)
            //         layouttypename = "List";
            //     else if (layouttype == 4)
            //         layouttypename = "Edit";
            // }
            if (layouttype > 0)
                layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

            layoutInfo.LayoutTypeName = layouttypename;
            layoutInfo.LayoutType = (LayoutType) layouttype;

            var subtype = reader.IsDBNull (4) ? "" : reader.GetString (4);
            // var subtypename = "";
            // if (subtype != "")
            // {
            //     if (subtype == "EN10003-ST01")
            //         subtypename = "Employee";
            //     else if (subtype == "EN10003-ST02")
            //         subtypename = "Consultant";
            //     else if (subtype == "EN10001-ST01")
            //         subtypename = "Standard";
            //     else if (subtype == "EN10002-ST01")
            //         subtypename = "Department";
            //     else if (subtype == "EN10002-ST02")
            //         subtypename = "Company";
            // }
            layoutInfo.Subtype = subtype;
            //layoutInfo.SubtypeeName = subtypename;

            var context = reader.IsDBNull (5) ? 0 : reader.GetInt32 (5);
            layoutInfo.Context = (LayoutContext) context;
            var contextname = "";
            // if (context > 0)
            // {
            //     if (context == 1)
            //         contextname = "New";
            //     else if (context == 2)
            //         contextname = "Edit";
            // }
            if (context > 0)
                contextname = Enum.GetName (typeof (LayoutContext), context);

            layoutInfo.ContextName = contextname;
            layoutInfo.Layout = reader.IsDBNull (6) ? String.Empty : reader.GetString (6);
            layoutInfo.ModifiedDate = reader.IsDBNull (7) ? DateTime.MinValue : reader.GetDateTime (7);
            //layoutInfo.ModifiedByName = reader.IsDBNull(8) ? String.Empty : reader.GetString(8);            
            //layoutInfo.DefaultLayout = !reader.IsDBNull(9) && reader.GetBoolean(9);            
            layoutInfo.ShowDefault = reader.IsDBNull (8) ? "false" : reader.GetBoolean (8) ? "true" : "false";

            return layoutInfo;
        }

        internal void CreateLayout (Guid tenantId, LayoutModel layoutModel) {
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_Create");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", layoutModel.Id);
                cmd.AppendXSmallText ("@strEntityId", layoutModel.EntityId);
                cmd.AppendMediumText ("@strName", layoutModel.Name);

                cmd.AppendInt ("@intType", (int) layoutModel.LayoutType);

                if (layoutModel.Subtype != "")
                    cmd.AppendSmallText ("@strSubType", layoutModel.Subtype);
                // else
                //     cmd.AppendSmallText("@strSubType", "");

                if (layoutModel.Context > 0)
                    cmd.AppendInt ("@intLayoutContext", (int) layoutModel.Context);
                // else
                //     cmd.AppendInt("@intLayoutContext", 0);

                if (!string.IsNullOrEmpty (layoutModel.Layout)) {
                    cmd.AppendXLargeText ("@layoutStr", layoutModel.Layout);
                }

                if (layoutModel.DefaultLayout) {
                    cmd.AppendBit ("@defaultLayout", layoutModel.DefaultLayout);
                }
                cmd.AppendGuid ("@guidUpdatedBy", layoutModel.ModifiedBy);
                ExecuteCommand (cmd);
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::CreateLayout");
            }
        }

        internal void CreatePicklistLayout (Guid tenantId, LayoutModel layoutModel) {
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.PicklistLayout_Create");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", layoutModel.Id);
                cmd.AppendXSmallText ("@strPicklistId", layoutModel.EntityId);
                cmd.AppendMediumText ("@strName", layoutModel.Name);

                cmd.AppendInt ("@intType", (int) layoutModel.LayoutType);

                if (layoutModel.Context > 0)
                    cmd.AppendInt ("@intLayoutContext", (int) layoutModel.Context);

                cmd.AppendGuid ("@guidUpdatedBy", layoutModel.ModifiedBy);

                if (!string.IsNullOrEmpty (layoutModel.Layout)) {
                    cmd.AppendXLargeText ("@defaultLayout", layoutModel.Layout);
                }
                if (layoutModel.DefaultLayout) {
                    cmd.AppendBit ("@isDefault", layoutModel.DefaultLayout);
                }

                ExecuteCommand (cmd);
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::CreatePicklistLayout");
            }
        }

        internal void SetPicklistLayoutDefault (Guid tenantId, LayoutModel layoutModel) {
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.PicklistLayout_Set_Default");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", layoutModel.Id);
                cmd.AppendXSmallText ("@strPicklistId", layoutModel.EntityId);

                cmd.AppendInt ("@intType", (int) layoutModel.LayoutType);
                cmd.AppendInt ("@intContext", (int) layoutModel.Context);

                cmd.AppendGuid ("@guidUpdatedBy", layoutModel.ModifiedBy);
                ExecuteCommand (cmd);
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::SetPicklistLayoutDefault");
            }
        }

        internal void SetListLayoutDefault (Guid tenantId, LayoutModel layoutModel) {
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.ListLayout_Set_Default");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", layoutModel.Id);
                cmd.AppendXSmallText ("@strListId", layoutModel.EntityId);

                cmd.AppendInt ("@intType", (int) layoutModel.LayoutType);

                cmd.AppendSmallText ("@strSubTypeId", layoutModel.Subtype);
                cmd.AppendInt ("@intContext", (int) layoutModel.Context);

                cmd.AppendGuid ("@guidUpdatedBy", layoutModel.ModifiedBy);
                ExecuteCommand (cmd);
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::SetListLayoutDefault");
            }
        }

        internal void UpdateLayoutDetails (Guid tenantId, Guid layoutId, LayoutModel templateModel) {
            SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_Update");
            cmd.AppendGuid ("@guidTenantId", tenantId);
            cmd.AppendGuid ("@guidId", layoutId);
            cmd.AppendXLargeText ("@strLayout", templateModel.Layout);
            cmd.AppendGuid ("@guidUpdatedBy", templateModel.ModifiedBy);
            cmd.AppendMediumText ("@strName", templateModel.Name);

            ExecuteCommand (cmd);
        }

        internal LayoutModel GetLayoutsDetail (Guid tenantId, string entityContext, int layoutType, string subType, int context) {
            //var entityLayout = new LayoutModel ();
            LayoutModel entityLayout = null;

            try {
                var cmd = CreateProcedureCommand ("dbo.Layout_GetBy_Type");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendXSmallText ("@strEntityId", entityContext);
                cmd.AppendInt ("@intType", layoutType);
                //if (subType > 0)
                //{
                //    cmd.AppendInt("@intSubType", subType);
                //}

                if (subType != "")
                    cmd.AppendSmallText ("@strSubType", subType);

                if (context > 0) {
                    cmd.AppendInt ("@LayoutContext", context);
                }

                using (var reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        entityLayout = new LayoutModel ();
                        entityLayout.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                        entityLayout.EntityId = reader.IsDBNull (1) ? string.Empty : reader.GetString (1);
                        entityLayout.Name = reader.IsDBNull (2) ? string.Empty : reader.GetString (2);
                        entityLayout.LayoutType = (LayoutType) (reader.IsDBNull (3) ? 0 : reader.GetInt32 (3));
                        entityLayout.Subtype = reader.IsDBNull (4) ? string.Empty : reader.GetString (4);
                        entityLayout.Context = (LayoutContext) (reader.IsDBNull (5) ? 0 : reader.GetInt32 (5));
                        entityLayout.Layout = reader.IsDBNull (6) ? string.Empty : reader.GetString (6);
                        entityLayout.ModifiedDate = reader.IsDBNull (7) ? DateTime.MinValue : reader.GetDateTime (7);
                        entityLayout.ModifiedBy = reader.IsDBNull (8) ? Guid.Empty : reader.GetGuid (8);
                        entityLayout.DefaultLayout = !reader.IsDBNull (9) && reader.GetBoolean (9);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsDetail");
            }
            return entityLayout;
        }

        internal void DeletePicklistLayout (Guid tenantId, Guid layoutId) {
            SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.PicklistLayout_Delete");
            cmd.AppendGuid ("@guidTenantId", tenantId);
            cmd.AppendGuid ("@guidId", layoutId);
            ExecuteCommand (cmd);
        }

        internal void DeleteListLayout (Guid tenantId, Guid layoutId) {
            SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.ListLayout_Delete");
            cmd.AppendGuid ("@guidTenantId", tenantId);
            cmd.AppendGuid ("@guidId", layoutId);
            ExecuteCommand (cmd);
        }

        public LayoutModel GetPicklistLayoutDetailsById (Guid tenantId, Guid id) {
            var layout = new LayoutModel ();

            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.PicklistLayout_GetBy_Id");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", id);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        //layout = ReadLayout(reader);

                        //var layoutInfo = new LayoutModel();

                        layout.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                        layout.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
                        layout.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
                        var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
                        var layouttypename = "";
                        // if (layouttype > 0)
                        // {
                        //     if (layouttype == 1)
                        //         layouttypename = "View";
                        //     else if (layouttype == 2)
                        //         layouttypename = "Form";
                        //     else if (layouttype == 3)
                        //         layouttypename = "List";
                        //     else if (layouttype == 4)
                        //         layouttypename = "Edit";
                        // }
                        if (layouttype > 0)
                            layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

                        layout.LayoutTypeName = layouttypename;
                        layout.LayoutType = (LayoutType) layouttype;

                        var context = reader.IsDBNull (4) ? 0 : reader.GetInt32 (4);
                        layout.Context = (LayoutContext) context;
                        var contextname = "";
                        // if (context > 0)
                        // {
                        //     if (context == 1)
                        //         contextname = "New";
                        //     else if (context == 2)
                        //         contextname = "Edit";
                        // }
                        if (context > 0)
                            contextname = Enum.GetName (typeof (LayoutContext), context);

                        layout.ContextName = contextname;
                        layout.Layout = reader.IsDBNull (5) ? String.Empty : reader.GetString (5);
                        layout.ModifiedDate = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6);
                        layout.ShowDefault = reader.IsDBNull (7) ? "false" : reader.GetBoolean (7) ? "true" : "false";

                    }
                }
            } catch (SqlException e) {
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsDetailsById");
            }

            return layout;
        }

        public void UpdatePicklistLayout (Guid tenantId, Guid layoutId, LayoutModel layout) {
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.PicklistLayout_Update");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendGuid ("@guidId", layoutId);
                cmd.AppendXLargeText ("@strLayout", layout.Layout);
                cmd.AppendGuid ("@guidUpdatedBy", layout.ModifiedBy);
                cmd.AppendMediumText ("@strName", layout.Name);

                ExecuteCommand (cmd);
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::UpdatePicklistLayout");
            }

        }

        internal LayoutModel GetDefaultPicklistLayout (Guid tenantId, string entityName, LayoutType layoutType, int context) {
            //var entityLayout = new LayoutModel ();
            LayoutModel entityLayout = null;
            try {
                var cmd = CreateProcedureCommand ("dbo.PicklistLayout_GetDefaultBy_Type");
                cmd.AppendGuid ("@guidTenantId", tenantId);
                cmd.AppendXSmallText ("@strEntityId", entityName);
                cmd.AppendInt ("@intType", (int) layoutType);
                if (context > 0) {
                    cmd.AppendInt ("@LayoutContext", context);
                }

                using (var reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        entityLayout = new LayoutModel ();
                        entityLayout.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                        entityLayout.EntityId = reader.IsDBNull (1) ? string.Empty : reader.GetString (1);
                        entityLayout.Name = reader.IsDBNull (2) ? string.Empty : reader.GetString (2);
                        entityLayout.LayoutType = (LayoutType) (reader.IsDBNull (3) ? 0 : reader.GetInt32 (3));
                        entityLayout.Context = (LayoutContext) (reader.IsDBNull (4) ? 0 : reader.GetInt32 (4));
                        entityLayout.Layout = reader.IsDBNull (5) ? string.Empty : reader.GetString (5);
                        entityLayout.ModifiedDate = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6);
                        entityLayout.ModifiedBy = reader.IsDBNull (7) ? Guid.Empty : reader.GetGuid (7);
                        entityLayout.DefaultLayout = !reader.IsDBNull (8) && reader.GetBoolean (8);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetDefaultPicklistLayout");
            }
            return entityLayout;
        }

        internal List<LayoutModel> GetLayoutsByEntityName (Guid tenantId, string entityContext, int type, bool isPicklist) {
            var layouts = new List<LayoutModel> ();
            try {
                if (isPicklist) {
                    SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.Picklist_GetBy_Type");
                    cmd.AppendGuid ("@guidTenantId", tenantId);
                    cmd.AppendXSmallText ("@strEntityId", entityContext);
                    cmd.AppendInt ("@intType", type);

                    using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                        while (reader.Read ()) {
                            var layoutInfo = new LayoutModel (); {
                                layoutInfo.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                                layoutInfo.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
                                layoutInfo.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
                                var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
                                var layouttypename = "";

                                if (layouttype > 0)
                                    layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

                                layoutInfo.LayoutType = (LayoutType) layouttype;
                                layoutInfo.LayoutTypeName = layouttypename;

                                var context = reader.IsDBNull (4) ? 0 : reader.GetInt32 (4);
                                var contextname = "";
                                if (context > 0)
                                    contextname = Enum.GetName (typeof (LayoutContext), context);

                                layoutInfo.Context = (LayoutContext) context;
                                layoutInfo.ContextName = contextname;
                                layoutInfo.Layout = reader.IsDBNull (5) ? string.Empty : reader.GetString (5);
                                layoutInfo.ModifiedDate = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6);
                                //layoutInfo.ModifiedByName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);                            
                                layoutInfo.DefaultLayout = !reader.IsDBNull (8) && reader.GetBoolean (8);
                                layoutInfo.ShowDefault = reader.IsDBNull (8) ? "false" : reader.GetBoolean (8) ? "true" : "false";
                            };
                            layouts.Add (layoutInfo);
                        }
                    }
                } else {
                    SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.EntityLayout_GetBy_Type");
                    cmd.AppendGuid ("@guidTenantId", tenantId);
                    cmd.AppendXSmallText ("@strEntityId", entityContext);
                    cmd.AppendInt ("@intType", type);

                    using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                        while (reader.Read ()) {
                            var layoutInfo = new LayoutModel (); {
                                layoutInfo.Id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                                layoutInfo.EntityId = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);
                                layoutInfo.Name = reader.IsDBNull (2) ? String.Empty : reader.GetString (2);
                                var layouttype = reader.IsDBNull (3) ? 0 : reader.GetInt32 (3);
                                var layouttypename = "";

                                if (layouttype > 0)
                                    layouttypename = Enum.GetName (typeof (LayoutType), layouttype);

                                layoutInfo.LayoutType = (LayoutType) layouttype;
                                layoutInfo.LayoutTypeName = layouttypename;

                                var context = reader.IsDBNull (4) ? 0 : reader.GetInt32 (4);
                                var contextname = "";
                                if (context > 0)
                                    contextname = Enum.GetName (typeof (LayoutContext), context);

                                layoutInfo.Context = (LayoutContext) context;
                                layoutInfo.ContextName = contextname;
                                layoutInfo.Layout = reader.IsDBNull (5) ? string.Empty : reader.GetString (5);
                                layoutInfo.ModifiedDate = reader.IsDBNull (6) ? DateTime.MinValue : reader.GetDateTime (6);
                                //layoutInfo.ModifiedByName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);                            
                                layoutInfo.DefaultLayout = !reader.IsDBNull (8) && reader.GetBoolean (8);
                                layoutInfo.ShowDefault = reader.IsDBNull (8) ? "false" : reader.GetBoolean (8) ? "true" : "false";

                                layoutInfo.Subtype = reader.IsDBNull (9) ? String.Empty : reader.GetString (9);
                            };
                            layouts.Add (layoutInfo);
                        }
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsByEntityName");
            }

            return layouts;
        }

        internal Guid GetTenantId (string entityContext, string code) {
            Guid id = Guid.Empty;

            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.Tenant_GetTenantId");
                cmd.AppendXSmallText ("@strEntityId", entityContext);
                cmd.AppendMediumText ("@strCode", code);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        id = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsDetailsById");
            }

            return id;
        }

        internal List<RoleInfo> GetUserRoles (Guid userId) {
            var userRoles = new List<RoleInfo> ();
            try {
                SqlProcedureCommand cmd = CreateProcedureCommand ("dbo.Role_GetBy_Id");
                cmd.AppendGuid ("@strUserId", userId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader (cmd)) {
                    while (reader.Read ()) {
                        var userRole = new RoleInfo (); {
                            userRole.RoleId = reader.IsDBNull (0) ? Guid.Empty : reader.GetGuid (0);
                            userRole.Name = reader.IsDBNull (1) ? String.Empty : reader.GetString (1);

                            var roleType = reader.IsDBNull (2) ? 0 : reader.GetByte (2);
                            var roleTypeName = "";

                            if (roleType > 0)
                                roleTypeName = Enum.GetName (typeof (RoleTypeEnum), roleType);

                            userRole.RoleType = (RoleTypeEnum) roleType;
                            userRole.RoleTypeName = roleTypeName;
                        };
                        userRoles.Add (userRole);
                    }
                }
            } catch (SqlException e) {
                _log.Error (e);
                throw ReportAndTranslateException (e, "LayoutData::GetLayoutsDetailsById");
            }

            return userRoles;
        }
    }
}