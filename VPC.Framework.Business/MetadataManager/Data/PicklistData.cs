using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VPC.Entities.EntityCore.Model.PickListItem;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Exception;

namespace VPC.Framework.Business.MetadataManager.Data
{
    internal class PicklistData : EntityModelData
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        #region Picklist
        internal void Create(string tenantCode, Picklist picklist)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", picklist.Id);
                cmd.AppendSmallText("@strName", picklist.Name);
                cmd.AppendBit("@bitIsStandard", picklist.IsStandard);
                if (picklist.EntityId > 0)
                    cmd.AppendSmallInt("@intEntityId", picklist.EntityId);
                cmd.AppendBit("@bitFixedValueList", picklist.FixedValueList);
                cmd.AppendBit("@bitCustomizeValue", picklist.CustomizeValue);
                cmd.AppendBit("@bitIsKeyValueType", picklist.IsKeyValueType);
                cmd.AppendBit("@bitActive", picklist.Active);
                cmd.AppendBit("@bitIsDeletetd", picklist.IsDeleteted);
                cmd.AppendGuid("@guidUpdatedBy", picklist.UpdatedBy);
                SqlParameter retvalue = cmd.AppendInt("@intReturn");
                ExecuteCommand(cmd);
                if ((int)retvalue.Value == 1)
                    throw new VPCException(DbErrorCode.Duplicate);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "Picklist::Create");
            }
        }

        internal bool Update(string tenantCode, short id, Picklist picklist)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", id);
                cmd.AppendSmallText("@strName", picklist.Name);
                cmd.AppendBit("@bitIsStandard", picklist.IsStandard);
                if (picklist.EntityId > 0)
                    cmd.AppendSmallInt("@intEntityId", picklist.EntityId);
                cmd.AppendBit("@bitFixedValueList", picklist.FixedValueList);
                cmd.AppendBit("@bitCustomizeValue", picklist.CustomizeValue);
                cmd.AppendBit("@bitIsKeyValueType", picklist.IsKeyValueType);
                cmd.AppendBit("@bitActive", picklist.Active);
                cmd.AppendBit("@bitIsDeletetd", picklist.IsDeleteted);
                cmd.AppendGuid("@guidUpdatedBy", picklist.UpdatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "Picklist::Update");
            }
        }

        internal bool UpdateStatus(string tenantCode, short id, Guid updatedBy)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_Update_Status");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", id);
                cmd.AppendGuid("@guidUpdatedBy", updatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "Picklist::UpdateStatus");
            }
        }

        internal bool SoftDelete(string tenantCode, short id, Guid updatedBy)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_SoftDelete");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", id);
                cmd.AppendGuid("@guidUpdatedBy", updatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "Picklist::SoftDelete");
            }


        }

        internal bool Delete(string tenantCode, short id)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_Delete");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", id);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "Picklist::Delete");
            }
        }

        internal Picklist GetById(string tenantCode, short id)
        {
            var picklist = new Picklist();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_GetBy_Id");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intId", id);
                using (var reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        picklist.Id = (short)(reader.IsDBNull(0) ? 0 : reader.GetInt16(0));
                        picklist.Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        picklist.IsStandard = !reader.IsDBNull(2) && reader.GetBoolean(2);
                        picklist.EntityId = (short)(reader.IsDBNull(3) ? 0 : reader.GetInt16(3));
                        picklist.FixedValueList = !reader.IsDBNull(4) && reader.GetBoolean(4);
                        picklist.CustomizeValue = !reader.IsDBNull(5) && reader.GetBoolean(5);
                        picklist.IsKeyValueType = !reader.IsDBNull(6) && reader.GetBoolean(6);
                        picklist.Active = !reader.IsDBNull(7) && reader.GetBoolean(7);
                        picklist.IsDeleteted = !reader.IsDBNull(8) && reader.GetBoolean(8);
                        picklist.UpdatedBy = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9);
                        picklist.UpdatedDate = reader.IsDBNull(10) ? DateTime.MinValue : reader.GetDateTime(10);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Picklist::GetById");
            }
            return picklist;
        }

        internal List<Picklist> GetAll(string tenantCode, PicklistQuery query)
        {
            var lstPicklists = new List<Picklist>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Picklist_GetAll");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendInt("@intPageNumber", query.PagingParameters.PageNumber);
                cmd.AppendInt("@intPageSize", query.PagingParameters.PageSize);

                if (!string.IsNullOrEmpty(query.SearchText))
                {
                    cmd.AppendSmallText("@strSearchText", query.SearchText);
                }
                cmd.AppendBit("@bitIsdeleted", query.IsDeleted);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var info = new Picklist()
                        {
                            Id = (short)(reader.IsDBNull(0) ? 0 : reader.GetInt16(0)),
                            Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            IsStandard = !reader.IsDBNull(2) && reader.GetBoolean(2),
                            EntityId = (short)(reader.IsDBNull(3) ? 0 : reader.GetInt16(3)),
                            FixedValueList = !reader.IsDBNull(4) && reader.GetBoolean(4),
                            CustomizeValue = !reader.IsDBNull(5) && reader.GetBoolean(5),
                            IsKeyValueType = !reader.IsDBNull(6) && reader.GetBoolean(6),
                            Active = !reader.IsDBNull(7) && reader.GetBoolean(7),
                            IsDeleteted = !reader.IsDBNull(8) && reader.GetBoolean(8),
                            UpdatedBy = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9),
                            UpdatedDate = reader.IsDBNull(10) ? DateTime.MinValue : reader.GetDateTime(10)
                        };
                        lstPicklists.Add(info);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Picklist::GetAll");
            }
            return lstPicklists;
        }
        #endregion

        #region Picklist values

        internal void CreatePicklistValue(string tenantCode, PicklistValue picklistValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", picklistValue.Values.Id);
                cmd.AppendSmallInt("@intPicklistId", picklistValue.Values.PicklistId);
                cmd.AppendSmallText("@strKey", picklistValue.Values.Key);
                cmd.AppendMediumText("@strText", picklistValue.Values.Text);
                cmd.AppendBit("@bitActive", picklistValue.Values.Active);
                cmd.AppendBit("@bitIsDeletetd", picklistValue.Values.IsDeleteted);
                cmd.AppendBit("@bitFlagged", picklistValue.Values.Flagged);
                cmd.AppendGuid("@guidUpdatedBy", picklistValue.Values.UpdatedBy);

                SqlParameter retvalue = cmd.AppendInt("@intReturn");
                ExecuteCommand(cmd);
                if ((int)retvalue.Value == 1)
                    throw new VPCException(DbErrorCode.Duplicate);
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PicklistValue::Create");
            }
        }

        internal bool UpdatePicklistValue(string tenantCode, Guid id, PicklistValue picklistValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", picklistValue.Values.Id);
                cmd.AppendSmallText("@strKey", picklistValue.Values.Key);
                cmd.AppendMediumText("@strText", picklistValue.Values.Text);
                cmd.AppendBit("@bitActive", picklistValue.Values.Active);
                cmd.AppendBit("@bitIsDeletetd", picklistValue.Values.IsDeleteted);
                cmd.AppendBit("@bitFlagged", picklistValue.Values.Flagged);
                cmd.AppendGuid("@guidUpdatedBy", picklistValue.Values.UpdatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PicklistValue::Update");
            }
        }

        internal bool SoftDeletePicklistValue(string tenantCode, Guid id, short picklistId, Guid updatedBy)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_SoftDelete");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", id);
                cmd.AppendGuid("@guidUpdatedBy", updatedBy);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PicklistValue::SoftDelete");
            }
        }

        internal bool DeletePicklistValue(string tenantCode, short picklistId, Guid id)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Delete");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", id);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PicklistValue::Delete");
            }
        }

        internal bool UpdatePicklistValueStatus(string tenantCode, short picklistId, Guid id, Guid updatedBy)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Update_Status");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", id);
                cmd.AppendGuid("@guidUpdatedBy", updatedBy);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PicklistValue::UpdateStatus");
            }
        }

        internal Values GetPicklistValueById(string tenantCode, short picklistId, Guid id)
        {
            var picklistValue = new Values();
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_GetBy_Id");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", id);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                using (var reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        picklistValue = new Values();
                        picklistValue.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        picklistValue.PicklistId = (short)(reader.IsDBNull(1) ? 0 : reader.GetInt16(1));
                        picklistValue.Key = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        picklistValue.Text = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        picklistValue.Active = !reader.IsDBNull(4) && reader.GetBoolean(4);
                        picklistValue.IsDeleteted = !reader.IsDBNull(5) && reader.GetBoolean(5);
                        picklistValue.Flagged = !reader.IsDBNull(6) && reader.GetBoolean(6);
                        picklistValue.UpdatedBy = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        picklistValue.UpdatedDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "PicklistValue::GetById");
            }
            return picklistValue;
        }

        internal List<Values> GetAllPicklistValues(string tenantCode, short picklistId, PicklistQuery query)
        {
            var pickListValues = new List<Values>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_GetAll");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendInt("@intPageNumber", query.PagingParameters.PageNumber);
                cmd.AppendInt("@intPageSize", query.PagingParameters.PageSize);

                if (!string.IsNullOrEmpty(query.SearchText))
                {
                    cmd.AppendSmallText("@strSearchText", query.SearchText);
                }
                cmd.AppendBit("@bitIsdeleted", query.IsDeleted);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {

                        var info = new Values()
                        {
                            Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                            PicklistId = (short)(reader.IsDBNull(1) ? 0 : reader.GetInt16(1)),
                            Key = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Text = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            Active = !reader.IsDBNull(4) && reader.GetBoolean(4),
                            IsDeleteted = !reader.IsDBNull(5) && reader.GetBoolean(5),
                            Flagged = !reader.IsDBNull(6) && reader.GetBoolean(6),
                            UpdatedBy = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7),
                            UpdatedDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8)
                        };
                        pickListValues.Add(info);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "PicklistValue::GetAll");
            }
            return pickListValues;
        }
        #endregion

        #region Picklist values favourite

        internal List<PickListValueFavourite> GetPicklistValueFavourites(string tenantCode, short picklistId, Guid userId)
        {
            var lstPicklistFavourites = new List<PickListValueFavourite>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_GetFavourite");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                cmd.AppendGuid("@guidUserId", userId);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var info = new PickListValueFavourite
                        {
                            PicklistId = (short)(reader.IsDBNull(0) ? 0 : reader.GetInt16(0)),
                            PicklistValueId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                            UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                        };
                        lstPicklistFavourites.Add(info);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "PickListValueFavourite::GetPicklistValueFavourites");
            }
            return lstPicklistFavourites;
        }

        internal void CreatePicklistValueFavourite(string tenantCode, PickListValueFavourite pickListValueFavourite)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Create_Favourite");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendSmallInt("@intPicklistId", pickListValueFavourite.PicklistId);
                cmd.AppendGuid("@guidPicklistValueId", pickListValueFavourite.PicklistValueId);
                cmd.AppendGuid("@guidUserId", pickListValueFavourite.UserId);

                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueFavourite::CreatePicklistValueFavourite");
            }
        }

        internal bool DeletePicklistValuefavourite(string tenantCode, short picklistId, Guid picklistValueId, Guid userId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Favourite_Delete");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidPicklistValueId", picklistValueId);
                cmd.AppendSmallInt("@intPicklistId", picklistId);
                cmd.AppendGuid("@guidUserId", userId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueFavourite::Delete");
            }
        }
        #endregion

        internal void CreateCountryExtendedValue(string tenantCode, PickListValueForCountry counteryExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Country_ExtendedValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", counteryExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", counteryExtendedValue.PicklistValueId);
                cmd.AppendGuid("@guidCurrency", counteryExtendedValue.Currency);
                cmd.AppendGuid("@guidLanguage", counteryExtendedValue.Language);
                cmd.AppendGuid("@guidTimezone", counteryExtendedValue.Timezone);
                cmd.AppendSmallText("@strIsoCode", counteryExtendedValue.IsoCode);
                cmd.AppendSmallText("@strNationality", counteryExtendedValue.Nationality);

                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForCountry::Create");
            }
        }

        internal void UpdateCountryExtendedValue(string tenantCode, PickListValueForCountry counteryExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Country_ExtendedValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", counteryExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", counteryExtendedValue.PicklistValueId);
                cmd.AppendGuid("@guidCurrency", counteryExtendedValue.Currency);
                cmd.AppendGuid("@guidLanguage", counteryExtendedValue.Language);
                cmd.AppendGuid("@guidTimezone", counteryExtendedValue.Timezone);
                cmd.AppendSmallText("@strIsoCode", counteryExtendedValue.IsoCode);
                cmd.AppendSmallText("@strNationality", counteryExtendedValue.Nationality);

                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForCountry::Update");
            }
        }

        internal void CreateCurrencyExtendedValue(string tenantCode, PickListValueForCurrency currencyExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Currency_ExtendedValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", currencyExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", currencyExtendedValue.PicklistValueId);
                cmd.AppendTinyInt("@guidCurrency", currencyExtendedValue.DecimalPrecision);
                cmd.AppendTinyInt("@guidLanguage", currencyExtendedValue.DecimalVisualization);
                cmd.AppendSmallText("@strIsoCode", currencyExtendedValue.IsoCode);

                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForCurrency::Create");
            }
        }

        internal void UpdateCurrencyExtendedValue(string tenantCode, PickListValueForCurrency currencyExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Currency_ExtendedValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", currencyExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", currencyExtendedValue.PicklistValueId);
                cmd.AppendTinyInt("@guidCurrency", currencyExtendedValue.DecimalPrecision);
                cmd.AppendTinyInt("@guidLanguage", currencyExtendedValue.DecimalVisualization);
                cmd.AppendSmallText("@strIsoCode", currencyExtendedValue.IsoCode);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForCurrency::Update");
            }
        }

        internal void CreateLanguageExtendedValue(string tenantCode, PickListValueForLanguage languageExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Language_ExtendedValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", languageExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", languageExtendedValue.PicklistValueId);
                cmd.AppendSmallText("@strDateFormat", languageExtendedValue.DateFormat);
                cmd.AppendSmallText("@strIsoCode", languageExtendedValue.IsoCode);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForLanguage::Create");
            }
        }

        internal void UpdateLanguageExtendedValue(string tenantCode, PickListValueForLanguage languageExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Language_ExtendedValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", languageExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", languageExtendedValue.PicklistValueId);
                cmd.AppendSmallText("@strDateFormat", languageExtendedValue.DateFormat);
                cmd.AppendSmallText("@strIsoCode", languageExtendedValue.IsoCode);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForLanguage::Update");
            }
        }

        internal void CreateTimezoneExtendedValue(string tenantCode, PickListValueForTimeZone timeZoneExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Timezone_ExtendedValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", timeZoneExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", timeZoneExtendedValue.PicklistValueId);
                cmd.AppendDecimal("@decGmtDeviation", timeZoneExtendedValue.GmtDeviation);
                if (!string.IsNullOrEmpty(timeZoneExtendedValue.SummerTimeStart))
                    cmd.AppendXLargeText("@strSummerTimeStart", timeZoneExtendedValue.SummerTimeStart);
                if (!string.IsNullOrEmpty(timeZoneExtendedValue.WinterTimeStart))
                    cmd.AppendXLargeText("@strWinterTimeStart", timeZoneExtendedValue.WinterTimeStart);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForTimeZone::Create");
            }
        }

        internal void UpdateTimezoneExtendedValue(string tenantCode, PickListValueForTimeZone timeZoneExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_Timezone_ExtendedValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", timeZoneExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", timeZoneExtendedValue.PicklistValueId);
                cmd.AppendDecimal("@decGmtDeviation", timeZoneExtendedValue.GmtDeviation);
                if (!string.IsNullOrEmpty(timeZoneExtendedValue.SummerTimeStart))
                    cmd.AppendXLargeText("@strSummerTimeStart", timeZoneExtendedValue.SummerTimeStart);
                if (!string.IsNullOrEmpty(timeZoneExtendedValue.WinterTimeStart))
                    cmd.AppendXLargeText("@strWinterTimeStart", timeZoneExtendedValue.WinterTimeStart);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForTimeZone::Update");
            }
        }

        internal void CreateStateExtendedValue(string tenantCode, PickListValueForState stateExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_State_ExtendedValue_Create");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", stateExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", stateExtendedValue.PicklistValueId);
                cmd.AppendGuid("@guidCountryId", stateExtendedValue.Country);

                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForState::Create");
            }
        }

        internal void UpdateStateExtendedValue(string tenantCode, PickListValueForState stateExtendedValue)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.PicklistValue_State_ExtendedValue_Update");
                cmd.AppendXSmallText("@strTenantCode", tenantCode);
                cmd.AppendGuid("@guidId", stateExtendedValue.Id);
                cmd.AppendGuid("@guidPicklistValueId", stateExtendedValue.PicklistValueId);
                cmd.AppendGuid("@guidCountryId", stateExtendedValue.Country);
                ExecuteCommand(cmd);

            }
            catch (SqlException e)
            {
                _log.Error(e);
                throw ReportAndTranslateException(e, "PickListValueForState::Update");
            }
        }
    }
}