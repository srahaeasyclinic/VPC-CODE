using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json.Linq;
using VPC.Entities.Common;
using VPC.Entities.Email;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;
//using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    public interface IEntityResourceManager {
        string GetQueryResult (Guid tenantId, string entityName, QueryContext queryModel);
        DataTable GetResult (Guid tenantId, string entityName, QueryContext queryModel);
        DataTable GetResultById (Guid tenantId, string entityName, dynamic id, QueryContext queryModel);
        Guid SaveResult (Guid tenantId, Guid userId, string resourceName, JObject resource, string subtype);

        bool UpdateResult (Guid tenantId, Guid userId, Guid resourceId, string entityName, JObject resource, string subType);
        bool UpdateResultWithoutWorkFlow (Guid tenantId, Guid userId, Guid resourceId, string entityName, JObject resource, string subType);
        bool DeleteResult (Guid tenantId, Guid userId, Guid resultId, string entityName, string subType);
        DataTable GetResultDetailsFromDefaultLayout (Guid tenantId, string name, Guid id, LayoutType form, string subtype, LayoutContext edit);
        bool ExecuteUpdateQuery (string queryRes);
        Guid GetSelectedView (List<FieldModel> fields);
        EmailTemplate GetWellKnownTemplate (Guid tenantId, string entityname, string contextname, int welknowntype, JObject resource);

        EmailTemplate GetTemplate (Guid tenantId, string entityname, string contextname, JObject resource);

        bool UpdateSpecificField (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery, string whatValue); //whichProperty should be property name of a class.
        DataTable GetResultDetailsFromVersion (Guid tenantId, string entityName, Guid id, LayoutType form, string subType, LayoutContext edit, Guid versionId);
        DataTable GetResultDetailsByVersionId (Guid tenantId, string primaryEntityName, Guid primaryEntityId, string versionEntityName, Guid versionId, LayoutType type, string subType, LayoutContext context);
    }

    public sealed class EntityResourceManager : IEntityResourceManager {

        private readonly IEntityQueryManager _queryManager = new EntityQueryManager ();
        readonly IMetadataManager _iMetadataManager = new MetadataManager.Contracts.MetadataManager ();

        DataTable IEntityResourceManager.GetResult (Guid tenantId, string entityName, QueryContext queryModel) {
            return _queryManager.GetResult (tenantId, entityName, queryModel);
        }

        DataTable IEntityResourceManager.GetResultById (Guid tenantId, string entityName, dynamic id, QueryContext queryModel) {
            return GetResultById (tenantId, entityName, id, queryModel);
        }

        DataTable GetResultById (Guid tenantId, string entityName, dynamic id, QueryContext queryModel) {
            return _queryManager.GetResultById (tenantId, entityName, id, queryModel);
        }

        string IEntityResourceManager.GetQueryResult (Guid tenantId, string entityName, QueryContext queryModel) {
            return _queryManager.BuildQuery (tenantId, entityName, queryModel);
        }

        Guid IEntityResourceManager.GetSelectedView (List<FieldModel> fields) {
            var targetId = Guid.NewGuid ();
            string view = GetNewSelectedValue (fields);
            if (!string.IsNullOrEmpty (view)) {
                targetId = new Guid (view);
            }
            return targetId;
        }

        string GetNewSelectedValue (List<FieldModel> fields) {
            string targetId = "";

            foreach (var item in fields) {
                switch (item.ControlType) {
                    case "Section":
                        targetId = GetNewSelectedValue (item.Fields);
                        break;
                    case "Tabs":
                        targetId = GetNewSelectedValue (item.Tabs);
                        break;
                    case "tab":
                        targetId = GetNewSelectedValue (item.Fields);
                        break;
                    case "section":
                        targetId = GetNewSelectedValue (item.Fields);
                        break;
                    case "custom":
                        targetId = Convert.ToString (item.SelectedView);
                        break;
                }

                if (targetId != "")
                    break;
            }

            return targetId;
        }

        Guid IEntityResourceManager.SaveResult (Guid tenantId, Guid userId, string entityName, JObject resource, string subType) {
            IOperationFlowEngine operationEngine = new OperationFlowEngine ();
            var subTypeCode = _iMetadataManager.GetSubTypeId (entityName, subType);

            var mainObj = resource.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals (entityName.ToLower ()));
            var targetObj = mainObj.First ().ToObject<JObject> ();

            var properties = new WorkFlowProcessProperties { EntityName = entityName, SubTypeCode = subTypeCode, UserId = userId, IsSuperAdmin = false };

            operationEngine.PreProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = targetObj }, properties);
            properties.resultId = _queryManager.SaveResult (tenantId, entityName, resource, subTypeCode, userId);
            operationEngine.Process (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = targetObj }, properties);
            operationEngine.FirstOperation (tenantId, properties);
            operationEngine.PostProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = targetObj }, properties);
            return properties.resultId;

            // return _queryManager.SaveResult (tenantId, entityName, resource, subTypeCode, userId);
        }

        bool IEntityResourceManager.UpdateResult (Guid tenantId, Guid userId, Guid resultId, string entityName, JObject resource, string subType) {
            IOperationFlowEngine operationEngine = new OperationFlowEngine ();
            var subTypeCode = _iMetadataManager.GetSubTypeId (entityName, subType);

            var mainObj = resource.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals (entityName.ToLower ()));
            var targetObj = mainObj.First ().ToObject<JObject> ();

            var properties = new WorkFlowProcessProperties { EntityName = entityName, SubTypeCode = subTypeCode, UserId = userId, IsSuperAdmin = false, resultId = resultId };
            operationEngine.PreProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Update, Data = targetObj }, properties);
            var returnVal = _queryManager.UpdateResult (tenantId, userId, entityName, resultId, resource, subType);
            operationEngine.Process (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Update, Data = targetObj }, properties);
            operationEngine.PostProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Update, Data = targetObj }, properties);
            return returnVal;
        }

        bool IEntityResourceManager.UpdateResultWithoutWorkFlow (Guid tenantId, Guid userId, Guid resultId, string entityName, JObject resource, string subType) {
            return _queryManager.UpdateResult (tenantId, userId, entityName, resultId, resource, subType);
        }

        bool IEntityResourceManager.DeleteResult (Guid tenantId, Guid userId, Guid resultId, string entityName, string subType) {
            IOperationFlowEngine operationEngine = new OperationFlowEngine ();
            var subTypeCode = _iMetadataManager.GetSubTypeId (entityName, subType);
            var properties = new WorkFlowProcessProperties { EntityName = entityName, SubTypeCode = subTypeCode, UserId = userId, IsSuperAdmin = false, resultId = resultId };
            operationEngine.PreProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = null }, properties);
            var returnVal = _queryManager.DeleteResult (tenantId, userId, resultId, entityName);
            operationEngine.Process (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = null }, properties);
            operationEngine.PostProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = null }, properties);
            return returnVal;
        }

        DataTable IEntityResourceManager.GetResultDetailsFromDefaultLayout (Guid tenantId, string name, Guid id, LayoutType type, string subtype, LayoutContext context) {
            QueryContext query = MetadataHelper.GetQueryContext (name, 0, 0, "InternalId=" + id);
            ILayoutManager iLayoutManager = new LayoutManager ();
            var fields = iLayoutManager.GetDesignFieldsFromDefaultLayoutForEntity (tenantId, name, type, subtype, (int) context);
            if (fields == null || !fields.Any ()) throw new FieldAccessException ("Layout not found.");
            query.Fields = string.Join (",", fields);
            var result = GetResultById (tenantId, name, id, query);
            return result;
        }
        DataTable IEntityResourceManager.GetResultDetailsByVersionId (Guid tenantId, string primaryEntityName, Guid primaryEntityId, string versionEntityName, Guid versionId, LayoutType type, string subType, LayoutContext context) {

            if (string.IsNullOrEmpty (versionEntityName)) return null;
            QueryContext query = MetadataHelper.GetQueryContext (versionEntityName, 0, 0, "InternalId=" + versionId);
            // var queryFilter = new QueryFilter();
            // queryFilter.FieldName = "DraftVersion";
            // queryFilter.Value = versionId.ToString();
            // queryFilter.Operator = "=";
            // query.Filters.Add(queryFilter);
            ILayoutManager iLayoutManager = new LayoutManager ();
            var fields = iLayoutManager.GetDesignFieldsFromDefaultLayoutForEntity (tenantId, primaryEntityName, type, subType, (int) context);
            if (fields == null || !fields.Any ()) throw new FieldAccessException ("Layout not found.");
            query.Fields = string.Join (",", fields);
            var result = GetResultById (tenantId, versionEntityName, versionId, query);
            return result;
        }
        bool IEntityResourceManager.ExecuteUpdateQuery (string queryRes) {
            return _queryManager.ExecuteUpdateQuery (queryRes);
        }

        /// <summary>
        /// Prepare GetTemplate model
        /// </summary>
        /// <param name="tenantId">tenant Id</param>
        /// <param name="entityname"> emailtemplate or SMS</param>
        /// <param name="contextname">contextname like user,product etc..</param>
        /// <param name="resource">tag replaced value key pair</param>
        /// <returns>EmailTemplate model</returns>
        public EmailTemplate GetWellKnownTemplate (Guid tenantId, string entityname, string contextname, int welknowntype, JObject resource) {
            var queryFilter1 = new List<QueryFilter> ();
            queryFilter1.Add (new QueryFilter { FieldName = "Context", Operator = "Equal", Value = _iMetadataManager.GetEntityContextByEntityName (contextname) });
            queryFilter1.Add (new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString () });
            queryFilter1.Add (new QueryFilter { FieldName = "CommunicationContextType", Operator = "Equal", Value = welknowntype });
            var queryContext1 = new QueryContext { Fields = "Title,Body,Context,CommunicationContextType", Filters = queryFilter1, PageSize = 100, PageIndex = 1 };
            DataTable templatedt = _queryManager.GetResult (tenantId, entityname, queryContext1);
            var template = EntityMapper<EmailTemplate>.Mapper (templatedt);
            if (template != null) {
                if (template.Body != null && !string.IsNullOrEmpty (template.Body.Value)) {
                    template.Body.Value = _iMetadataManager.GetTemplateBodyWithTagablesValue (template.Body.Value, resource);
                }
            }
            return template;

        }

        /// <summary>
        /// Prepare GetTemplate model
        /// </summary>
        /// <param name="tenantId">tenant Id</param>
        /// <param name="entityname">emailtemplate or smstemplate</param>
        /// <param name="contextname">contextname like user,product etc..</param>
        /// <param name="resource">tag replaced value key pair</param>
        /// <returns>EmailTemplate model</returns>
        EmailTemplate IEntityResourceManager.GetTemplate (Guid tenantId, string entityname, string contextname, JObject resource) {
            var queryFilter1 = new List<QueryFilter> ();
            queryFilter1.Add (new QueryFilter { FieldName = "Context", Operator = "Equal", Value = _iMetadataManager.GetEntityContextByEntityName (contextname) });
            queryFilter1.Add (new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString () });
            queryFilter1.Add (new QueryFilter { FieldName = "CommunicationContextType", Operator = "is", Value = null });
            var queryContext1 = new QueryContext { Fields = "Title,Body,Context,CommunicationContextType", Filters = queryFilter1, PageSize = 100, PageIndex = 1 };
            DataTable templatedt = _queryManager.GetResult (tenantId, entityname, queryContext1);

            var template = EntityMapper<EmailTemplate>.Mapper (templatedt);
            if (template != null) {
                if (template.Body != null && !string.IsNullOrEmpty (template.Body.Value)) {
                    template.Body.Value = _iMetadataManager.GetTemplateBodyWithTagablesValue (template.Body.Value, resource);
                }
            }
            return template;
        }

        bool IEntityResourceManager.UpdateSpecificField (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery, string whatValue) {
            return _queryManager.UpdateSpecificField (tenantId, entityName, primaryKeyValue, whichPropery, whatValue);
        }

        DataTable IEntityResourceManager.GetResultDetailsFromVersion (Guid tenantId, string entityName, Guid id, LayoutType type, string subType, LayoutContext context, Guid versionId) {
            var entityDetails = _iMetadataManager.GetEntitityByName (entityName);

            if (entityDetails != null && entityDetails.VersionControl != null) {
                var versionEntitiy = _iMetadataManager.GetEntitityByName (entityDetails.VersionControl.Name);
                if (versionEntitiy == null || versionEntitiy.Fields == null || !versionEntitiy.Fields.Any ()) return null;
                var filterField = versionEntitiy.Fields.FirstOrDefault (t => t.TypeOf.ToLower ().Equals (entityName.ToLower ()));

                var versionFilterString = filterField.Name + "=" + id;
                versionFilterString += ",DraftVersion=" + versionId.ToString ();

                QueryContext query = MetadataHelper.GetVersionQuery (entityDetails.VersionControl.Name, 0, 0, versionFilterString);
                ILayoutManager iLayoutManager = new LayoutManager ();
                var fields = iLayoutManager.GetDesignFieldsFromDefaultLayoutForEntity (tenantId, entityName, type, subType, (int) context);
                if (fields == null || !fields.Any ()) throw new FieldAccessException ("Layout not found.");
                query.Fields = string.Join (",", fields);
                var result = GetResultById (tenantId, entityDetails.VersionControl.Name, id, query);
                return result;
            }

            return null;

        }
    }
}