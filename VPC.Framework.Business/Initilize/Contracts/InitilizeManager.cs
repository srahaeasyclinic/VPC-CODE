using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.TenantSubscription;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.Credential.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.Initilize.APIs;
using VPC.Framework.Business.Initilize.StaticContents;
using VPC.Framework.Business.Menu.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.APIs;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.Metadata.Business.DataTypes.Complex;
using VPC.Metadata.Business.Entity.Infrastructure;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Entities.WorkFlow.Engine;
using VPC.Framework.Business.Initilize.Contracts.Helper;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Utilities;
using System.IO;

namespace VPC.Framework.Business.Initilize.Contracts
{
    public interface IInitilizeManager
    {
        Guid GetRootTenantCode();
        List<LayoutModel> GetRootTenantLayouts(Guid tenantId);
        bool InitializeRootTenantWorkFlow(Guid tenantId);

        // Delete it later
        bool InitializeRootTenantWorkFlow(Guid tenantId, string entityId);
        InitilizeResponseMessage Initilize(Guid tenantId, List<string> entityIds, Guid userId, Guid subscriptionId);

        void Test(Guid root, Guid newTen);
    }

    public sealed class InitilizeManager : IInitilizeManager
    {
        private readonly IInitilizeReview _review;
        private readonly IManagerWorkFlowSecurity _managerWorkFlow;
        private readonly IPicklistManager _picklIstManager;
        IMetadataManager iMetadataManager;
        private readonly IMenuManager _IMenuManager;

        public string Empty { get; private set; }

        public InitilizeManager()
        {
            _review = new InitilizeReview();
            _managerWorkFlow = new ManagerWorkFlowSecurity();
            _picklIstManager = new PicklistManager();
            iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            _IMenuManager = new MenuManager();
        }

        Guid IInitilizeManager.GetRootTenantCode()
        {
            return _review.getRootTenantCode();
        }

        List<LayoutModel> IInitilizeManager.GetRootTenantLayouts(Guid tenantId)
        {
            return _review.GetRootTenantLayouts(tenantId);
        }

        #region Initialization
        InitilizeResponseMessage Initilize_new(Guid initilizedTenantCode, List<string> entityIds, Guid userId)
        {
            return null;
        }

        InitilizeResponseMessage IInitilizeManager.Initilize(Guid initilizedTenantCode, List<string> entityIds, Guid userId, Guid masterSubscriptionId)
        {
            var responses = new InitilizeResponseMessage();
            responses.Info = new List<Informatiom>();
            var info = new Informatiom();
            var entities = new List<EntityMessageInfo>();
            var rootTenantCode = _review.getRootTenantCode();

            if (rootTenantCode == Guid.Empty)
            {
                info.ErrorLevel = 1;
                info.Message = "Root tenant not found.";
                responses.Info.Add(info);
                return responses;
            }

            var rootTenantLayouts = _review.GetRootTenantLayouts(rootTenantCode);

            if (rootTenantLayouts == null)
            {
                info.ErrorLevel = 1;
                info.Message = "Root tenant does not have any layouts";
                responses.Info.Add(info);
                return responses;
            }

            if (initilizedTenantCode == Guid.Empty)
            {
                info.ErrorLevel = 1;
                info.Message = "Initialize tenant code not found.";
                responses.Info.Add(info);
                return responses;
            }

            // List<TypeInfo> metadataClasses = Assembly
            //     .GetEntryAssembly()
            //     .GetReferencedAssemblies()
            //     .Select(Assembly.Load)
            //     .SelectMany(x => x.DefinedTypes)
            //     .Where(type =>
            //        typeof(EntityBase).GetTypeInfo().IsAssignableFrom(type.AsType()) && !type.IsAbstract).ToList();

            List<TypeInfo> metadataClasses = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(type =>
                   typeof(EntityBase).GetTypeInfo().IsAssignableFrom(type.AsType()) &&
                   !type.IsAbstract).ToList();

            foreach (var id in entityIds)
            {
                foreach (var item in metadataClasses)
                {

                    var contextId = iMetadataManager.GetEntityContextByEntityName(item.Name);
                    if (!contextId.Equals(id)) continue;

                    var details = iMetadataManager.GetEntitityByName(item.Name);

                    var entityInfo = new EntityMessageInfo();
                    entityInfo.EntityContext = id;
                    entityInfo.LayoutFor = LayoutFor.Metadata;
                    entityInfo.IsSupportWorkflow = details.SupportWorkflow;
                    entityInfo.EntityName = details.Name;
                    if (details.Subtypes != null && details.Subtypes.Any())
                    {
                        entityInfo.Subtypes = new List<string>();
                        entityInfo.Subtypes.AddRange(details.Subtypes);
                    }

                    if (details.Operations != null && details.Operations.Any())
                    {
                        entityInfo.Operations = new List<Operation>();
                        entityInfo.Operations.AddRange(details.Operations);
                    }

                    if (details.Fields != null && details.Fields.Count > 0)
                    {
                        foreach (var field in details.Fields)
                        {
                            if (field.DataType == "PickList")
                            {
                                var pickListDetails = iMetadataManager.GetEntitityByName(field.TypeOf);
                                if (pickListDetails.Subtypes == null || pickListDetails.Subtypes.Count == 0)
                                {
                                    var entityPicklistInfo = new EntityMessageInfo();
                                    entityPicklistInfo.EntityContext = iMetadataManager.GetEntityContextByEntityName(field.TypeOf, true);
                                    entityPicklistInfo.LayoutFor = LayoutFor.Picklist;
                                    entityPicklistInfo.IsSupportWorkflow = false;
                                    entityPicklistInfo.EntityName = field.TypeOf;
                                    entities.Add(entityPicklistInfo);
                                }
                            }
                        }
                    }
                    entities.Add(entityInfo);
                    InitPickListValues(rootTenantCode, initilizedTenantCode, details, userId);

                }

            }

            IEnumerable<EntityMessageInfo> noDuplicatesEntities = entities.Distinct();

            ILayoutManager layout = new LayoutManager();

            var entityMessageInfos = noDuplicatesEntities.ToList();
            //Initialize the meta data entities layouts
            // InitializeMetadataLayouts (entityMessageInfos, responses, info, rootTenantLayouts, layout, userId, initilizedTenantCode);
            var entityList = entityMessageInfos.Where(t => t.LayoutFor.Equals(LayoutFor.Metadata)).Select(t => t.EntityContext).ToList();
            var statusEnity = InitializeMedataLayoutsByIds(entityList, rootTenantCode, initilizedTenantCode);

            //Initialize the Pick list entities layouts
            //  InitializePicklistLayouts(entityMessageInfos, responses, info, rootTenantLayouts, layout, userId, initilizedTenantCode);
            var picklists = entityMessageInfos.Where(t => t.LayoutFor.Equals(LayoutFor.Picklist)).Select(t => t.EntityContext).ToList();
            if (!picklists.Contains("10015"))
            {
                picklists.Add("10015");
            }
            var statusPicklist = InitializePicklistLayoutsByIds(picklists, rootTenantCode, initilizedTenantCode);

            //Initialize work flow 
            InitilizeTenantWorkflow(rootTenantCode, initilizedTenantCode, entityIds);

            //Initialize Picklist
            //InitializePicklistValue(picklists, rootTenantCode, initilizedTenantCode, userId);

            //Init Menu group
            InitMenuGroup(rootTenantCode, initilizedTenantCode, userId);

            //Initialize Menu Item
            InitializeMenu(rootTenantCode, initilizedTenantCode);

            // Initialize parent child relation of menu
            InitializeParentMenu(rootTenantCode, initilizedTenantCode);

            //Initialize tenant subscription
            var subscriptionId = InitilizeTenantSubscription(rootTenantCode, initilizedTenantCode, masterSubscriptionId);

            //Init Email Template
            InitEmailTemplate(initilizedTenantCode, userId);

            //Intialize Batch type
            //InitBatchType(rootTenantCode, initilizedTenantCode);
            //Initilize Password policy
            InitCredential(rootTenantCode, initilizedTenantCode, userId);
            // Initlize Smtp settings
            InitSmtpSettings(rootTenantCode, initilizedTenantCode);
            //Init Role view
            IntiRoleView(initilizedTenantCode);

            //Initialize resources
            //InitilizeResources (rootTenantCode, initilizedTenantCode);
            List<TypeInfo> metadataClassesForResource = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(type =>
                   (typeof(EntityBase).GetTypeInfo().IsAssignableFrom(type.AsType()) ||
                       typeof(PicklistBase).GetTypeInfo().IsAssignableFrom(type.AsType())) &&
                   !type.IsAbstract).ToList();

            if (metadataClassesForResource.Count > 0)
            {
                InitilizeResources(rootTenantCode, initilizedTenantCode, metadataClassesForResource);
            }
            else
            {
                InitilizeResources(rootTenantCode, initilizedTenantCode);
            }
            // Replace Default Values
          ReplaceDefaultValues(rootTenantCode, initilizedTenantCode);
            return responses;
        }

