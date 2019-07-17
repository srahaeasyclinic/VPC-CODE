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
    }

    public sealed class InitilizeManager : IInitilizeManager
    {
        private readonly IInitilizeReview _review;
        private readonly IManagerWorkFlowSecurity _managerWorkFlow;
        private readonly IPicklistManager _picklIstManager;
        IMetadataManager iMetadataManager;
        public InitilizeManager()
        {
            _review = new InitilizeReview();
            _managerWorkFlow = new ManagerWorkFlowSecurity();
            _picklIstManager = new PicklistManager();
            iMetadataManager = new MetadataManager.Contracts.MetadataManager();
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

            //List<TypeInfo> picklistClasses = Assembly
            //       .GetEntryAssembly()
            //       .GetReferencedAssemblies()
            //       .Select(Assembly.Load)
            //       .SelectMany(x => x.DefinedTypes)
            //       .Where(type =>
            //           typeof(PicklistBase).GetTypeInfo().IsAssignableFrom(type.AsType())
            //       ).ToList();

            List<TypeInfo> metadataClasses = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(type =>
                   typeof(EntityBase).GetTypeInfo().IsAssignableFrom(type.AsType()) && !type.IsAbstract).ToList();

            //foreach (var id in entityIds)
            //{
            //    foreach (var item in picklistClasses)
            //    {
            //        PicklistBase instance = (PicklistBase)Activator.CreateInstance(item);
            //        var contextId = instance.PicklistContext.GetContext().ToString();
            //        if (contextId.Equals(id))
            //        {
            //            var entityInfo = new EntityInfo();
            //            entityInfo.EntityContext = id;
            //            entityInfo.LayoutFor = LayoutFor.Picklist;
            //            entities.Add(entityInfo);
            //        }
            //    }
            //}

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

            //Initialize resources
            InitilizeResources (rootTenantCode, initilizedTenantCode);
            // if(metadataClasses.Count > 0)
            // {
            //     InitilizeResources(rootTenantCode, initilizedTenantCode, metadataClasses);
            // }
            // else
            // {
            //     InitilizeResources(rootTenantCode, initilizedTenantCode);
            // }
            

            //Initialize Picklist
            //InitializePicklistValue(picklists, rootTenantCode, initilizedTenantCode, userId);

            //Init Menu group
            InitMenuGroup(rootTenantCode, initilizedTenantCode, userId);

            //Initialize Menu Item
            InitializeMenu(rootTenantCode, initilizedTenantCode);

            

            //Initialize tenant subscription
            var subscriptionId = InitilizeTenantSubscription(rootTenantCode, initilizedTenantCode, masterSubscriptionId);

            //Init Email Template
            InitEmailTemplate(initilizedTenantCode, userId);

            //Intialize Batch type
            InitBatchType(rootTenantCode, initilizedTenantCode);
            //Initilize Password policy
            InitCredential(rootTenantCode, initilizedTenantCode, userId);
            // Initlize Smtp settings
            InitSmtpSettings(rootTenantCode, initilizedTenantCode);
            //Init Role view
            IntiRoleView(initilizedTenantCode);

            return responses;
        }

         private void IntiRoleView(Guid newTenantId)
        {

            ILayoutManager _iLayoutManager=new LayoutManager();
                var  userLayouts = _iLayoutManager.GetLayoutsByEntityName(newTenantId, "User");
                if(userLayouts.Count>0)
                {
                    var  roleLayout = _iLayoutManager.GetLayoutsByEntityName(newTenantId, "Role");
                    LayoutModel viewLayout=new LayoutModel();
                    if(roleLayout.Count>0)
                    {
                        viewLayout=(from roleLay in roleLayout where roleLay.LayoutType==LayoutType.View select roleLay).FirstOrDefault();
                    }

                    foreach(var userLayout in userLayouts)
                    {
                        if(userLayout.LayoutType==LayoutType.Form)
                        {
                            var details=_iLayoutManager.GetLayoutsDetailsById(newTenantId,userLayout.Id);
                            if(details.FormLayoutDetails!=null)
                            {
                                 foreach(var detail in details.FormLayoutDetails.Fields)
                                 {
                                     if(detail.DataType=="Section")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(newTenantId,userLayout.Id,details);
                                             }
                                         }
                                      }
                                     else if(detail.DataType=="Tabs")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(newTenantId,userLayout.Id,details);
                                             }

                                         }

                                      }
                                      else if(detail.DataType=="Tabs")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(newTenantId,userLayout.Id,details);
                                             }

                                         }

                                      }
                                      
                                      else if(detail.DataType=="Role" && detail.Name=="UserInRole")
                                             {
                                                 detail.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(newTenantId,userLayout.Id,details);
                                             }

                                 }
                            }                            
                        }
                    }               

                }
        }


        private void InitMenuGroup(Guid rootTenantId, Guid newTenantId, Guid userId)
        {
            var fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForMenuGroup.IconClass";
            SavePickListValues(rootTenantId, newTenantId, "MenuGroup", fields, userId);
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
                    else if (getAllPickListValueList.TypeOf == "MenuGroup")
                    {
                        fields = "InternalId,PicklistContext,Key,Text,Flagged,IsDeletetd,Active,IsDefault,PickListValueForMenuGroup.IconClass";
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
        private void InitBatchType(Guid rootTenantId, Guid newTenantId)
        {
            ISecurityCacheManager securityCacheManager=new SecurityCacheManager();
            IManagerBatchType _managerBatchType = new ManagerBatchType();
            var batchTypes = _managerBatchType.GetBatchTypes(rootTenantId);
            if (batchTypes.Count > 0)
            {
                var enableBatchTypes = (from batchType in batchTypes where batchType.Status select batchType).ToList();
                if (enableBatchTypes.Count > 0)
                {
                    List<BatchTypeInfo> batchs = new List<BatchTypeInfo>();
                    foreach (var enableBatchType in enableBatchTypes)
                    {
                        enableBatchType.BatchTypeId = Guid.NewGuid();
                        batchs.Add(enableBatchType);
                    }
                    _managerBatchType.CreateBatchTypes(newTenantId, batchs);
                }
            }
            securityCacheManager.BatchTypeClear();
            securityCacheManager.BatchTypesCache();


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

            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", jsonObjectEmailTemplate, emailTemplateSubTypes[0].Name.ToString());

            //Reset Password
            //body=@"<p>&nbsp;</p><p>Dear [FirstName]&nbsp;[MiddleName]&nbsp;[LastName] ,&nbsp;<br />Welcome to Aboard!</p><p>Your credential is</p><p>Username:&nbsp;[UserCredential.Username]</p><p>Password:&nbsp;[UserCredential.Password]</p><p>Reagrds,</p><p>View Team,</p><p>&nbsp;</p>";
            dynamic jsonObjectResetPassword = new JObject();
            jsonObjectResetPassword.Title = EmailTemplatesContent.ResetPasswordTitle;
            jsonObjectResetPassword.Context = entityName;
            jsonObjectResetPassword.Body = EmailTemplatesContent.ResetPasswordBody; //.Replace("&", "&amp;").Replace("'", "&apos;");
            jsonObjectResetPassword.CommunicationContextType = (int)ContextTypeEnum.Passwordreset;
            jsonObjectResetPassword.EntityContext = entityContext;
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", jsonObjectResetPassword, emailTemplateSubTypes[0].Name.ToString());

            //Change Password
            dynamic jsonObjectChangePassword = new JObject();
            jsonObjectChangePassword.Title = EmailTemplatesContent.ChangePasswordTitle;
            jsonObjectChangePassword.Context = entityName;
            jsonObjectChangePassword.Body = EmailTemplatesContent.ChangePasswordBody;//.Replace ("&", "&amp;").Replace ("'", "&apos;");;
            jsonObjectChangePassword.CommunicationContextType = (int)ContextTypeEnum.Forgotpassword;
            jsonObjectChangePassword.EntityContext = entityContext;
            _iEntityResourceManager.SaveResult(initilizedTenantCode, userId, "Emailtemplate", jsonObjectChangePassword, emailTemplateSubTypes[0].Name.ToString());

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
                        bool newLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New) != null;
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

                        var defaultNewLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New && z.EntityId == id.EntityContext).ToList();
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
                    var defaultNewLayouts = rootTenantLayouts.Where(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New && z.EntityId == id.EntityContext).ToList();
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
                        bool newLayoutPresent = entityDefaultLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New) != null;
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

                        var defaultNewLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New && z.EntityId == id.EntityContext);
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

                    var defaultNewLayout = rootTenantLayouts.FirstOrDefault(z => z.LayoutType == LayoutType.Form && z.Context == LayoutContext.New && z.EntityId == id.EntityContext);
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

        private void InitilizeResources(Guid rootTenantCode, Guid currentTenantCode)
        {
            IResourceManager manager = new ResourceManager.Contracts.ResourceManager();
            manager.CopyResources(rootTenantCode, currentTenantCode);
        }

        private void InitilizeResources(Guid rootTenantCode, Guid currentTenantCode, List<TypeInfo> metadataClasses)
        {
            try
            {
                var resources = new List<Entities.EntityCore.Model.Resource.Resource>();
                IMetadataManager _iMetadataManager = new MetadataManager.Contracts.MetadataManager();
                IResourceManager manager = new ResourceManager.Contracts.ResourceManager();

                #region Add all resources

                
                foreach (var item in metadataClasses)
                {
                    var entity = _iMetadataManager.GetEntitityByName(item.Name);

                    if(entity.Operations != null && entity.Operations.Count > 0)
                    {
                        foreach (var operation in entity.Operations)
                        {

                            var operationResource = new Entities.EntityCore.Model.Resource.Resource();
                            operationResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "Operation", operation.Name.Trim());
                            operationResource.Value = operation.Name.Trim();
                            resources.Add(operationResource);
                        }
                    }

                    if(entity.RowLevelOperations != null && entity.RowLevelOperations.Count > 0)
                    {
                        foreach(var rowLevelOperation in entity.RowLevelOperations)
                        {
                            var rowLevelOperationResource = new Entities.EntityCore.Model.Resource.Resource();
                            rowLevelOperationResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "RowLevelOperation", rowLevelOperation.Name.Trim());
                            rowLevelOperationResource.Value = rowLevelOperation.Name.Trim();
                            resources.Add(rowLevelOperationResource);
                        }
                    }

                    if (!String.IsNullOrEmpty(entity.DisplayName))
                    {
                        var displayNameResource = new Entities.EntityCore.Model.Resource.Resource();
                        displayNameResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "DisplayName", entity.DisplayName.Trim());
                        displayNameResource.Value = entity.DisplayName.Trim();
                        resources.Add(displayNameResource);
                    }

                    if (!String.IsNullOrEmpty(entity.PluralName))
                    {
                        var pluralNameResource = new Entities.EntityCore.Model.Resource.Resource();
                        pluralNameResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "PluralName", entity.PluralName.Trim());
                        pluralNameResource.Value = entity.PluralName.Trim();
                        resources.Add(pluralNameResource);

                    }

                    if (entity.Tasks != null && entity.Tasks.Count > 0)
                    {
                        foreach (var task in entity.Tasks)
                        {
                            var taskResource = new Entities.EntityCore.Model.Resource.Resource();
                            taskResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "Task", task.Name.Trim());
                            taskResource.Value = task.Name.Trim();
                            resources.Add(taskResource);

                        }
                    }


                    if (entity.Subtypes != null && entity.Subtypes.Count > 0)
                    {
                        foreach (var subtype in entity.Subtypes)
                        {
                            var subtypeResource = new Entities.EntityCore.Model.Resource.Resource();
                            subtypeResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "SubType", subtype.Trim());
                            subtypeResource.Value = subtype.Trim();
                            resources.Add(subtypeResource);
                        }
                    }

                    

                    if(entity.RelatedEntities != null && entity.RelatedEntities.Count > 0)
                    {
                        foreach (var relatedEntity in entity.RelatedEntities)
                        {
                            var relatedEntityResource = new Entities.EntityCore.Model.Resource.Resource();
                            relatedEntityResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "RelatedEntity", relatedEntity.Name.Trim());
                            relatedEntityResource.Value = relatedEntity.Name.Trim();
                            resources.Add(relatedEntityResource);
                        }
                    }

                    if(entity.Relations != null && entity.Relations.Count > 0)
                    {
                        foreach (var relation in entity.Relations)
                        {
                            var relationResource = new Entities.EntityCore.Model.Resource.Resource();
                            relationResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "Relation", relation.Name.Trim());
                            relationResource.Value = relation.Name.Trim();
                            resources.Add(relationResource);
                        }
                    }

                    if (entity.Fields != null && entity.Fields.Count > 0)
                    {
                        foreach (var field in entity.Fields)
                        {

                            var fieldResource = new Entities.EntityCore.Model.Resource.Resource();
                            string fieldName = string.Empty;
                            if (field.Name.Contains('.'))
                            {
                                fieldName = field.Name.Replace('.', '_');
                            }
                            else
                            {
                                fieldName = field.Name;
                            }

                            fieldResource.Key = String.Format("{0}_{1}_{2}", entity.Name.Trim(), "Field", fieldName.Trim());
                            fieldResource.Value = fieldName;
                            resources.Add(fieldResource);

                        }
                    }
                }
            

                #endregion

                bool isResourceCreated = manager.CreateResources(rootTenantCode, currentTenantCode, resources);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }


        private Guid InitilizeTenantSubscription(Guid rootTenantCode,Guid initilizedTenantCode,Guid subscriptionId)
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
                //Create subscription entities
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

    }
}