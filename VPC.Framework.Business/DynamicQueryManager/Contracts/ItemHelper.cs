using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal static class ItemHelper {

        internal static string ItemTableName = "[dbo].[Item]";
        internal static string ItemTablePrimarykey = "[Id]";

        internal static string ItemTableItemNameField = "[Name]";
        internal static string Alias = "_it";
        internal static string ItemClassName = "Item";
        internal static string SubTypeField = "SubType";
        
        private static readonly Dictionary<string, string> ItemsData = new Dictionary<string, string> () { 
            { "Item.InternalId", "[Id]" }, 
            { "EntityCode", "[EntityCode]" }, 
             { "TenantId", "[TenantId]" },
            { "SubType", "[EntitySubTypeCode]" }, 
            { "ItemName", "[Name]" }, 
            { "Code", "[Code]" }, 
            { "Active", "[Active]" }, 
            { "UpdatedBy", "[UpdatedBy]" }, 
            { "UpdatedOn", "[UpdatedOn]" }
        };

        // internal static List<FieldModel> GetItemFields(){

        //     var myList = new List<FieldModel>();
        //      PropertyInfo[] propertyInfos = typeof(Item).GetProperties(BindingFlags.Public|BindingFlags.Static);
        //      PropertyInfo[] propertyInfos1 = typeof(Item).GetProperties();
        //     // foreach (var iField in itemsData)
        //     // {
        //     //     var filedModel = new FieldModel();
        //     //     filedModel.ReadOnly = true;
        //     //     filedModel.Name = iField.Key;
        //     //     if(iField.Value!="Id"){
        //     //         filedModel.ApplicableForAdvanceSearch = true;
        //     //         filedModel.ApplicableForSimpleSearch = true;
        //     //         if(iField.Value=="Active"){
        //     //             filedModel.DataType = "PickList";
        //     //             filedModel.ControlType = "DropDown";
        //     //         }
        //     //         myList.Add(filedModel);
        //     //     }
        //     // }
        //     return myList;
        // }

        // internal static Dictionary<string, Dictionary<string, dynamic>> GetItemDetails(Guid id, string tenantId, string entityName, string entityCode, string entitySubtype, string itemName, Guid updatedBy)
        // {
        // var itemDetails = new Dictionary<string, Dictionary<string, dynamic>>();
        //     var itemColumn = new Dictionary<string, dynamic>();
        //     itemColumn.Add("TenantCode", tenantId);
        //     itemColumn.Add("Id", id);
        //     itemColumn.Add("EntityCode", entityCode);//"EN10003"
        //     itemColumn.Add("EntitySubTypeCode", entitySubtype);//"EN10003-ST01"
        //     itemColumn.Add("Name", itemName);
        //     itemColumn.Add("UpdatedBy", updatedBy);
        //     itemColumn.Add("UpdatedOn", DateTime.UtcNow);
        //     itemDetails.Add("[dbo].[Item]", itemColumn);
        //     return itemDetails;
        // }
        internal static List<ColumnAndField> GetItemSelectDetails (Guid tenantId, string entityCode, int index = 0) {

            var itemTables = new List<ColumnAndField> ();

            foreach (var item in ItemsData) {
                var column = new ColumnAndField ();
                column.FieldName = item.Key;
                column.ColumnName = item.Value;
                column.TableName = ItemTableName;
                column.PrimaryKey = ItemTablePrimarykey;
                column.EntityPrefix = Alias;
                column.EntityFullName = ItemClassName;
                column.ClientName = string.Empty;
                column.QueryIndex = index;
                if (index != 0) {
                    index++;
                }
                itemTables.Add (column);
            }
            return itemTables;
        }

        //tapash why these fields are static???
        internal static List<ColumnAndField> GetItemSelectDetailsWithValue (Guid tenantId, Guid itemId, string entityContext, string subtype, string rowName, bool active, Guid updatedBy, string code) {
            var itemTableColumns = ItemHelper.GetItemSelectDetails (tenantId, entityContext);
            foreach (var item in itemTableColumns) {
                if (item.ColumnName.Equals ("[Id]")) {
                    item.Value = itemId;
                } else if (item.ColumnName.Equals ("[EntityCode]") && !string.IsNullOrEmpty (entityContext)) {
                    item.Value = entityContext;
                } else if (item.ColumnName.Equals ("[EntitySubTypeCode]")) {
                    item.Value = subtype;
                } else if (item.ColumnName.Equals ("[Name]") && !string.IsNullOrEmpty (rowName)) {
                    item.Value = rowName;
                } else if (item.ColumnName.Equals ("[Active]")) {
                    item.Value = active;
                } else if (item.ColumnName.Equals ("[Code]") && !string.IsNullOrEmpty (code)) {
                    item.Value = code;
                } else if (item.ColumnName.Equals ("[UpdatedBy]")) {
                    item.Value = updatedBy;
                } else if (item.ColumnName.Equals ("[UpdatedOn]")) {
                    item.Value = DateTime.UtcNow.ToString ("MM/dd/yyyy HH:mm:ss");;
                }
            }

            //added tenant with value..
            var column6 = new ColumnAndField ();
            column6.FieldName = "TenantId";
            column6.ColumnName = "TenantId";
            column6.TableName = ItemTableName;
            column6.PrimaryKey = ItemTablePrimarykey;
            column6.EntityPrefix = Alias;
            column6.Value = tenantId;
            itemTableColumns.Add (column6);

            return itemTableColumns;
        }
    }
}