        private void IntiRoleView(Guid newTenantId)
        {

            ILayoutManager _iLayoutManager = new LayoutManager();
            var userLayouts = _iLayoutManager.GetLayoutsByEntityName(newTenantId, "User");
            if (userLayouts.Count > 0)
            {
                var roleLayout = _iLayoutManager.GetLayoutsByEntityName(newTenantId, "Role");
                LayoutModel viewLayout = new LayoutModel();
                if (roleLayout.Count > 0)
                {
                    viewLayout = (from roleLay in roleLayout where roleLay.LayoutType == LayoutType.View select roleLay).FirstOrDefault();
                }

                foreach (var userLayout in userLayouts)
                {
                    if (userLayout.LayoutType == LayoutType.Form)
                    {
                        var details = _iLayoutManager.GetLayoutsDetailsById(newTenantId, userLayout.Id);
                        if (details.FormLayoutDetails != null)
                        {
                            foreach (var detail in details.FormLayoutDetails.Fields)
                            {
                                if (detail.DataType == "Section")
                                {
                                    foreach (var innerField in detail.Fields)
                                    {
                                        if (innerField.DataType == "Role" && innerField.Name == "UserInRole")
                                        {
                                            innerField.SelectedView = viewLayout.Id;

                                            details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                            {
                                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                                            });

                                            _iLayoutManager.UpdateLayoutDetails(newTenantId, userLayout.Id, details);
                                        }
                                    }
                                }
                                else if (detail.DataType == "Tabs")
                                {
                                    foreach (var innerField in detail.Fields)
                                    {
                                        if (innerField.DataType == "Role" && innerField.Name == "UserInRole")
                                        {
                                            innerField.SelectedView = viewLayout.Id;

                                            details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                            {
                                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                                            });

                                            _iLayoutManager.UpdateLayoutDetails(newTenantId, userLayout.Id, details);
                                        }

                                    }

                                }
                                else if (detail.DataType == "Tabs")
                                {
                                    foreach (var innerField in detail.Fields)
                                    {
                                        if (innerField.DataType == "Role" && innerField.Name == "UserInRole")
                                        {
                                            innerField.SelectedView = viewLayout.Id;

                                            details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                            {
                                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                                            });

                                            _iLayoutManager.UpdateLayoutDetails(newTenantId, userLayout.Id, details);
                                        }

                                    }

                                }
                                else if (detail.DataType == "Role" && detail.Name == "UserInRole")
                                {
                                    detail.SelectedView = viewLayout.Id;

                                    details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                    {
                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                    });

                                    _iLayoutManager.UpdateLayoutDetails(newTenantId, userLayout.Id, details);
                                }

                            }
                        }
                    }
                }

            }
        }

        private void InitMenuGroup(Guid rootTenantId, Guid newTenantId, Guid userId)
        {
            var fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForMenuGroup.Displayorder";
            SavePickListValues(rootTenantId, newTenantId, "ApplicationMenuGroup", fields, userId);
        }

        private void InitPickListValues(Guid rootTenantId, Guid newTenantId, Entity entityDetails, Guid userId)
        {
            var dataTypePicklist = DataUtility.GetEnumName(VPC.Metadata.Business.DataAnnotations.DataType.PickList);
            var getAllPickListValueLists = (from entityDetail in entityDetails.Fields where entityDetail.DataType == dataTypePicklist select entityDetail).ToList();
            var picklists = _picklIstManager.GetAllCustomizablePicklist();
            foreach (var getAllPickListValueList in getAllPickListValueLists)
            {

                var checkAllowedToInit = (from picklist in picklists where picklist == getAllPickListValueList.TypeOf select picklist).FirstOrDefault();
                if (checkAllowedToInit != null)
                {
                    string fields = string.Empty;
                    if (getAllPickListValueList.TypeOf == "Currency")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForCurrency.DecimalPrecision,PickListValueForCurrency.DecimalVisualization,PickListValueForCurrency.IsoCode";
                    }
                    else if (getAllPickListValueList.TypeOf == "Language")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForLanguage.DateFormat,PickListValueForLanguage.IsoCode";
                    }
                    else if (getAllPickListValueList.TypeOf == "SecurityFunction")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForSecurityFunction.scopeEntityId";
                    }
                    else if (getAllPickListValueList.TypeOf == "Timezone")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForTimeZone.GmtDeviation,PickListValueForTimeZone.SummerTimeStart,PickListValueForTimeZone.WinterTimeStart";
                    }
                    else if (getAllPickListValueList.TypeOf == "ApplicationMenuGroup")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForMenuGroup.Displayorder";
                    }
                    else
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault";
                    }

                    if (getAllPickListValueList.TypeOf != "AutoReleaseMode")
                        SavePickListValues(rootTenantId, newTenantId, getAllPickListValueList.TypeOf, fields, userId);

                }
            }

        }

        private List<FieldModel> GetPickListFields(Entity entityDetails)
        {
            var dataTypePicklist = DataUtility.GetEnumName(VPC.Metadata.Business.DataAnnotations.DataType.PickList);
            var getAllPickListValueLists = (from entityDetail in entityDetails.Fields where entityDetail.DataType == dataTypePicklist select entityDetail).ToList();
            //picklists = _picklIstManager.GetAllCustomizablePicklist();
           

            return getAllPickListValueLists;

        }

        private void SavePickListValues(Guid rootTenantId, Guid newTenantId, string pickListType, string fields, Guid userId)
        {

            var rootTenantFilter = new List<QueryFilter>();
            rootTenantFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = rootTenantId.ToString() });
            var rootTenantContext = new QueryContext { Fields = fields, Filters = rootTenantFilter, PageSize = 100, PageIndex = 1 };
            var rootTenantPickLists = _picklIstManager.GetPicklistValues(rootTenantId, pickListType, rootTenantContext);

            if (rootTenantPickLists.Rows.Count > 0)
            {

                var newTenantFilter = new List<QueryFilter>();
                newTenantFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = newTenantId.ToString() });
                var newTenantContext = new QueryContext { Fields = fields, Filters = newTenantFilter, PageSize = 100, PageIndex = 1 };
                var newTenantPickLists = _picklIstManager.GetPicklistValues(rootTenantId, pickListType, newTenantContext);
                if (newTenantPickLists.Rows.Count == 0)
                {

                    var columnNames = rootTenantPickLists.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                    for (int i = 0; i < rootTenantPickLists.Rows.Count; i++)
                    {
                        JObject person = new JObject();
                        foreach (var columnName in columnNames)
                        {
                            if (columnName == "InternalId")
                            {
                                person.Add(new JProperty(columnName, Guid.NewGuid().ToString()));
                            }
                            else
                            {
                                var colName = columnName;
                                if (columnName.Contains("_"))
                                {
                                    colName = columnName.Replace("_", ".");
                                }
                                if (!string.IsNullOrEmpty(rootTenantPickLists.Rows[i][columnName].ToString()))
                                    person.Add(new JProperty(colName, rootTenantPickLists.Rows[i][columnName].ToString()));
                            }
                        }
                        _picklIstManager.SavePicklistValue(newTenantId, userId, pickListType, person);
                    }
                }
            }
        }

        private void InitSmtpSettings(Guid rootTenantId, Guid newTenantId)
        {
            ISettingManager _settingManager = new SettingManager();
            var settings = _settingManager.GetSettings(rootTenantId);
            if (settings.Count > 0)
            {
                foreach (var setting in settings)
                {
                    _settingManager.CreateSetting(newTenantId, setting);
                }

            }

        }

        private void InitCredential(Guid rootTenantId, Guid newTenantId, Guid userId)
        {

            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = newTenantId.ToString() });
            var queryContext = new QueryContext { Fields = "InternalId", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            DataTable dataTableUser = _iEntityResourceManager.GetResultById(newTenantId, "user", userId, queryContext);
            User userEntity = EntityMapper<VPC.Entities.EntityCore.Metadata.User>.Mapper(dataTableUser);
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            PasswordPolicy passwordpolicy = sqlMembership.getPasswordPolicy(newTenantId, true);
            IManagerCredential crd = new ManagerCredential();
            CredentialInfo credentialData = crd.GetCredential(newTenantId, Guid.Parse(userEntity.InternalId.Value));
            var isnew = false;
            if (passwordpolicy != null)
            {
                isnew = passwordpolicy.ResetOnFirstLogin.Value;
            }
            crd.SetIsNew(newTenantId, new CredentialInfo
            {
                CredentialId = credentialData.CredentialId,
                ParentId = credentialData.ParentId,
                IsNew = isnew
            });

        }
        private void InitEmailTemplate(Guid initilizedTenantCode, Guid userId)
        {

            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            var emailTemplateSubTypes = _iMetadataManager.GetSubTypes("Emailtemplate");
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();

            string entityName = "User";
            // string body=@"<p>&nbsp;</p><p>Dear [FirstName]&nbsp;[MiddleName]&nbsp;[LastName] ,&nbsp;<br />Welcome to Aboard!</p><p>Your credential is</p><p>Tenantcode:&nbsp;[TenantCode]</p><p>Username:&nbsp;[UserCredential.Username]</p><p>Password:&nbsp;[UserCredential.Password]</p><p>Reagrds,</p><p>View Team,</p><p>&nbsp;</p>";
            string entityContext = "EN10016";

            //New User
            dynamic jsonObjectEmailTemplate = new JObject();
            jsonObjectEmailTemplate.Title = EmailTemplatesContent.NewUserCredentialTitle;
            jsonObjectEmailTemplate.Context = entityName;
            jsonObjectEmailTemplate.Body = EmailTemplatesContent.NewUserCredentialBody;
            jsonObjectEmailTemplate.CommunicationContextType = (int)ContextTypeEnum.Welcome;
            jsonObjectEmailTemplate.EntityContext = entityContext;

            var myObj = new JObject();
            myObj.Add("Emailtemplate", jsonObjectEmailTemplate);
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", myObj, emailTemplateSubTypes[0].Name.ToString());

            //Reset Password
            //body=@"<p>&nbsp;</p><p>Dear [FirstName]&nbsp;[MiddleName]&nbsp;[LastName] ,&nbsp;<br />Welcome to Aboard!</p><p>Your credential is</p><p>Username:&nbsp;[UserCredential.Username]</p><p>Password:&nbsp;[UserCredential.Password]</p><p>Reagrds,</p><p>View Team,</p><p>&nbsp;</p>";
            dynamic jsonObjectResetPassword = new JObject();
            jsonObjectResetPassword.Title = EmailTemplatesContent.ResetPasswordTitle;
            jsonObjectResetPassword.Context = entityName;
            jsonObjectResetPassword.Body = EmailTemplatesContent.ResetPasswordBody; //.Replace("&", "&amp;").Replace("'", "&apos;");
            jsonObjectResetPassword.CommunicationContextType = (int)ContextTypeEnum.Passwordreset;
            jsonObjectResetPassword.EntityContext = entityContext;
            myObj = new JObject();
            myObj.Add("Emailtemplate", jsonObjectResetPassword);
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", myObj, emailTemplateSubTypes[0].Name.ToString());

            //Change Password
            dynamic jsonObjectChangePassword = new JObject();
            jsonObjectChangePassword.Title = EmailTemplatesContent.ChangePasswordTitle;
            jsonObjectChangePassword.Context = entityName;
            jsonObjectChangePassword.Body = EmailTemplatesContent.ChangePasswordBody; //.Replace ("&", "&amp;").Replace ("'", "&apos;");;
            jsonObjectChangePassword.CommunicationContextType = (int)ContextTypeEnum.Forgotpassword;
            jsonObjectChangePassword.EntityContext = entityContext;
            myObj = new JObject();
            myObj.Add("Emailtemplate", jsonObjectChangePassword);
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", myObj, emailTemplateSubTypes[0].Name.ToString());

            //User export
            dynamic jsonUserExport = new JObject();
            jsonUserExport.Title = EmailTemplatesContent.UserExportTitle;
            jsonUserExport.Context = entityName;
            jsonUserExport.Body = EmailTemplatesContent.UserExportBody; //.Replace ("&", "&amp;").Replace ("'", "&apos;");;
            jsonUserExport.CommunicationContextType = (int)ContextTypeEnum.ExportUser;
            jsonUserExport.EntityContext = entityContext;
            myObj = new JObject();
            myObj.Add("Emailtemplate", jsonUserExport);
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", myObj, emailTemplateSubTypes[0].Name.ToString());

        }

        private bool InitializePicklistLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
            IInitilizeAdmin _InitilizeAdmin = new InitilizeAdmin();
            return _InitilizeAdmin.InitializePickListLayoutsByIds(picklists, rootTenantCode, initilizedTenantCode);
        }

        private bool InitializeMedataLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
            IInitilizeAdmin _InitilizeAdmin = new InitilizeAdmin();
            return _InitilizeAdmin.InitializeMedataLayoutsByIds(picklists, rootTenantCode, initilizedTenantCode);
        }

        #endregion

        private void InitializeMetadataLayouts(IEnumerable<EntityMessageInfo> entityPicklistContext, InitilizeResponseMessage responses, Informatiom info, List<LayoutModel> rootTenantLayouts, ILayoutManager layout, Guid userId, Guid tenantId)
        {
            var entities = entityPicklistContext.Where(z => z.LayoutFor == LayoutFor.Metadata).ToList();
            foreach (var id in entities)
            {
                var layouts = layout.GetLayoutsByEntityContext(tenantId, id.EntityContext);
                var entityDefaultLayouts = layouts.Where(x => x.DefaultLayout && x.EntityId == id.EntityContext).ToList();
                if (layouts.Count > 0)
                {

                    //get the entity layouts of type List,New and Edit
                    if (entityDefaultLayouts.Count > 0)
                    {
                        bool listLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.List) != null;
                        bool newLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add) != null;
                        bool editLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit) != null;

                        if (!listLayoutPresent)
                        {
                            //Create new list layout
                            LayoutModel defaultListLayout = rootTenantLayouts.FirstOrDefault(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List);
                            if (defaultListLayout != null)
                            {
                                layout.Create(defaultListLayout, userId, tenantId);
                            }
                        }

                        if (!newLayoutPresent)
                        {
                            //Create new layout
                            var defaultNewLayouts = rootTenantLayouts.Where(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List).ToList();
                            if (defaultNewLayouts.Count > 0)
                            {
                                foreach (var item in defaultNewLayouts)
                                {
                                    layout.Create(item, userId, tenantId);
                                }
                            }

                        }

                        if (!editLayoutPresent)
                        {
                            //Create new edit layout
                            var defaultEditLayouts = rootTenantLayouts.Where(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List).ToList();
                            if (defaultEditLayouts.Count > 0)
                            {
                                foreach (var item in defaultEditLayouts)
                                {
                                    layout.Create(item, userId, tenantId);
                                }
                            }
                        }

                    }
                    else
                    {
                        var defaultListLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.List && z.EntityId == id.EntityContext);
                        if (defaultListLayout == null)
                        {
                            info.ErrorLevel = 2;
                            info.Message = $"No default list layout found for the entity context {id}";
                            responses.Info.Add(info);
                        }
                        else
                        {
                            layout.Create(defaultListLayout, userId, tenantId);
                        }

                        var defaultNewLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add && z.EntityId == id.EntityContext).ToList();
                        foreach (var item in defaultNewLayouts)
                        {
                            layout.Create(item, userId, tenantId);
                        }

                        var defaultEditLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit && z.EntityId == id.EntityContext).ToList();
                        foreach (var item in defaultEditLayouts)
                        {
                            layout.Create(item, userId, tenantId);
                        }

                    }
                }
                else
                {
                    var defaultListLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.List && z.EntityId == id.EntityContext);
                    if (defaultListLayout == null)
                    {
                        info.ErrorLevel = 2;
                        info.Message = $"No default list layout found for the entity context {id}";
                        responses.Info.Add(info);
                    }
                    else
                    {
                        layout.Create(defaultListLayout, userId, tenantId);
                    }
                    var defaultNewLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add && z.EntityId == id.EntityContext).ToList();
                    foreach (var item in defaultNewLayouts)
                    {
                        layout.Create(item, userId, tenantId);
                    }

                    var defaultEditLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit && z.EntityId == id.EntityContext).ToList();
                    foreach (var item in defaultEditLayouts)
                    {
                        layout.Create(item, userId, tenantId);
                    }

                }
            }
        }

        private void InitializePicklistLayouts(IEnumerable<EntityMessageInfo> entityPicklistContext, InitilizeResponseMessage responses, Informatiom info, List<LayoutModel> rootTenantLayouts, ILayoutManager layout, Guid userId, Guid tenantId)
        {
            var picklists = entityPicklistContext.Where(c => c.LayoutFor == LayoutFor.Picklist);
            foreach (var id in picklists)
            {
                var layouts = layout.GetPicklistLayout(tenantId, id.EntityContext);
                if (layouts != null && layouts.Count > 0)
                {
                    //get the entity layouts of type List,New and Edit
                    var entityDefaultLayouts = layouts.Where(x => x.DefaultLayout && x.EntityId == id.EntityContext).ToList();
                    if (entityDefaultLayouts.Count > 0)
                    {
                        bool listLayoutPresent = entityDefaultLayouts.Where(z => z.LayoutType == LayoutType.List).FirstOrDefault() != null;
                        bool newLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add) != null;
                        bool editLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit) != null;

                        if (!listLayoutPresent)
                        {
                            //Create new list layout
                            LayoutModel defaultListLayout = rootTenantLayouts.FirstOrDefault(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List);
                            if (defaultListLayout != null)
                            {
                                layout.CreatePicklistLayout(defaultListLayout, userId, tenantId);
                            }
                        }

                        if (!newLayoutPresent)
                        {
                            //Create new layout
                            LayoutModel defaultNewLayout = rootTenantLayouts.FirstOrDefault(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List);
                            if (defaultNewLayout != null)
                            {
                                layout.CreatePicklistLayout(defaultNewLayout, userId, tenantId);
                            }

                        }

                        if (!editLayoutPresent)
                        {
                            //Create new edit layout
                            LayoutModel defaultEditLayout = rootTenantLayouts.FirstOrDefault(y => y.EntityId == id.EntityContext && y.LayoutType == LayoutType.List);
                            if (defaultEditLayout != null)
                            {
                                layout.CreatePicklistLayout(defaultEditLayout, userId, tenantId);
                            }

                        }

                    }
                    else
                    {
                        var defaultListLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.List && z.EntityId == id.EntityContext);
                        if (defaultListLayout == null)
                        {
                            info.ErrorLevel = 2;
                            info.Message = $"No default list layout found for the entity context {id}";
                            responses.Info.Add(info);
                        }
                        else
                        {
                            layout.CreatePicklistLayout(defaultListLayout, userId, tenantId);
                        }

                        var defaultNewLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add && z.EntityId == id.EntityContext);
                        if (defaultNewLayout == null)
                        {
                            info.ErrorLevel = 2;
                            info.Message = $"No default new layout found for the entity context {id}";
                            responses.Info.Add(info);
                        }
                        else
                        {
                            layout.CreatePicklistLayout(defaultNewLayout, userId, tenantId);
                        }

                        var defaultEditLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit && z.EntityId == id.EntityContext);
                        if (defaultEditLayout == null)
                        {
                            info.ErrorLevel = 2;
                            info.Message = $"No default edit layout found for the entity context {id}";
                            responses.Info.Add(info);
                        }
                        else
                        {
                            layout.CreatePicklistLayout(defaultEditLayout, userId, tenantId);
                        }
                    }
                }
                else
                {
                    var defaultListLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.List && z.EntityId == id.EntityContext);
                    if (defaultListLayout == null)
                    {
                        info.ErrorLevel = 2;
                        info.Message = $"No default list layout found for the entity context {id}";
                        responses.Info.Add(info);
                    }
                    else
                    {
                        layout.CreatePicklistLayout(defaultListLayout, userId, tenantId);
                    }

                    var defaultNewLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Add && z.EntityId == id.EntityContext);
                    if (defaultNewLayout == null)
                    {
                        info.ErrorLevel = 2;
                        info.Message = $"No default new layout found for the entity context {id}";
                        responses.Info.Add(info);
                    }
                    else
                    {
                        layout.CreatePicklistLayout(defaultNewLayout, userId, tenantId);
                    }

                    var defaultEditLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.Edit && z.EntityId == id.EntityContext);
                    if (defaultEditLayout == null)
                    {
                        info.ErrorLevel = 2;
                        info.Message = $"No default edit layout found for the entity context {id}";
                        responses.Info.Add(info);
                    }
                    else
                    {
                        layout.CreatePicklistLayout(defaultEditLayout, userId, tenantId);
                    }
                }
            }
        }

        private void InitilizeTenantWorkflow(Guid rootTenantId, Guid newTenantId, List<string> entityIds)
        {
            _managerWorkFlow.InitializeTenantWorkFlow(rootTenantId, newTenantId, entityIds);
        }

        internal void InitilizeResources(Guid rootTenantCode, Guid currentTenantCode)
        {
            IResourceManager manager = new ResourceManager.Contracts.ResourceManager();
            manager.CopyResources(rootTenantCode, currentTenantCode);
        }

        internal void InitilizeResources(Guid rootTenantCode, Guid currentTenantCode, List<TypeInfo> metadataClasses)
        {
            try
            {
                var resources = new List<Entities.EntityCore.Model.Resource.Resource>();
                IMetadataManager _iMetadataManager = new MetadataManager.Contracts.MetadataManager();
                IResourceManager manager = new ResourceManager.Contracts.ResourceManager();
                #region StaticResource
                resources.AddRange(GetResouceFromJsonFile());
                #endregion
                #region MenuResource
                resources.AddRange(InitializeResourceForMenu(rootTenantCode,manager));
                #endregion

                #region Add all resources for all entities and picklists

                foreach (var item in metadataClasses)
                {
                    var entityCode = string.Empty;
                    if (item.BaseType.BaseType == typeof(PicklistBase))
                    {
                        entityCode = _iMetadataManager.GetEntityContextByEntityName(item.Name, true);
                    }
                    else
                    {
                        entityCode = _iMetadataManager.GetEntityContextByEntityName(item.Name);
                    }

                    var entity = _iMetadataManager.GetEntitityByName(item.Name);

                    if (entity != null)
                    {
                        var entityName = entity.Name.Trim().ToLower();

                        InitializeResourceForEntityRowLevelOperation(entity, entityCode, entityName, resources);

                        InitializeResourceForDisplayName(entity, entityCode, entityName, resources);

                        InitializeResourceForPluralName(entity, entityCode, entityName, resources);

                        InitializeResourceForSubType(entity, entityCode, entityName, resources);

                        InitializeResourceForRelatedEntity(entity, entityCode, entityName, resources);

                        InitializeResourceForEntityRelation(entity, entityCode, entityName, resources);

                        InitializeResourceForEntityWorkFlow(rootTenantCode, entity, entityCode, entityName, resources);

                        InitializeResourceForWorkflow(resources, entity);

                        #region Entity Fields, Validation and Picklist

                        if (entity.Fields != null && entity.Fields.Count > 0)
                        {
                            foreach (var field in entity.Fields)
                            {
                                if (field.DataType.ToLower() == "picklist" && !String.IsNullOrEmpty(field.TypeOf))
                                {
                                    InitializeResourceForPicklist(field, _iMetadataManager, entity, entityCode, resources);
                                }

                                InitializeResourceForEntityFields(field, entity, entityCode, entityName, resources);

                            }
                        }

                        //for intersect entity
                        if (entity.DetailEntities != null && entity.DetailEntities.Count > 0)
                        {
                            foreach (var intersectfield in entity.DetailEntities)
                            {
                                InitializeResourceForIntersectEntity(intersectfield, entity, entityCode, entityName, resources);
                            }
                        }

                        #endregion

                        InitializeResourceForEntityTypeAndRelatedField(entity, entityCode, entityName, resources);

                    }

                }

                InitializeResourceForErrorCodes(resources);

                // InitializeResourceForCommonTexts(resources);
                #endregion

                bool isResourceCreated = manager.CreateResources(rootTenantCode, currentTenantCode, resources);
            }
            catch (System.NullReferenceException nullEx)
            {
                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }


        private void InitializeResourceForEntityRowLevelOperation(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            #region Entity RowLevelOperations

            if (entity.RowLevelOperations != null && entity.RowLevelOperations.Count > 0)
            {
                foreach (var rowLevelOperation in entity.RowLevelOperations)
                {
                    var rowLevelOperationResource = new Entities.EntityCore.Model.Resource.Resource();
                    rowLevelOperationResource.Key = String.Format("{0}_{1}_{2}", entityName, "rowleveloperation", rowLevelOperation.Name.Replace(" ", "").Trim().ToLower());
                    rowLevelOperationResource.Value = rowLevelOperation.Name.Trim();
                    rowLevelOperationResource.EntityCode = entityCode;
                    rowLevelOperationResource.IsStatic = false;
                     
                    if (!IsExistingResourceKey(resources, rowLevelOperationResource.Key))
                        resources.Add(rowLevelOperationResource);
                }
            }

            #endregion
        }

        private void InitializeResourceForPluralName(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            #region Entity PluralName

            if (!String.IsNullOrEmpty(entity.PluralName))
            {
                var pluralNameResource = new Entities.EntityCore.Model.Resource.Resource();
                pluralNameResource.Key = String.Format("{0}_{1}", entityName, "pluralname");
                pluralNameResource.Value = entity.PluralName.Trim();
                pluralNameResource.EntityCode = entityCode;
                pluralNameResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, pluralNameResource.Key))
                    resources.Add(pluralNameResource);

            }

            #endregion
        }

        private void InitializeResourceForDisplayName(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            #region Entity DisplayName

            if (!String.IsNullOrEmpty(entity.DisplayName))
            {
                var displayNameResource = new Entities.EntityCore.Model.Resource.Resource();
                displayNameResource.Key = String.Format("{0}_{1}", entityName, "displayname");
                displayNameResource.Value = entity.DisplayName.Trim();
                displayNameResource.EntityCode = entityCode;
                displayNameResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, displayNameResource.Key))
                    resources.Add(displayNameResource);
            }

            #endregion
        }

        

        private void InitializeResourceForSubType(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            #region Entity Subtypes

            if (entity.Subtypes != null && entity.Subtypes.Count > 0)
            {
                foreach (var subtype in entity.Subtypes)
                {
                    var subtypeResource = new Entities.EntityCore.Model.Resource.Resource();
                    subtypeResource.Key = String.Format("{0}_{1}_{2}", entityName, "subtype", subtype.Replace(" ", "").Trim().ToLower());
                    subtypeResource.Value = subtype.Trim();
                    subtypeResource.EntityCode = entityCode;
                    subtypeResource.IsStatic = false;
                     
                    if (!IsExistingResourceKey(resources, subtypeResource.Key))
                        resources.Add(subtypeResource);
                }
            }

            #endregion
        }

        private void InitializeResourceForRelatedEntity(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region Entity Related Entities

                if (entity.RelatedEntities != null && entity.RelatedEntities.Count > 0)
                {
                    foreach (var relatedEntity in entity.RelatedEntities)
                    {
                        var relatedEntityResource = new Entities.EntityCore.Model.Resource.Resource();
                        relatedEntityResource.Key = String.Format("{0}_{1}_{2}", entityName, "relatedentity", relatedEntity.Name.Replace(" ", "").Trim().ToLower());
                        relatedEntityResource.Value = relatedEntity.Name.Trim();
                        relatedEntityResource.EntityCode = entityCode;
                        relatedEntityResource.IsStatic = false;
                         
                        if (!IsExistingResourceKey(resources, relatedEntityResource.Key))
                            resources.Add(relatedEntityResource);
                    }
                }

                #endregion
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        private void InitializeResourceForEntityRelation(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region Entity Relations

                if (entity.Relations != null && entity.Relations.Count > 0)
                {
                    foreach (var relation in entity.Relations)
                    {
                        var relationResource = new Entities.EntityCore.Model.Resource.Resource();
                        relationResource.Key = String.Format("{0}_{1}_{2}", entityName, "relation", relation.Name.Replace(" ", "").Trim().ToLower());
                        relationResource.Value = relation.Name.Trim();
                        relationResource.EntityCode = entityCode;
                        relationResource.IsStatic = false;
                         
                        if (!IsExistingResourceKey(resources, relationResource.Key))
                            resources.Add(relationResource);
                    }
                }

                #endregion
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        private void InitializeResourceForEntityWorkFlow(Guid rootTenantCode, Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region Entity Workflow

                if (entity.SupportWorkflow)
                {
                    IManagerWorkFlow _managerWorkFlow = new ManagerWorkFlow();
                    var workFlowItems = _managerWorkFlow.GetWorkFlows(rootTenantCode, entityName);

                    if (workFlowItems != null && workFlowItems.Count > 0)
                    {
                        foreach (var workFlow in workFlowItems)
                        {
                            if (workFlow.Operations != null && workFlow.Operations.Count > 0)
                            {
                                foreach (var wrkFlowOperation in workFlow.Operations)
                                {
                                    if (wrkFlowOperation.WorkFlowProcess != null && wrkFlowOperation.WorkFlowProcess.Count > 0)
                                    {
                                        foreach (var wrkFlwProces in wrkFlowOperation.WorkFlowProcess)
                                        {
                                            if (wrkFlwProces.WorkFlowProcessTasks != null && wrkFlwProces.WorkFlowProcessTasks.Count > 0)
                                            {
                                                foreach (var wrkFlowTask in wrkFlwProces.WorkFlowProcessTasks)
                                                {
                                                    var wrkFlowTaskResource = new Entities.EntityCore.Model.Resource.Resource();
                                                    wrkFlowTaskResource.Key = String.Format("{0}_{1}_{2}", entityName, "workflow_process", wrkFlowTask.ProcessName.Replace(" ", "").Trim().ToLower());
                                                    wrkFlowTaskResource.Value = wrkFlowTask.ProcessName;
                                                    wrkFlowTaskResource.EntityCode = entityCode;
                                                    wrkFlowTaskResource.IsStatic = false;
                                                     
                                                    if (!IsExistingResourceKey(resources, wrkFlowTaskResource.Key))
                                                        resources.Add(wrkFlowTaskResource);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (workFlow.Steps != null && workFlow.Steps.Count > 0)
                            {
                                foreach (var wrkFlwStep in workFlow.Steps)
                                {
                                    if (wrkFlwStep.Roles != null && wrkFlwStep.Roles.Count > 0)
                                    {
                                        foreach (var wrkFlwRole in wrkFlwStep.Roles)
                                        {
                                            var wrkFlowRoleResource = new Entities.EntityCore.Model.Resource.Resource();
                                            wrkFlowRoleResource.Key = String.Format("{0}_{1}_{2}", entityName, "workflow_role", wrkFlwRole.RoleName.Replace(" ", "").Trim().ToLower());
                                            wrkFlowRoleResource.Value = wrkFlwRole.RoleName;
                                            wrkFlowRoleResource.EntityCode = entityCode;
                                            wrkFlowRoleResource.IsStatic = false;
                                             
                                            if (!IsExistingResourceKey(resources, wrkFlowRoleResource.Key))
                                                resources.Add(wrkFlowRoleResource);
                                        }
                                    }
                                }
                            }

                            if (workFlow.SubTypeCode != null)
                            {
                                var wrkFlowSubTypeCodeResource = new Entities.EntityCore.Model.Resource.Resource();
                                wrkFlowSubTypeCodeResource.Key = String.Format("{0}_{1}_{2}", entityName, "workflow", workFlow.SubTypeCode.Replace(" ", "").Trim().ToLower());
                                wrkFlowSubTypeCodeResource.Value = workFlow.SubTypeCode;
                                wrkFlowSubTypeCodeResource.EntityCode = entityCode;
                                wrkFlowSubTypeCodeResource.IsStatic = false;
                                 
                                if (!IsExistingResourceKey(resources, wrkFlowSubTypeCodeResource.Key))
                                    resources.Add(wrkFlowSubTypeCodeResource);
                            }
                        }
                    }
                }

                #endregion
            }
            catch (System.NullReferenceException nullex)
            {
                throw nullex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void InitializeResourceForPicklist(FieldModel field, IMetadataManager _iMetadataManager, Entity entity, string entityCode, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region Picklist Resource Add

                if (field.DataType.ToLower() == "picklist" && !String.IsNullOrEmpty(field.TypeOf))
                {
                    var pickList = _iMetadataManager.GetEntitityByName(field.TypeOf);

                    if (pickList != null)
                    {
                        try
                        {
                            var picklistName = pickList.Name.ToLower();

                            #region Picklist RowLevelOperations
                            if (pickList.RowLevelOperations != null && pickList.RowLevelOperations.Count > 0)
                            {
                                foreach (var rowLevelOperationPL in pickList.RowLevelOperations)
                                {
                                    var rowLevelOperationPLResource = new Entities.EntityCore.Model.Resource.Resource();
                                    rowLevelOperationPLResource.Key = String.Format("{0}_{1}_{2}", picklistName, "rowleveloperation", rowLevelOperationPL.Name.Replace(" ", "").Trim().ToLower());
                                    rowLevelOperationPLResource.Value = rowLevelOperationPL.Name.Trim();
                                    rowLevelOperationPLResource.EntityCode = entityCode;
                                    rowLevelOperationPLResource.IsStatic = false;

                                    if (!IsExistingResourceKey(resources, rowLevelOperationPLResource.Key))
                                        resources.Add(rowLevelOperationPLResource);
                                }
                            }
                            #endregion

                            #region Picklist DisplayName
                            if (!String.IsNullOrEmpty(pickList.DisplayName))
                            {
                                var displayNamePLResource = new Entities.EntityCore.Model.Resource.Resource();
                                displayNamePLResource.Key = String.Format("{0}_{1}", picklistName, "displayname");
                                displayNamePLResource.Value = pickList.DisplayName.Trim();
                                displayNamePLResource.EntityCode = entityCode;
                                displayNamePLResource.IsStatic = false;

                                if (!IsExistingResourceKey(resources, displayNamePLResource.Key))
                                    resources.Add(displayNamePLResource);
                            }
                            #endregion

                            #region Picklist PluralName
                            if (!String.IsNullOrEmpty(pickList.PluralName))
                            {
                                var pluralNamePLResource = new Entities.EntityCore.Model.Resource.Resource();
                                pluralNamePLResource.Key = String.Format("{0}_{1}", picklistName, "pluralname");
                                pluralNamePLResource.Value = pickList.PluralName.Trim();
                                pluralNamePLResource.EntityCode = entityCode;
                                pluralNamePLResource.IsStatic = false;

                                if (!IsExistingResourceKey(resources, pluralNamePLResource.Key))
                                    resources.Add(pluralNamePLResource);

                            }
                            #endregion

                            #region Picklist Subtypes
                            if (pickList.Subtypes != null && pickList.Subtypes.Count > 0)
                            {
                                foreach (var subtype in pickList.Subtypes)
                                {
                                    var subtypePLResource = new Entities.EntityCore.Model.Resource.Resource();
                                    subtypePLResource.Key = String.Format("{0}_{1}_{2}", picklistName, "subtype", subtype.Replace(" ", "").Trim().ToLower());
                                    subtypePLResource.Value = subtype.Trim();
                                    subtypePLResource.EntityCode = entityCode;
                                    subtypePLResource.IsStatic = false;
                                    if (!IsExistingResourceKey(resources, subtypePLResource.Key))
                                        resources.Add(subtypePLResource);
                                }
                            }
                            #endregion

                            #region Picklist Fields

                            if (pickList.Fields != null && pickList.Fields.Count > 0)
                            {
                                InitializeResourceForPicklistFields(pickList.Fields, entityCode, picklistName, pickList, resources);
                            }

                            #endregion
                        }
                        catch (System.NullReferenceException nullEx)
                        {
                            throw nullEx;
                        }
                        catch (System.ArgumentException arg)
                        {
                            throw arg;
                        }
                        catch (System.Exception ex)
                        {

                            throw ex;
                        }
                    }

                }

                #endregion
            }
            catch (System.NullReferenceException nullEx)
            {

                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        private void InitializeResourceForPicklistFields(List<FieldModel> picklistFields, string entityCode, string picklistName, Entity pickList, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region PickListFields

                foreach (var picklistField in picklistFields)
                {
                    var PLfieldResource = new Entities.EntityCore.Model.Resource.Resource();
                    string PLfieldName = string.Empty;
                    string displayName = string.Empty;

                    if (!String.IsNullOrEmpty(picklistField.DisplayName))
                    {
                        displayName = picklistField.DisplayName.Contains('.') ? picklistField.DisplayName.Replace('.', '_') : picklistField.DisplayName;
                        if (picklistField.Name.Contains('.'))
                        {
                            PLfieldName = picklistField.Name.Replace('.', '_');
                        }
                        else
                        {
                            PLfieldName = picklistField.Name;
                        }

                    }
                    else
                    {
                        if (picklistField.Name.Contains('.'))
                        {
                            PLfieldName = picklistField.Name.Replace('.', '_');
                            displayName = picklistField.Name;
                        }
                        else
                        {
                            PLfieldName = picklistField.Name;
                            displayName = picklistField.Name;
                        }
                    }

                    PLfieldResource.Key = String.Format("{0}_{1}_{2}", picklistName, "field", PLfieldName.Replace(" ", "").Trim().ToLower());
                    PLfieldResource.Value = displayName;
                    PLfieldResource.EntityCode = entityCode;
                    PLfieldResource.IsStatic = false;
                     
                    if (!IsExistingResourceKey(resources, PLfieldResource.Key))
                        resources.Add(PLfieldResource);

                }

                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        private string GetWorkFlowModel(PropertyInfo data)
        {
            var workflowmodel = ((WorkFlowModelAttribute[])data.GetCustomAttributes(typeof(WorkFlowModelAttribute), false));
            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            var entityName = _iMetadataManager.GetEntityNameByEntityContext(workflowmodel[0].Context);
            return entityName;
        }
        private void InitializeResourceForWorkflow(List<Entities.EntityCore.Model.Resource.Resource> resources, Entity entity)
        {
            #region Workflow resorce initialization
            try
            {
                Type myType = (typeof(WorkFlowEngine));
                var workflowlist = myType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(WorkFlowModelAttribute)));
                var selectedworkflowmodel = workflowlist.Where(x => GetWorkFlowModel(x) == entity.Name);
                if (selectedworkflowmodel.Count() > 0)
                {
                    foreach (var prop in selectedworkflowmodel)
                    {
                        var workflowmodel = ((WorkFlowModelAttribute[])prop.GetCustomAttributes(typeof(WorkFlowModelAttribute), false));
                        if (workflowmodel.Any())
                        {
                            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
                            var workflowtransitionresource = new Entities.EntityCore.Model.Resource.Resource();
                            workflowtransitionresource.Key = String.Format("{0}_workflow_step_{1}", entity.Name.ToLower().Trim(), workflowmodel[0].Key.ToLower().Trim());
                            workflowtransitionresource.Value = workflowmodel[0].TransitionResourceValue.Trim();
                            workflowtransitionresource.EntityCode = workflowmodel[0].Context;
                            workflowtransitionresource.IsStatic = false;
                             
                            if (!IsExistingResourceKey(resources, workflowtransitionresource.Key))
                                resources.Add(workflowtransitionresource);

                            var workflowStatusResource = new Entities.EntityCore.Model.Resource.Resource();
                            workflowStatusResource.Key = String.Format("{0}_workflow_tran_{1}", entity.Name.ToLower().Trim(), workflowmodel[0].Key.ToLower().Trim());
                            workflowStatusResource.Value = workflowmodel[0].StatusResourceValue.Trim();
                            workflowStatusResource.EntityCode = workflowmodel[0].Context;
                            workflowStatusResource.IsStatic = false;
                             
                            if (!IsExistingResourceKey(resources, workflowStatusResource.Key))
                                resources.Add(workflowStatusResource);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            #endregion

        }
        private void InitializeResourceForEntityFields(FieldModel field, Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                var fieldResource = new Entities.EntityCore.Model.Resource.Resource();
                string fieldName = string.Empty;
                dynamic displayNameType = field;
                string displayName = string.Empty;

                if (!String.IsNullOrEmpty(field.DisplayName))
                {
                    displayName = field.DisplayName.Contains('.') ? field.DisplayName.Replace('.', '_') : field.DisplayName;
                    if (field.Name.Contains('.'))
                    {
                        fieldName = field.Name.Replace('.', '_');
                    }
                    else
                    {
                        fieldName = field.Name;

                    }

                }
                else
                {

                    if (field.Name.Contains('.'))
                    {
                        fieldName = field.Name.Replace('.', '_');
                        displayName = field.Name;
                    }
                    else
                    {
                        fieldName = field.Name;
                        displayName = field.Name;
                    }
                }

                fieldResource.Key = String.Format("{0}_{1}_{2}", entityName, "field", fieldName.Replace(" ", "").Trim().ToLower());
                fieldResource.Value = displayName.Contains('_') ? displayName.Split('_')[displayName.Split('_').Length - 1] : displayName;
                fieldResource.EntityCode = entityCode;
                fieldResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, fieldResource.Key))
                    resources.Add(fieldResource);

                #region Field Validation Resources Add

                string validationKeyText = "Validation";
                string fieldNameWithEntity = entity.Name.Replace(" ", "").Trim().ToLower() + "_" + fieldName.Replace(" ", "").Trim().ToLower();

                //if (field.Required) {
                var mandatoryResource = new Entities.EntityCore.Model.Resource.Resource();
                mandatoryResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "required", fieldNameWithEntity.ToLower());
                mandatoryResource.Value = String.Format("{0} is mandatory", displayName.Trim());
                mandatoryResource.EntityCode = entityCode;
                mandatoryResource.IsStatic = false;
                //Added by Soma
                if (!IsExistingResourceKey(resources, mandatoryResource.Key))
                    resources.Add(mandatoryResource);

                //}

                #region Field Validation

                if (field.Validators != null && field.Validators.Count > 0)
                {

                    foreach (var validator in field.Validators)
                    {
                        if (validator.Dblength != null && validator.Dblength > 0)
                        {
                            int minLength = 0;

                            var maxlengthValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                            maxlengthValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "maxlength", fieldNameWithEntity.ToLower());
                            maxlengthValidationResource.Value = String.Format("{0} accepts max {1} characters", displayName.Trim(), validator.Dblength.ToString().Trim());
                            maxlengthValidationResource.EntityCode = entityCode;
                            maxlengthValidationResource.IsStatic = false;
                            //Added by Soma
                            if (!IsExistingResourceKey(resources, maxlengthValidationResource.Key))
                                resources.Add(maxlengthValidationResource);

                            var rangeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                            rangeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "range", fieldNameWithEntity.ToLower());
                            rangeValidationResource.Value = String.Format("{0} should be in between {1} and {2}", displayName.Trim(), Convert.ToString(minLength), validator.Dblength.ToString().Trim());
                            rangeValidationResource.EntityCode = entityCode;
                            rangeValidationResource.IsStatic = false;
                            //Added by Soma
                            if (!IsExistingResourceKey(resources, rangeValidationResource.Key))
                                resources.Add(rangeValidationResource);

                            if (validator.Name == "LengthValidator")
                            {

                                var lengthValidatorResource = new Entities.EntityCore.Model.Resource.Resource();
                                lengthValidatorResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", validator.Name.ToLower(), fieldNameWithEntity.ToLower());
                                lengthValidatorResource.Value = String.Format("{0} has to be less than {1} characters", displayName.Trim(), validator.Dblength.ToString().Trim());
                                lengthValidatorResource.EntityCode = entityCode;
                                lengthValidatorResource.IsStatic = false;
                                //Added by Soma
                                if (!IsExistingResourceKey(resources, lengthValidatorResource.Key))
                                    resources.Add(lengthValidatorResource);
                                
                                // Added by Soma
                                var minlengthValidatorResource = new Entities.EntityCore.Model.Resource.Resource();
                                minlengthValidatorResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", validator.Name.ToLower(), fieldNameWithEntity.ToLower());
                                minlengthValidatorResource.Value = String.Format("{0} has to be more than {1} characters", displayName.Trim(), validator.MinDblength.ToString().Trim());
                                minlengthValidatorResource.EntityCode = entityCode;
                                minlengthValidatorResource.IsStatic = false;
                                //Added by Soma
                                if (!IsExistingResourceKey(resources, minlengthValidatorResource.Key))
                                    resources.Add(minlengthValidatorResource);

                            }
                        }

                        if (validator.Pattern != null)
                        {
                            var formatValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                            formatValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "format", fieldNameWithEntity.ToLower());
                            formatValidationResource.Value = String.Format("{0} accepts 'abc@example.com' format", displayName.Trim(), validator.Pattern.Trim());
                            formatValidationResource.EntityCode = entityCode;
                            formatValidationResource.IsStatic = false;
                            //Added by Soma
                            if (!IsExistingResourceKey(resources, formatValidationResource.Key))
                                resources.Add(formatValidationResource);
                        }

                        if (validator.Name == "RegularExpression")
                        {

                            var regularExpValResource = new Entities.EntityCore.Model.Resource.Resource();
                            regularExpValResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", validator.Name.ToLower(), fieldNameWithEntity.ToLower());
                            regularExpValResource.Value = String.Format("{0} is not valid", displayName.Trim());
                            regularExpValResource.EntityCode = entityCode;
                            regularExpValResource.IsStatic = false;
                            //Added by Soma
                            if (!IsExistingResourceKey(resources, regularExpValResource.Key))
                                resources.Add(regularExpValResource);
                        }

                        if (validator.Name == "PercentValidator")
                        {

                            var percentValResource = new Entities.EntityCore.Model.Resource.Resource();
                            percentValResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", validator.Name.ToLower(), fieldNameWithEntity.ToLower());
                            percentValResource.Value = String.Format("{0} is not valid", displayName.Trim());
                            percentValResource.EntityCode = entityCode;
                            percentValResource.IsStatic = false;
                            //Added by Soma
                            if (!IsExistingResourceKey(resources, percentValResource.Key))
                                resources.Add(percentValResource);
                        }
                    }
                }

                #endregion

                #region Field DataType

                if (field.DataType != null)
                {

                    #region DataType Resource Add

                    foreach (VPC.Metadata.Business.DataAnnotations.DataType dataType in Enum.GetValues(typeof(VPC.Metadata.Business.DataAnnotations.DataType)))
                    {

                        int dataTypeVal = (int)dataType;
                        string dataTypeDesc = dataType.ToString();

                        if (dataTypeVal > 0 && dataTypeDesc != null)
                        {

                            if (dataTypeDesc.ToLower() == "number" && field.DataType.ToLower() == "number")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "number", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only number", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                //Added by Soma
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key))
                                    resources.Add(dbTypeValidationResource);

                            }
                            if (dataTypeDesc.ToLower() == "text" && field.DataType.ToLower() == "text")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "text", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only text", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                //Added by Soma
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key))
                                    resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "bool" && field.DataType.ToLower() == "bool")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "boolean", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only boolean", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key))
                                    resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "date" && field.DataType.ToLower() == "date")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "date", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only date", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key))
                                    resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "datetime" && field.DataType.ToLower() == "datetime")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "datetime", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only datetime", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "guid" && field.DataType.ToLower() == "guid")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "guid", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only guid", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "picklist" && field.DataType.ToLower() == "picklist")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "picklist", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only picklist", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "complex" && field.DataType.ToLower() == "complex")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "complex", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only complex type", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "email" && field.DataType.ToLower() == "email")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "email", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only email", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "byte" && field.DataType.ToLower() == "byte")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "byte", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only byte", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }

                            if (dataTypeDesc.ToLower() == "password" && field.DataType.ToLower() == "password")
                            {
                                var dbTypeValidationResource = new Entities.EntityCore.Model.Resource.Resource();
                                dbTypeValidationResource.Key = String.Format("{0}_{1}_{2}_{3}", validationKeyText.ToLower(), "field", "password", fieldNameWithEntity.ToLower());
                                dbTypeValidationResource.Value = String.Format("{0} is not valid as it accepts only password", displayName.Trim());
                                dbTypeValidationResource.EntityCode = entityCode;
                                dbTypeValidationResource.IsStatic = false;
                                if (!IsExistingResourceKey(resources, dbTypeValidationResource.Key)) resources.Add(dbTypeValidationResource);
                            }
                        }

                    }

                    #endregion

                }

                #endregion

                #endregion

                // if (field.DataType != null)
                // {
                //     var fieldDataTypeResource = new Entities.EntityCore.Model.Resource.Resource();
                //     fieldDataTypeResource.Key = String.Format("{0}_{1}_{2}_{3}", entityName, "field", "datatype", field.DataType.Replace(" ", "").Trim().ToLower());
                //     fieldDataTypeResource.Value = field.DataType;
                //     fieldDataTypeResource.EntityCode = entityCode;
                //     fieldDataTypeResource.IsStatic = false;
                //     //Added by Soma
                //     if (!IsExistingResourceKey(resources, fieldDataTypeResource.Key))
                //         resources.Add(fieldDataTypeResource);
                // }

                // if (field.ControlType != null)
                // {
                //     var fieldControlTypeResource = new Entities.EntityCore.Model.Resource.Resource();
                //     fieldControlTypeResource.Key = String.Format("{0}_{1}_{2}_{3}", entityName, "field", "controltype", field.ControlType.Replace(" ", "").Trim().ToLower());
                //     fieldControlTypeResource.Value = field.ControlType;
                //     fieldControlTypeResource.EntityCode = entityCode;
                //     fieldControlTypeResource.IsStatic = false;
                //     //Added by Soma
                //     if (!IsExistingResourceKey(resources, fieldControlTypeResource.Key))
                //         resources.Add(fieldControlTypeResource);
                // }
                //}
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void InitializeResourceForIntersectEntity(Entity intersectField, Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                var fieldResource = new Entities.EntityCore.Model.Resource.Resource();
                string fieldName = string.Empty;
                string displayName = string.Empty;

                if (!String.IsNullOrEmpty(intersectField.DisplayName))
                {
                    displayName = intersectField.DisplayName.Contains('.') ? intersectField.DisplayName.Replace('.', '_') : intersectField.DisplayName;
                    if (intersectField.Name.Contains('.'))
                    {
                        fieldName = intersectField.Name.Replace('.', '_');
                    }
                    else
                    {
                        fieldName = intersectField.Name;

                    }

                }
                else
                {

                    if (intersectField.Name.Contains('.'))
                    {
                        fieldName = intersectField.Name.Replace('.', '_');
                        displayName = intersectField.Name;
                    }
                    else
                    {
                        fieldName = intersectField.Name;
                        displayName = intersectField.Name;
                    }
                }

                fieldResource.Key = String.Format("{0}_{1}_{2}", entityName, "field", fieldName.Replace(" ", "").Trim().ToLower());
                fieldResource.Value = displayName.Contains('_') ? displayName.Split('_')[displayName.Split('_').Length - 1] : displayName;
                fieldResource.EntityCode = entityCode;
                fieldResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, fieldResource.Key))
                    resources.Add(fieldResource);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        private void InitializeResourceForEntityTypeAndRelatedField(Entity entity, string entityCode, string entityName, List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            if (entity.RelatedField != null)
            {
                var RelatedFieldResource = new Entities.EntityCore.Model.Resource.Resource();
                RelatedFieldResource.Key = String.Format("{0}_{1}_{2}_{3}", entityName, "field", "relatedfield", entity.RelatedField.Replace(" ", "").Trim().ToLower());
                RelatedFieldResource.Value = entity.RelatedField;
                RelatedFieldResource.EntityCode = entityCode;
                RelatedFieldResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, RelatedFieldResource.Key))
                    resources.Add(RelatedFieldResource);
            }

            if (entity.Type != null)
            {
                var TypeResource = new Entities.EntityCore.Model.Resource.Resource();
                TypeResource.Key = String.Format("{0}_{1}_{2}_{3}", entityName, "field", "type", entity.Type.Replace(" ", "").Trim().ToLower());
                TypeResource.Value = entity.Type;
                TypeResource.EntityCode = entityCode;
                TypeResource.IsStatic = false;
                 
                if (!IsExistingResourceKey(resources, TypeResource.Key))
                    resources.Add(TypeResource);
            }
        }

        private void InitializeResourceForErrorCodes(List<Entities.EntityCore.Model.Resource.Resource> resources)
        {
            try
            {
                #region Error codes

                foreach (ErrorCodeEnum errCode in Enum.GetValues(typeof(ErrorCodeEnum)))
                {

                    int errcodeVal = (int)errCode;
                    string errcodeDesc = errCode.GetDescription<ErrorCodeEnum>();

                    if (errcodeVal >= 0 && errcodeDesc != null)
                    {
                        var errCodeResource = new Entities.EntityCore.Model.Resource.Resource();
                        errCodeResource.Key = String.Format("{0}_{1}_{2}", "error", "code", Convert.ToString(errcodeVal));
                        errCodeResource.Value = errcodeDesc;
                        errCodeResource.IsStatic = true;
                         
                        if (!IsExistingResourceKey(resources, errCodeResource.Key))
                            resources.Add(errCodeResource);
                    }

                }

                #endregion
            }
            catch (System.NullReferenceException nullEx)
            {

                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        private List<Entities.EntityCore.Model.Resource.Resource> InitializeResourceForMenu(Guid initilizedTenantCode,IResourceManager _resourceManager)
        {

            try
            {
                List<Entities.EntityCore.Model.Resource.Resource> menuResources = new List<Entities.EntityCore.Model.Resource.Resource>();
                //get language key value
                // string langKey = "";
                // string langValue = "";
                // var retLan = _resourceManager.GetDefaultLanguageByTenant(initilizedTenantCode);
                // if (retLan != null && retLan.Key != null)
                // {
                //     langKey = Convert.ToString(retLan.Key);
                // }
                // if (retLan != null && retLan.Text != null)
                // {
                //     langValue = Convert.ToString(retLan.Text);
                // }

                List<MenuItem> allmenus = _IMenuManager.GetMenuBytenant(initilizedTenantCode);

                if (allmenus != null && allmenus.Count > 0)
                {
                    foreach (var item in allmenus)
                    {
                        if (item.Menucode != null && item.Menucode != "")
                        {
                            var retRes = _resourceManager.GetResourcesByKey(initilizedTenantCode, item.Menucode);
                            if (retRes.Count == 0)
                            {
                                var menuResource = new Entities.EntityCore.Model.Resource.Resource();
                                menuResource.Key = item.Menucode;
                                menuResource.Value = item.Name;
                                menuResource.EntityCode = null;
                                menuResource.IsStatic = false;
                                menuResources.Add(menuResource);
                                // string msg = "";
                                // _resourceManager.Create(initilizedTenantCode, new Entities.EntityCore.Model.Resource.Resource(item.Menucode, item.Name, langKey, langValue, "", false), UserId, ref msg);
                            }
                        }

                    }
                }

                return menuResources;
            }
           catch (System.NullReferenceException nullEx)
            {
                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        private Guid InitilizeTenantSubscription(Guid rootTenantCode, Guid initilizedTenantCode, Guid subscriptionId)
        {
            IManagerTenantSubscription _managerSubscription = new ManagerTenantSubscription();
            IManagerTenantSubscriptionEntity _managerSubscriptionEntity = new ManagerTenantSubscriptionEntity();
            IManagerTenantSubscriptionEntityDetail _managerSubscriptionEntityDetail = new ManagerTenantSubscriptionEntityDetail();
            //GET  SUBSCRIPTION FROM MASTER
            var subscription = _managerSubscription.TenantSubscription(rootTenantCode, subscriptionId);
            if (subscription != null)
            {
                //INSERT SUBSCRIPTION
                var newSubscriptionId = _managerSubscription.Create(initilizedTenantCode, subscription);
                //GET ALL ENTITIES
                var subscriptionEntities = _managerSubscriptionEntity.TenantSubscriptionEntities(rootTenantCode, subscriptionId);
                List<TenantSubscriptionEntityInfo> newSubscriptionEntities = new List<TenantSubscriptionEntityInfo>();
                List<TenantSubscriptionEntityDetailInfo> newEntityDetails = new List<TenantSubscriptionEntityDetailInfo>();

                foreach (var subscriptionEntity in subscriptionEntities)
                {
                    //GET ALL ENTITY DEAIL
                    var subscriptionEntityDetails = _managerSubscriptionEntityDetail.TenantSubscriptionEntityDetails(rootTenantCode, subscriptionEntity.TenantSubscriptionEntityId);

                    subscriptionEntity.TenantSubscriptionId = newSubscriptionId;
                    subscriptionEntity.TenantSubscriptionEntityId = Guid.NewGuid();
                    newSubscriptionEntities.Add(subscriptionEntity);

                    foreach (var detail in subscriptionEntityDetails)
                    {
                        detail.SubscriptionEntityDetailId = Guid.NewGuid();
                        detail.SubscriptionEntityId = subscriptionEntity.TenantSubscriptionEntityId;
                        newEntityDetails.Add(detail);
                    }
                }
                _managerSubscriptionEntity.Create(initilizedTenantCode, newSubscriptionEntities);
                //Create subscription entities detail
                _managerSubscriptionEntityDetail.Create(initilizedTenantCode, newEntityDetails);
            }

            return subscription.TenantSubscriptionId;

        }

        private void InitializePicklistValue(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode, Guid userId)
        {
            IInitilizeAdmin _InitilizeAdmin = new InitilizeAdmin();
            _InitilizeAdmin.InitializePicklistValue(picklists, rootTenantCode, initilizedTenantCode, userId);
        }

        private void InitializeMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            IInitilizeAdmin _InitilizeAdmin = new InitilizeAdmin();
            _InitilizeAdmin.InitializeMenu(rootTenantCode, initilizedTenantCode);
        }

        bool IInitilizeManager.InitializeRootTenantWorkFlow(Guid tenantId)
        {
            return _managerWorkFlow.InitializeRootTenantWorkFlow(tenantId);
        }

        bool IInitilizeManager.InitializeRootTenantWorkFlow(Guid tenantId, string entityId)
        {
            return _managerWorkFlow.InitializeRootTenantWorkFlow(tenantId, entityId);
        }

        private void InitializeParentMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            _IMenuManager.InitilizeParentMenu(rootTenantCode, initilizedTenantCode);
        }

        public void Test(Guid root, Guid newTen)
        {
            var replacer = new DefaultValueReplaceHelper();
            replacer.Execute(root, newTen);
        }
        private void ReplaceDefaultValues(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            var replacer = new DefaultValueReplaceHelper();
            replacer.Execute(rootTenantCode, initilizedTenantCode);
        }

        internal List<Entities.EntityCore.Model.Resource.Resource> InitialiseForRepairResources(Guid rootTenantCode, Guid currentTenantCode, List<TypeInfo> metadataClasses)
        {
            try
            {
                var resourcesCurrentTenant = new List<Entities.EntityCore.Model.Resource.Resource>();
                var resources = new List<Entities.EntityCore.Model.Resource.Resource>();
                IMetadataManager _iMetadataManager = new MetadataManager.Contracts.MetadataManager();
                IResourceManager manager = new ResourceManager.Contracts.ResourceManager();
                #region MenuResource
                resources.AddRange(InitializeResourceForMenu(rootTenantCode,manager));
                #endregion
                #region Add all resources

                foreach (var item in metadataClasses)
                {
                    var entityCode = string.Empty;
                    if (item.BaseType.BaseType == typeof(PicklistBase))
                    {
                        entityCode = _iMetadataManager.GetEntityContextByEntityName(item.Name, true);
                    }
                    else
                    {
                        entityCode = _iMetadataManager.GetEntityContextByEntityName(item.Name);
                    }

                    var entity = _iMetadataManager.GetEntitityByName(item.Name);

                    if (entity != null)
                    {
                        var entityName = entity.Name.Trim().ToLower();

                        InitializeResourceForEntityRowLevelOperation(entity, entityCode, entityName, resources);

                        InitializeResourceForDisplayName(entity, entityCode, entityName, resources);

                        InitializeResourceForPluralName(entity, entityCode, entityName, resources);

                        InitializeResourceForSubType(entity, entityCode, entityName, resources);

                        InitializeResourceForRelatedEntity(entity, entityCode, entityName, resources);

                        InitializeResourceForEntityRelation(entity, entityCode, entityName, resources);

                        InitializeResourceForEntityWorkFlow(rootTenantCode, entity, entityCode, entityName, resources);

                        #region Entity Fields, Validation and Picklist

                        if (entity.Fields != null && entity.Fields.Count > 0)
                        {
                            foreach (var field in entity.Fields)
                            {
                                if (field.DataType.ToLower() == "picklist" && !String.IsNullOrEmpty(field.TypeOf))
                                {
                                    InitializeResourceForPicklist(field, _iMetadataManager, entity, entityCode, resources);
                                }

                                InitializeResourceForEntityFields(field, entity, entityCode, entityName, resources);

                            }
                        }

                        if (entity.DetailEntities != null && entity.DetailEntities.Count > 0)
                        {
                            foreach (var intersectfield in entity.DetailEntities)
                            {
                                InitializeResourceForIntersectEntity(intersectfield, entity, entityCode, entityName, resources);
                            }
                        }

                        #endregion

                        InitializeResourceForEntityTypeAndRelatedField(entity, entityCode, entityName, resources);

                    }
                }

                InitializeResourceForErrorCodes(resources);
                #endregion

                return resources;
            }
            catch (System.NullReferenceException nullEx)
            {
                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        internal List<Resource> InitialiseForRepairResources(Guid rootTenantCode, Guid currentTenantCode)
        {
            IResourceManager manager = new ResourceManager.Contracts.ResourceManager();
            return manager.GetResources(currentTenantCode);
        }
        private bool IsExistingResourceKey(List<Resource> resourcesList, string key)
        {
            foreach (Resource res in resourcesList)
            {
                if (res.Key == key)
                    return true;
            }
            return false;
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        private List<Entities.EntityCore.Model.Resource.Resource> GetResouceFromJsonFile()
        {
            try
            {
                String fileName = @"Resources\resource.json";
                List<Entities.EntityCore.Model.Resource.Resource> staticResources = new List<Entities.EntityCore.Model.Resource.Resource>();
                Dictionary<string, string> jsonDic = new Dictionary<string, string>();
                using (System.IO.TextReader reader = new StreamReader(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        var json = sr.ReadToEnd();
                        jsonDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                        foreach (string key in jsonDic.Keys)
                        {
                            var staticResource = new Entities.EntityCore.Model.Resource.Resource();
                            staticResource.Key = key;
                            staticResource.Value = jsonDic[key];
                            staticResource.EntityCode = null;
                            staticResource.IsStatic = true;
                            staticResources.Add(staticResource);
                        }
                    }
                }
                return staticResources;
            }
            catch (System.NullReferenceException nullEx)
            {
                throw nullEx;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}