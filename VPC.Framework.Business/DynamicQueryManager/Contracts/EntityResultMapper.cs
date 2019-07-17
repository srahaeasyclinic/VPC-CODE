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

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal class EntityResultMapper {

        public class MappedItem {
            public string TypeOf { get; set; }
            public string Name { get; set; }

            public DataTable Result { get; set; }
        }
        public DataTable MapResult (Guid tenantId, string entityName, DataTable source) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var subTypes = iMetadataManager.GetSubTypesDetails (entityName);
            IPicklistManager iPicklistManager = new PicklistManager ();
            var picklists = iPicklistManager.GetAllPicklists (tenantId);
            var simpleNonCustomizable = picklists.Where (t => !t.CustomizeValue).ToList ();
            if (!simpleNonCustomizable.Any ()) return source;
            var entity = iMetadataManager.GetEntitityByName (entityName);
            List<MappedItem> val = new List<MappedItem> ();
            foreach (var field in entity.Fields) {
                var matchingField = simpleNonCustomizable.FirstOrDefault (t => t.Name.ToLower ().Equals (field.TypeOf.ToLower ()));
                if (matchingField != null) {
                    var values = iPicklistManager.GetPicklistValues (tenantId, matchingField.Name, null);
                    if (values != null && values.Rows.Count > 0) {
                        var mappedItem = new MappedItem ();
                        mappedItem.Name = field.Name;
                        mappedItem.TypeOf = field.TypeOf;
                        mappedItem.Result = values;
                        val.Add (mappedItem);
                    }
                }
            }

            DataTable dtClone = source.Clone (); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++) {
                if (dtClone.Columns[i].DataType != typeof (string))
                    dtClone.Columns[i].DataType = typeof (string);
            }

            foreach (DataRow dr in source.Rows) {
                dtClone.ImportRow (dr);
            }

            foreach (DataRow item in dtClone.Rows) {
                try {
                    var subType = item["SubType"];
                    var mapped = subTypes.FirstOrDefault (t => t.Key.Equals (subType));
                    if (mapped.Key != null) {
                        item["SubType"] = mapped.Value.ToString ();
                    }
                    foreach (var data in val) {
                        if (item.Table.Columns.Contains (data.Name.ToLower ())) {
                            if (data.Result != null && data.Result.Rows.Count > 0) {
                                var actualValue = item[data.Name].ToString ();
                                DataRow[] rowsData = data.Result.Select ();
                                for (int i = 0; i < rowsData.Length; i++) {
                                    var existenceValue = rowsData[i]["InternalId"];
                                    if (actualValue.ToString ().ToLower ().Equals (existenceValue.ToString ().ToLower ())) {
                                        var text = rowsData[i]["Text"].ToString ();
                                        item[data.Name] = text.ToString ();
                                    }
                                }
                            }
                        }

                    }
                } catch { }
            }
            return dtClone;
        }
    }
}