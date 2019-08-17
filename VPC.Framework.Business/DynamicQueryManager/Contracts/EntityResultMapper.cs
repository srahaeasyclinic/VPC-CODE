using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Metadata.Business.Entity.CustomField;
using VPC.Metadata.Business.Entity.CustomField.Execution;


namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal class EntityResultMapper {

        public class MappedItem {
            public string TypeOf { get; set; }
            public string Name { get; set; }

            public DataTable Result { get; set; }
        }
        public DataTable MapResult (Guid tenantId, string entityName, DataTable source, QueryContext queryModel)
        {

            if (source == null || source.Rows.Count == 0) return source;

            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            var subTypes = iMetadataManager.GetSubTypesDetails(entityName);
            IPicklistManager iPicklistManager = new PicklistManager();
            var picklists = iPicklistManager.GetAllPicklists(tenantId);
            var simpleNonCustomizable = picklists.Where(t => !t.CustomizeValue).ToList();
            if (!simpleNonCustomizable.Any()) return source;
            var entity = iMetadataManager.GetEntitityByName(entityName);
            List<MappedItem> val = new List<MappedItem>();
            if(entity.Fields!=null && entity.Fields.Any()){
                GetUsedPicklistInEntity(tenantId, simpleNonCustomizable, entity.Fields, val);
            }
            if(entity.VersionControl!=null && entity.VersionControl.Fields!=null && entity.VersionControl.Fields.Any()){
                GetUsedPicklistInEntity(tenantId, simpleNonCustomizable, entity.VersionControl.Fields, val);
            }

            DataTable dtClone = source.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in source.Rows)
            {
                dtClone.ImportRow(dr);
            }

            var customFields = new List<FieldModel>();
            // string[] userQuery = null;
            if (queryModel.Fields != null && queryModel.Fields.Any())
            {
                var entityDetails = iMetadataManager.GetEntitityByName(entityName);

                if (entityDetails?.Fields != null && entityDetails.Fields.Any())
                {
                    var userQuery = queryModel?.Fields?.Split(',');
                    foreach (var clientField in userQuery)
                    {
                        var match = entityDetails.Fields.FirstOrDefault(t =>
                            t.Name.ToLower().Equals(clientField.ToString().ToLower()) && !string.IsNullOrEmpty(t.Context) && !string.IsNullOrEmpty(t.ContextType));
                        if (match != null)
                        {
                            if (match.ContextType.Equals("CustomServerFieldBase"))
                            {
                                dtClone.Columns.Add(match.Name, typeof(string));
                                customFields.Add(match);
                            }
                            else if (match.ContextType.Equals("CustomClientFieldBase"))
                            {
                                DataColumn client = new DataColumn(match.Name, typeof(string));
                                //  client.DefaultValue = match.Context;
                                dtClone.Columns.Add(client);
                            }
                        }
                    }
                }
            }

            foreach (DataRow item in dtClone.Rows)
            {
                try
                {
                    var subType = item["SubType"];
                    var mapped = subTypes.FirstOrDefault(t => t.Key.Equals(subType));
                    if (mapped.Key != null)
                    {
                        item["SubType"] = mapped.Value.ToString();
                    }

                    foreach (var data in val)
                    {
                        if (item.Table.Columns.Contains(data.Name.ToLower()))
                        {
                            if (data.Result != null && data.Result.Rows.Count > 0)
                            {
                                var actualValue = item[data.Name].ToString();
                                DataRow[] rowsData = data.Result.Select();
                                for (int i = 0; i < rowsData.Length; i++)
                                {
                                    var existenceValue = rowsData[i]["InternalId"];
                                    if (actualValue.ToString().ToLower().Equals(existenceValue.ToString().ToLower()))
                                    {
                                        var text = rowsData[i]["Text"].ToString();
                                        item[data.Name] = text.ToString();
                                    }
                                }
                            }
                        }

                    }
                    //------------------------- custom client field
                    if (customFields.Any())
                    {
                        foreach (var cf in customFields)
                        {
                            if (item.Table.Columns.Contains(cf.Name))
                            {
                                var conditions = new Dictionary<string, string>();
                                //conditions.Add ("InternalId", item["InternalId"].ToString ());
                                var id = Guid.Parse(item["InternalId"].ToString());
                                item[cf.Name] = GetCustomFieldValue(id, cf.Context, conditions);
                            }
                        }
                    }

                }
                catch
                { //digest exceptions }
                }



            }
            return dtClone;
        }

        private static void GetUsedPicklistInEntity(Guid tenantId, List<Entities.EntityCore.Model.PickListItem.PicklistObject> simpleNonCustomizable, List<FieldModel>fields, List<MappedItem> val)
        {
            IPicklistManager iPicklistManager = new PicklistManager();

            foreach (var field in fields)
            {
                var matchingField = simpleNonCustomizable.FirstOrDefault(t => t.Name.ToLower().Equals(field.TypeOf.ToLower()));
                if (matchingField != null)
                {
                    var values = iPicklistManager.GetPicklistValues(tenantId, matchingField.Name, null);
                    if (values != null && values.Rows.Count > 0)
                    {
                        var mappedItem = new MappedItem();
                        mappedItem.Name = field.Name;
                        mappedItem.TypeOf = field.TypeOf;
                        mappedItem.Result = values;
                        val.Add(mappedItem);
                    }
                }
            }
        }

        public DataTable GetCustomField (Guid tenantId, string entityName, DataTable source, QueryContext queryModel) {
            if (source == null || source.Rows.Count == 0)return source;
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var subTypes = iMetadataManager.GetSubTypesDetails (entityName);
            IPicklistManager iPicklistManager = new PicklistManager ();
            var entity = iMetadataManager.GetEntitityByName (entityName);
            List<MappedItem> val = new List<MappedItem> ();
            var customFields = new List<FieldModel> ();
            // string[] userQuery = null;
            if (queryModel.Fields != null && queryModel.Fields.Any ()) {
                var entityDetails = iMetadataManager.GetEntitityByName (entityName);
                if (entityDetails?.Fields != null && entityDetails.Fields.Any ()) {
                    var userQuery = queryModel?.Fields?.Split (',');
                    foreach (var clientField in userQuery) {
                        var match = entityDetails.Fields.FirstOrDefault (t =>
                            t.Name.ToLower ().Equals (clientField.ToString ().ToLower ()) && !string.IsNullOrEmpty(t.Context) && !string.IsNullOrEmpty(t.ContextType));
                        if (match != null) {
                            if (match.ContextType.Equals ("CustomServerFieldBase")) {
                                source.Columns.Add (match.Name, typeof (string));
                                customFields.Add (match);
                            } else if (match.ContextType.Equals ("CustomClientFieldBase")) {
                                DataColumn client = new DataColumn (match.Name, typeof (string));
                               // client.DefaultValue = match.Context;
                                source.Columns.Add (client);
                            }
                        }
                    }
                }
            }

            foreach (DataRow item in source.Rows) {
                try {
                    if (customFields.Any ()) {
                        foreach (var cf in customFields) {
                            if (item.Table.Columns.Contains (cf.Name)) {
                                var conditions = new Dictionary<string, string> ();
                                var id = Guid.Parse(item["InternalId"].ToString());
                                item[cf.Name] = GetCustomFieldValue (id, cf.Context, conditions);
                            }
                        }
                    }
                } catch { //digest exceptions }
                }
            }
            return source;
        }
        private string GetCustomFieldValue (Guid id, string methodName, Dictionary<string, string> payload) {
            var customServerFields = new CustomServerFieldEngine ();
            var customFieldPayload = new CustomFieldExecutionPayload {
                Payload = payload,
                Id = id,
            };
            var value = customServerFields.GetValue (methodName, customFieldPayload);
            return value;
        }
    }
}