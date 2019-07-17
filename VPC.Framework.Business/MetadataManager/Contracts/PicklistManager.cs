using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Model.PickListItem;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Infrastructure;
namespace VPC.Framework.Business.MetadataManager.Contracts {
    public interface IPicklistManager {

        List<PicklistObject> GetAllPicklists (Guid tenantId);
        DataTable GetPicklistValues (Guid tenantId, string name, QueryContext queryContext);
        DataTable GetPicklistValueDetails (Guid tenantId, string name, Guid id, LayoutType type, LayoutContext context);
        Guid SavePicklistValue (Guid tenantId, Guid userId, string name, JObject resource);
        bool UpdatePicklistValueDetails (Guid tenantId, string name, Guid id, JObject resource);
        bool DeletePickListValueById (Guid tenantId, string name, Guid id);
        List<string> GetAllCustomizablePicklist ();

        bool IsNonCustomizablePicklist(string picklistName);
    }

    public sealed class PicklistManager : IPicklistManager {
        List<PicklistObject> IPicklistManager.GetAllPicklists (Guid tenantId) {
            var result = GetPicklists ();
            var list = new List<PicklistObject> ();
            if (result == null || !result.Any ()) return list;
            list.AddRange (from item in result where item.BaseType == null || item.BaseType != typeof (ExtendedPicklist) select GetPicklistModel (item, tenantId, false));
            return list;
        }

        List<string> IPicklistManager.GetAllCustomizablePicklist () {
            var list = new List<string> ();
            var result = GetPicklists ();
            foreach (var item in result) {
                if (!item.BaseType.Equals (typeof (ExtendedPicklist))) {
                    PicklistBase pickListBase = (PicklistBase) Activator.CreateInstance (item);
                    var isCustomizeValue = Utility.CustomizeValue (item);
                    if (!isCustomizeValue) continue;
                    list.Add (item.Name);
                }
            }
            return list;
        }
        bool IPicklistManager.IsNonCustomizablePicklist (string picklistName) {
            var isCustomizeValue = false;
            var result = GetPicklists ();
            foreach (var item in result) {
                if (!item.BaseType.Equals (typeof (ExtendedPicklist)) && item.Name.ToLower ().Equals (picklistName.ToLower())) {
                    PicklistBase pickListBase = (PicklistBase) Activator.CreateInstance (item);
                    var res = Utility.CustomizeValue (item);
                    isCustomizeValue = (!res);
                }
            }
            return isCustomizeValue;
        }
        DataTable IPicklistManager.GetPicklistValues (Guid tenantId, string name, QueryContext queryContext) {
            var result = GetPicklists ();
            if (result != null) {
                TypeInfo picklist = null;
                foreach (var item in result) {
                    if (!item.BaseType.Equals (typeof (ExtendedPicklist))) {
                        var pluralName = Utility.GetPluralName (item);
                        if (pluralName.ToLower ().Equals (name.ToLower ()) || item.Name.ToLower ().Equals (name.ToLower ())) {
                            picklist = item;
                            break;
                        }
                    }
                }
                if (picklist != null) {
                    PicklistBase pickListBase = (PicklistBase) Activator.CreateInstance (picklist);
                    var isCustomizeValue = Utility.CustomizeValue (picklist);
                    if (isCustomizeValue) {
                        IPicklistQueryManager iPicklistQueryManager = new PicklistQueryManager ();
                        return iPicklistQueryManager.GetResult (tenantId, name, queryContext);
                    }
                    return pickListBase.GetValues ();
                }
            }
            throw new ArgumentException ("Picklists not found");
        }

        DataTable IPicklistManager.GetPicklistValueDetails (Guid tenantId, string name, Guid id, LayoutType type, LayoutContext context) {
            QueryContext query = MetadataHelper.GetQueryContext (name, 0, 0, "InternalId=" + id);
            ILayoutManager iLayoutManager = new LayoutManager ();
            var fields = iLayoutManager.GetDesignFieldsFromDefaultLayoutForPickList (tenantId, name, type, (int) context);
            if (fields != null && fields.Any ()) {
                query.Fields = string.Join (",", fields);
            }
            IPicklistQueryManager iPicklistQueryManager = new PicklistQueryManager ();
            return iPicklistQueryManager.GetResultById (tenantId, name, id, query);
        }

        private List<TypeInfo> GetPicklists () {
            var classes = Assembly
                .GetEntryAssembly () ?
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type => typeof (PicklistBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) && !type.IsAbstract).ToList ();
            if (classes == null)
                throw new ArgumentException ("Picklists not found");
            return classes;
        }

        private PicklistObject GetPicklistModel (Type item, Guid tenantId, bool getvalues = true, int pageIndex = 1, int pageSize = 10, string filters = "") {
            var picklist = new PicklistObject {
            Name = item.Name,
            PluralName = Utility.GetPluralName (item),
            DisplayName = Utility.GetDisplayName (item)
            };
            picklist.FixedValueList = picklist.IsStandard = (typeof (SimplePicklist).IsAssignableFrom (item));
            picklist.CustomizeValue = Utility.CustomizeValue (item); //(typeof(ComplexPicklist).IsAssignableFrom(item));
            PicklistBase pickListBase = (PicklistBase) Activator.CreateInstance (item);
            picklist.Id = pickListBase.PicklistContext.GetContext ();
            if (item.BaseType != null) picklist.Type = item.BaseType.Name;
            if (!getvalues) return picklist;

            picklist.Values = new List<PicklistValueV1> ();
            DataTable picklistValues = null;
            if (picklist.FixedValueList) {
                picklistValues = pickListBase.GetValues ();
            } else {
                IPicklistQueryManager iEntityResourceManager = new PicklistQueryManager ();
                QueryContext query = MetadataHelper.GetQueryContext (item.Name, pageIndex, pageSize, filters);
                picklistValues = iEntityResourceManager.GetResult (tenantId, item.Name.ToLower (), query);
            }
            if (picklistValues == null) return picklist;
            List<PicklistValueV1> lists = Utility.ConvertDataTable<PicklistValueV1> (picklistValues);
            foreach (var list in lists) {
                list.PicklistId = picklist.Id;
            }
            picklist.Values.AddRange (lists);
            return picklist;
        }

        Guid IPicklistManager.SavePicklistValue (Guid tenantId, Guid userId, string name, JObject resource) {
            IPicklistQueryManager queryManager = new PicklistQueryManager ();
            return queryManager.SaveResult (tenantId, userId, name, resource, string.Empty);
        }

        bool IPicklistManager.UpdatePicklistValueDetails (Guid tenantId, string name, Guid id, JObject resource) {
            IPicklistQueryManager queryManager = new PicklistQueryManager ();
            return queryManager.UpdateResult (tenantId, id, name, resource, "");
        }

        bool IPicklistManager.DeletePickListValueById (Guid tenantId, string name, Guid id) {
            IPicklistQueryManager queryManager = new PicklistQueryManager ();
            return queryManager.DeleteResult (tenantId, name, id);
        }

    }

}