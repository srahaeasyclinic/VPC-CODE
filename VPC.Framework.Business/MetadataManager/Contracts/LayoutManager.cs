using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.Role;
using VPC.Framework.Business.MetadataManager.API;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Framework.Business.MetadataManager.Contracts {
    public interface ILayoutManager {
        List<LayoutModel> GetLayoutsByEntityName (Guid tenantId, string entityName);
        List<LayoutModel> GetLayoutsByEntityContext (Guid tenantId, string entityContext);

        LayoutModel GetLayoutsDetailsById (Guid tenantId, Guid id);
        //LayoutModel GetLayoutsDetailsById(Guid tenantId, string entityName, string type, string subtype, string context);
        LayoutModel GetDefaultLayoutForEntity (Guid tenantId, string entityName, int layoutType, string subType, int context);
        Guid Create (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId);
        Guid CreatePicklistLayout (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId);
        List<LayoutModel> GetLayoutsByPicklistName (Guid tenantId, string picklistName);
        void UpdateLayoutDetails (Guid tenantId, Guid layoutId, LayoutModel templateModel);
        void UpdateLayoutDetailsXml (Guid tenantId,List<LayoutModel> templateModel);
        void SetPicklistLayoutDefault (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId);
        void SetListLayoutDefault (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId);
        void DeletePicklistLayout (Guid tenantId, Guid layoutId);
        void DeleteListLayout (Guid tenantId, Guid layoutId);
        LayoutModel GetPicklistLayoutDetailsById (Guid tenantId, Guid id);
        void UpdatePicklistLayout (Guid tenantId, Guid layoutId, LayoutModel layout);

        LayoutModel GetDefaultPicklistLayout (Guid tenantId, string entityName, LayoutType layoutType, int context);
        List<string> GetDesignFieldsFromDefaultLayoutForPickList (Guid tenantId, string name, LayoutType type, int context);

        List<string> GetDesignFieldsFromDefaultLayoutForEntity (Guid tenantId, string name, LayoutType type, string subType, int context);

        List<LayoutModel> GetLayoutsByEntityName (Guid tenantId, string entityContext, int type, bool isPicklist);
        Guid Create (LayoutModel layoutModel, Guid userId, Guid tenantId);
        List<LayoutModel> GetPicklistLayout (Guid tenantId, string picklistId);
        Guid CreatePicklistLayout (LayoutModel layoutModel, Guid userId, Guid tenantId);
        Dictionary<string, string> GetDetailEntitiesFromDefaultLayoutForEntity (Guid tenantId, string entityName, int layoutType, string subType, int context);

        Guid GetTenantId (string entityName, string code);

        List<RoleInfo> GetUserRoles (Guid userId);
        List<FieldModel> GetComputedFields (Guid tenantId, string name, LayoutType type, string subtype, int context);

        Guid CloneLayout (string entityName, Guid layoutId, LayoutModel layoutModel, Guid userId, Guid tenantId);

        Guid ClonePicklistLayout (string entityName, Guid layoutId, LayoutModel layoutModel, Guid userId, Guid tenantId);
    }

    public sealed class LayoutManager : ILayoutManager {

        private readonly ILayoutAdmin _admin;
        private readonly ILayoutReview _review;

        private readonly IMetadataManager _iMetadataManager;
        public LayoutManager () {
            _admin = new LayoutAdmin ();
            _review = new LayoutReview ();
            _iMetadataManager = new MetadataManager ();
        }

        //public LayoutModel GetLayoutDetail(Guid tenantId, string entityName, int layoutType, int subType, int context)
        //{
        //    var entityContext = GetEntityContext(entityName);
        //    if (string.IsNullOrEmpty(entityContext)) return null;
        //    return _review.GetLayoutsDetail(tenantId, entityContext, layoutType, subType, context);
        //}

        LayoutModel ILayoutManager.GetDefaultLayoutForEntity (Guid tenantId, string entityName, int layoutType, string subType, int context) {
            return GetDefaultLayoutForEntity (tenantId, entityName, layoutType, subType, context);
        }

        LayoutModel GetDefaultLayoutForEntity (Guid tenantId, string entityName, int layoutType, string subType, int context) {
            var entityContext = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            if (string.IsNullOrEmpty (entityContext)) return null;
            //return _review.GetLayoutsDetail(tenantId, entityContext, layoutType, _iMetadataManager.GetSubTypeId(subType), context);
            var layout = _review.GetLayoutsDetail (tenantId, entityContext, layoutType, _iMetadataManager.GetSubTypeId (entityName, subType), context);

            if (layout != null) {
                MapLayoutDetails (tenantId, layout);

                if (layoutType.Equals ((int) LayoutType.List) && layout.ListLayoutDetails != null && layout.ListLayoutDetails.Fields != null && layout.ListLayoutDetails.Fields.Any ()) {
                    foreach (var field in layout.ListLayoutDetails.Fields) {
                        if (field.DataType.ToLower ().Equals ("picklist") && !string.IsNullOrEmpty (field.DefaultView)) {
                            var splitPicklist = field.Name.Split ('.');
                            var picklistName = splitPicklist[splitPicklist.Count () - 1];
                            var view = GetDefaultPicklistLayout (tenantId, picklistName, LayoutType.View, 0);
                            //it should be view layout...
                            if (view != null && view.ViewLayoutDetails != null && view.ViewLayoutDetails.Fields != null && view.ViewLayoutDetails.Fields.Any ()) {

                                foreach (var item in view.ViewLayoutDetails.Fields) {
                                    layout.ListLayoutDetails.Fields.Add (item);
                                }
                            }
                        }
                    }
                }

                //append plural name
                var result = _iMetadataManager.GetEntitityByName(entityName);
                if(result != null && result.PluralName != null)
                {
                    layout.PluralName = result.PluralName;
                }

                //append singular name               
                if(result != null && result.DisplayName != null)
                {
                    layout.SingularName = result.DisplayName;
                }

                //append version name               
                if(result != null && result.VersionControl != null && !string.IsNullOrEmpty(result.VersionControl.Name))
                {
                    layout.VersionName =Char.ToLowerInvariant( result.VersionControl.Name[0]) +  result.VersionControl.Name.Substring(1); ;
                }

            }

            return layout;
        }

        // private void addPicklistViewFields(Guid tenantId, List<SelectedItem> fields)
        // {
        //     // foreach(var item in fields) 
        //     // { 
        //     //     if(item.DataType == "PickList" && item.DefaultView != null && item.DefaultView != "")
        //     //     {
        //     //         var layout = GetPicklistLayoutDetailsById(tenantId, item.DefaultView);
        //     //     }
        //     // } 
        // }

        Guid ILayoutManager.Create (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId) {
            layoutModel.Id = Guid.NewGuid ();
            layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            layoutModel.ModifiedBy = userId;
            layoutModel.Subtype = _iMetadataManager.GetSubTypeId (entityName, layoutModel.SubtypeeName);
            //if (entityName == "User")
            //{
            //    layoutModel.EntityId = "EN10003";
            //}
            //layoutModel.ModifiedBy = new Guid("652F1C5A-7DC9-46DC-B6D9-B53FAB6B6985");

            _admin.CreateLayout (tenantId, layoutModel);
            return layoutModel.Id;
        }

        Guid ILayoutManager.Create (LayoutModel layoutModel, Guid userId, Guid tenantId) {
            layoutModel.Id = Guid.NewGuid ();
            layoutModel.ModifiedBy = userId;
            _admin.CreateLayout (tenantId, layoutModel);
            return layoutModel.Id;
        }

        Guid ILayoutManager.CreatePicklistLayout (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId) {
            layoutModel.Id = Guid.NewGuid ();
            layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, true);
            layoutModel.ModifiedBy = userId;
            //layoutModel.Subtype = _iMetadataManager.GetSubTypeId(layoutModel.SubtypeeName);

            _admin.CreatePicklistLayout (tenantId, layoutModel);
            return layoutModel.Id;
        }

        Guid ILayoutManager.CreatePicklistLayout (LayoutModel layoutModel, Guid userId, Guid tenantId) {
            layoutModel.Id = Guid.NewGuid ();
            layoutModel.ModifiedBy = userId;
            _admin.CreatePicklistLayout (tenantId, layoutModel);
            return layoutModel.Id;
        }

        void ILayoutManager.SetPicklistLayoutDefault (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId) {

            layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, true);
            layoutModel.ModifiedBy = userId;
            _admin.SetPicklistLayoutDefault (tenantId, layoutModel);

        }

        void ILayoutManager.SetListLayoutDefault (string entityName, LayoutModel layoutModel, Guid userId, Guid tenantId) {

            layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            layoutModel.ModifiedBy = userId;
            _admin.SetListLayoutDefault (tenantId, layoutModel);

        }

        void ILayoutManager.DeletePicklistLayout (Guid tenantId, Guid layoutId) {
            _admin.DeletePicklistLayout (tenantId, layoutId);
        }

        void ILayoutManager.DeleteListLayout (Guid tenantId, Guid layoutId) {
            _admin.DeleteListLayout (tenantId, layoutId);
        }
     void ILayoutManager.UpdateLayoutDetails (Guid tenantId, Guid layoutId, LayoutModel templateModel) {
            _admin.UpdateLayoutDetails (tenantId, layoutId, templateModel);
        }
        void ILayoutManager.UpdateLayoutDetailsXml (Guid tenantId, List<LayoutModel> templateModel) {
            _admin.UpdateLayoutDetailsXml (tenantId, templateModel);
        }

        List<LayoutModel> ILayoutManager.GetLayoutsByEntityName (Guid tenantId, string entityName) {
            var entityContext = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            if (string.IsNullOrEmpty (entityContext)) return null;
            //return _review.GetLayoutsByEntityName (tenantId, entityContext).OrderBy (x => x.Name).ToList ();

            var subTypes = _iMetadataManager.GetSubTypesDetails (entityName);
            var layouts = new List<LayoutModel> ();
            layouts = _review.GetLayoutsByEntityName (tenantId, entityContext).OrderBy (x => x.Name).ToList ();
            if (layouts != null && layouts.Count > 0) {
                for (int i = 0; i < layouts.Count; i++) {
                    if (layouts[i].Subtype != null && layouts[i].Subtype != "") {
                        if (subTypes != null) {
                            foreach (KeyValuePair<string, string> item in subTypes) {
                                if (item.Key == layouts[i].Subtype) {
                                    layouts[i].SubtypeeName = item.Value;
                                }
                            }
                        }
                    }
                }
            }
            return layouts;
        }

        List<LayoutModel> ILayoutManager.GetLayoutsByEntityContext (Guid tenantId, string entityContext) {
            return _review.GetLayoutsByEntityName (tenantId, entityContext).OrderBy (x => x.Name).ToList ();
        }

        List<LayoutModel> ILayoutManager.GetLayoutsByEntityName (Guid tenantId, string entityContext, int type, bool isPicklist) {
            return _review.GetLayoutsByEntityName (tenantId, entityContext, type, isPicklist).OrderBy (x => x.Name).ToList ();
        }

        List<LayoutModel> ILayoutManager.GetLayoutsByPicklistName (Guid tenantId, string picklistName) {
            var picklistId = _iMetadataManager.GetEntityContextByEntityName (picklistName, true);
            if (string.IsNullOrEmpty (picklistId)) return null;
            return _review.GetLayoutsByPicklistId (tenantId, picklistId);
        }

        List<LayoutModel> ILayoutManager.GetPicklistLayout (Guid tenantId, string picklistId) {
            return _review.GetLayoutsByPicklistId (tenantId, picklistId);
        }

        LayoutModel ILayoutManager.GetLayoutsDetailsById (Guid tenantId, Guid id) {
            //return _review.GetLayoutsDetailsById(tenantId, id);

            var layout = _review.GetLayoutsDetailsById (tenantId, id);

            if (layout != null && layout.EntityId != null && layout.Subtype != null) {
                var entityName = _iMetadataManager.GetEntityNameByEntityContext (layout.EntityId);
                var subTypes = _iMetadataManager.GetSubTypesDetails (entityName);

                foreach (KeyValuePair<string, string> item in subTypes) {
                    if (item.Key == layout.Subtype) {
                        layout.SubtypeeName = item.Value;
                    }
                }
            }

            MapLayoutDetails (tenantId, layout);

            return layout;
        }

        //LayoutModel ILayoutManager.GetLayoutsDetailsById(Guid tenantId, string entityName, string type, string subtype, string context)
        //{
        //    return _review.GetLayoutsDetailsById(tenantId, _iMetadataManager.GetEntityContextByEntityName(entityName), _iMetadataManager.GetTypeId(type), _iMetadataManager.GetSubTypeId(subtype), _iMetadataManager.GetContextId(context));
        //}

        LayoutModel ILayoutManager.GetPicklistLayoutDetailsById (Guid tenantId, Guid id) {
            var layout = _review.GetPicklistLayoutDetailsById (tenantId, id);
            MapLayoutDetails (tenantId, layout);
            return layout;
        }

        void ILayoutManager.UpdatePicklistLayout (Guid tenantId, Guid layoutId, LayoutModel layout) {
            _admin.UpdatePicklistLayout (tenantId, layoutId, layout);
        }

        LayoutModel ILayoutManager.GetDefaultPicklistLayout (Guid tenantId, string entityName, LayoutType layoutType, int context) {
            return GetDefaultPicklistLayout (tenantId, entityName, layoutType, context);
        }

        private LayoutModel GetDefaultPicklistLayout (Guid tenantId, string entityName, LayoutType layoutType, int context) {
            var entityContext = _iMetadataManager.GetEntityContextByEntityName (entityName, true);
            var layout = _review.GetDefaultPicklistLayout (tenantId, entityContext, layoutType, context);

            if (layout != null) {
                MapLayoutDetails (tenantId, layout);
                if (layoutType.Equals (LayoutType.List) && layout.ListLayoutDetails != null && layout.ListLayoutDetails.Fields != null && layout.ListLayoutDetails.Fields.Any ()) {

                    var addedFields = new List<SelectedItem> ();
                    var removeFields = new List<SelectedItem> ();

                    foreach (var field in layout.ListLayoutDetails.Fields) {
                        if (field.DataType.ToLower ().Equals ("picklist") && !string.IsNullOrEmpty (field.DefaultView)) {
                            field.Hidden = true;
                            var splitPicklist = field.Name.Split ('.');
                            var picklistName = splitPicklist[splitPicklist.Count () - 1];
                            var view = GetDefaultPicklistLayout (tenantId, picklistName, LayoutType.View, 0);
                            //it should be view layout...
                            if (view != null && view.ViewLayoutDetails != null && view.ViewLayoutDetails.Fields != null && view.ViewLayoutDetails.Fields.Any ()) {
                                //addedFields = view.ViewLayoutDetails.Fields;
                                foreach (var item in view.ViewLayoutDetails.Fields) {
                                    item.Name = picklistName + "." + item.Name;
                                    addedFields.Add (item);
                                }
                            }
                            removeFields.Add (field);
                        }
                    }
                    if (addedFields.Any ()) {
                        layout.ListLayoutDetails.Fields.AddRange (addedFields);
                    }

                }

                //append plural name
                var result = _iMetadataManager.GetEntitityByName(entityName);
                if(result != null && result.PluralName != null)
                {
                    layout.PluralName = result.PluralName;
                }

                //append singular name               
                if(result != null && result.DisplayName != null)
                {
                    layout.SingularName = result.DisplayName;
                }
            }

            return layout;
        }
        private void MapLayoutDetails (Guid tenantId, LayoutModel layoutObj) {
            if (layoutObj != null && !string.IsNullOrEmpty (layoutObj.Layout)) {
                var settings = new JsonSerializerSettings ();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var result = JsonConvert.DeserializeObject<ListLayoutDetails> (layoutObj.Layout);

                if (layoutObj.LayoutType == LayoutType.List) {
                    layoutObj.ListLayoutDetails = new ListLayoutDetails ();
                    layoutObj.ListLayoutDetails.Fields = new List<SelectedItem> ();

                    if (result != null) {
                        layoutObj.ListLayoutDetails.Fields = result.Fields;

                        // need to add picklist view.....
                        layoutObj.ListLayoutDetails.DefaultSortOrder = result.DefaultSortOrder;
                        layoutObj.ListLayoutDetails.MaxResult = result.MaxResult;
                        layoutObj.ListLayoutDetails.SearchProperties = result.SearchProperties;
                        layoutObj.ListLayoutDetails.Toolbar = result.Toolbar;
                        layoutObj.ListLayoutDetails.Actions = result.Actions;
                        layoutObj.ListLayoutDetails.DefaultGroupBy = result.DefaultGroupBy;

                        if (layoutObj.ListLayoutDetails.DefaultSortOrder == null) {
                            layoutObj.ListLayoutDetails.DefaultSortOrder = new OrderDetails ();
                            layoutObj.ListLayoutDetails.DefaultSortOrder.Name = string.Empty;
                            layoutObj.ListLayoutDetails.DefaultSortOrder.Value = string.Empty;
                        }
                        if (layoutObj.ListLayoutDetails.MaxResult == 0) {
                            layoutObj.ListLayoutDetails.MaxResult = 500;
                        }
                        if (layoutObj.ListLayoutDetails.Actions == null) {
                            //layout.ListLayoutDetails.Actions = _iEntityManager.GetRowLevelActionsByEntityName(TenantId, entityName);
                        }
                        if (layoutObj.ListLayoutDetails.SearchProperties == null) {
                            //layout.ListLayoutDetails.SearchProperties = _iEntityManager.SearchProperties(TenantId, entityName);
                        }
                        // addPicklistViewFields(tenantId, layout.ListLayoutDetails.Fields);
                    }
                } else if (layoutObj.LayoutType == LayoutType.View) {
                    layoutObj.ViewLayoutDetails = new ViewLayoutDetails ();
                    layoutObj.ViewLayoutDetails.Fields = new List<SelectedItem> ();
                    if (result != null) {
                        layoutObj.ViewLayoutDetails.Fields = result.Fields;
                        layoutObj.ViewLayoutDetails.DefaultSortOrder = result.DefaultSortOrder;
                        layoutObj.ViewLayoutDetails.Actions = result.Actions;
                    }
                } else if (layoutObj.LayoutType == LayoutType.Form) {
                    var formDetails = JsonConvert.DeserializeObject<FormLayoutDetails> (layoutObj.Layout);
                    layoutObj.FormLayoutDetails = new FormLayoutDetails ();
                    layoutObj.FormLayoutDetails = formDetails;
                }
                layoutObj.Layout = string.Empty;
            }
        }

        List<string> ILayoutManager.GetDesignFieldsFromDefaultLayoutForPickList (Guid tenantId, string name, LayoutType type, int context) {
            var defaultLayout = GetDefaultPicklistLayout (tenantId, name, type, (int) context);
            return GetFieldNameFromLayout (defaultLayout, type);
        }

        internal static List<string> GetFieldNameFromLayout (LayoutModel defaultLayout, LayoutType type) {
            switch (type) {
                case LayoutType.Form:
                    return FormList (defaultLayout.FormLayoutDetails);
                case LayoutType.List:
                    Console.WriteLine ("Case 2");
                    break;
                default:
                    Console.WriteLine ("Default case");
                    break;
            }
            return null;
        }

        private static List<string> FormList (FormLayoutDetails formLayoutDetails) {
            var nameList = new List<string> ();
            if (formLayoutDetails != null) {
                nameList.Add (formLayoutDetails.Name);
                if (formLayoutDetails.Fields != null && formLayoutDetails.Fields.Any ()) {
                    GetChild (formLayoutDetails.Fields, nameList);
                }
                if (formLayoutDetails.Tabs != null && formLayoutDetails.Tabs.Any ()) {
                    GetChild (formLayoutDetails.Tabs, nameList);
                }
            }
            return nameList;
        }

        private static void GetChild (List<FieldModel> fields, List<string> nameList) {
            if (fields != null && fields.Any ()) {
                foreach (var item in fields) {
                    nameList.Add (item.Name);
                    GetChild (item.Fields, nameList);
                    GetChild (item.Tabs, nameList);
                }
            }
        }

        List<string> ILayoutManager.GetDesignFieldsFromDefaultLayoutForEntity (Guid tenantId, string name, LayoutType type, string subType, int context) {
            var defaultLayout = GetDefaultLayoutForEntity (tenantId, name, (int) type, subType, (int) context);
            return GetFieldNameFromLayout (defaultLayout, type);
        }

        private Dictionary<string, string> GetSelectedViewIdsForDetailEntity (LayoutModel defaultLayout) {
            var myList = new Dictionary<string, string> ();
            if (defaultLayout != null && defaultLayout.FormLayoutDetails != null && defaultLayout.FormLayoutDetails.Fields != null && defaultLayout.FormLayoutDetails.Fields.Any ()) {
                MapSelectedViewIdsForDetailEntity (myList, defaultLayout.FormLayoutDetails.Fields);
            }
            return myList;
        }

        private void MapSelectedViewIdsForDetailEntity (Dictionary<string, string> result, List<FieldModel> fields) {
            if (fields != null && fields.Any ()) {
                foreach (var item in fields) {
                    if (item.ControlType.ToLower ().Equals ("custom") && !string.IsNullOrEmpty (item.SelectedView)) {
                        result.Add (item.Name, item.SelectedView);
                    }
                    if (item.Fields.Any ()) {
                        MapSelectedViewIdsForDetailEntity (result, item.Fields);
                    }
                    if (item.Tabs.Any ()) {
                        MapSelectedViewIdsForDetailEntity (result, item.Tabs);
                    }
                }
            }
        }

        Dictionary<string, string> ILayoutManager.GetDetailEntitiesFromDefaultLayoutForEntity (Guid tenantId, string entityName, int layoutType, string subType, int context) {
            var defaultLayout = GetDefaultLayoutForEntity (tenantId, entityName, (int) LayoutType.Form, subType, context);
            return GetSelectedViewIdsForDetailEntity (defaultLayout);
        }

        Guid ILayoutManager.GetTenantId (string entityName, string code) {
            //var entityContext = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            return _review.GetTenantId (entityName, code);
        }

        List<RoleInfo> ILayoutManager.GetUserRoles (Guid userId) {
            return _review.GetUserRoles (userId);
        }

        List<FieldModel> ILayoutManager.GetComputedFields (Guid tenantId, string name, LayoutType type, string subtype, int context) {
            var defaultLayout = GetDefaultLayoutForEntity (tenantId, name, (int) type, subtype, (int) context);
            return ComputedList (defaultLayout.FormLayoutDetails);
        }

        private static List<FieldModel> ComputedList (FormLayoutDetails formLayoutDetails) {
            var nameList = new List<FieldModel> ();
            if (formLayoutDetails != null) {
                if (formLayoutDetails.Fields != null && formLayoutDetails.Fields.Any ()) {
                    GetComputedChild (formLayoutDetails.Fields, nameList);
                }
                if (formLayoutDetails.Tabs != null && formLayoutDetails.Tabs.Any ()) {
                    GetComputedChild (formLayoutDetails.Tabs, nameList);
                }
            }
            return nameList;
        }

        private static void GetComputedChild (List<FieldModel> fields, List<FieldModel> nameList) {
            if (fields != null && fields.Any ()) {
                foreach (var item in fields) {
                    if (
                        (item.TypeOf != null && item.TypeOf.ToLower ().Equals ("computedtype") && item.ReceivingTypes != null && (item.ReceivingTypes.Any ())) ||
                        (item.BroadcastingTypes != null && item.BroadcastingTypes.Any ())

                    ) {

                        nameList.Add (item);

                    }
                    // if(item.TypeOf.ToLower ().Equals ("computedtype") && (item.ReceivingTypes!=null) && (item.ReceivingTypes.Any())){
                    //     var test = "dd";
                    // }
                    // if (
                    //     ((item.TypeOf != null && item.TypeOf.ToLower ().Equals ("computedtype")) && 
                    //     (item.ReceivingTypes.Any()) 
                    //     && (item.ReceiverDataTypes.Any())) ||
                    //     (item.BroadcastingTypes.Any())

                    // ) {
                    //     nameList.Add (item);
                    // }
                    GetComputedChild (item.Fields, nameList);
                    GetComputedChild (item.Tabs, nameList);
                }
            }
        }

        Guid ILayoutManager.CloneLayout (string entityName, Guid layoutId, LayoutModel layoutModel, Guid userId, Guid tenantId) {
            //get old layout
            var layout = _review.GetLayoutsDetailsById (tenantId, layoutId);

            layoutModel.Id = Guid.NewGuid ();
            if(layout != null && layout.EntityId != null && layout.LayoutType > 0 && layout.Layout != null)
            {
                layoutModel.EntityId = layout.EntityId;
                layoutModel.LayoutType = layout.LayoutType;
                //layoutModel.Layout = layout.Layout;
            }                
            else
            {
                layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            }    
          
            //filter old layout
            FilterSourceLayout(layout, entityName, layoutModel);

            layoutModel.ModifiedBy = userId;          
            layoutModel.Subtype = _iMetadataManager.GetSubTypeId (entityName, layoutModel.SubtypeeName);

            _admin.CreateLayout (tenantId, layoutModel);
            return layoutModel.Id;            
        }

         Guid ILayoutManager.ClonePicklistLayout (string entityName, Guid layoutId, LayoutModel layoutModel, Guid userId, Guid tenantId) {
            //get old layout
            var layout = _review.GetPicklistLayoutDetailsById (tenantId, layoutId);

            layoutModel.Id = Guid.NewGuid ();
            if(layout != null && layout.EntityId != null && layout.LayoutType > 0 && layout.Layout != null)
            {
                layoutModel.EntityId = layout.EntityId;
                layoutModel.LayoutType = layout.LayoutType;
                //layoutModel.Layout = layout.Layout;
            }
            else
            {
                layoutModel.EntityId = _iMetadataManager.GetEntityContextByEntityName (entityName, false);
            }    

            //filter old layout
            FilterSourceLayout(layout, entityName, layoutModel);       

            layoutModel.ModifiedBy = userId;

            _admin.CreatePicklistLayout (tenantId, layoutModel);
            return layoutModel.Id;
        }

        bool RemoveInAccessibleFields (List<FieldModel> fields, string fieldName) {

            foreach (var item in fields) {
                switch (item.ControlType.ToLower()) {
                    // case "Section":
                    //     checkFieldAccesscibility (item.Fields);
                    //     break;
                    case "tabs":
                        RemoveInAccessibleFields (item.Tabs, fieldName);
                        break;
                    case "tab":
                        RemoveInAccessibleFields (item.Fields, fieldName);
                        break;
                    case "section":
                        RemoveInAccessibleFields (item.Fields, fieldName);
                        break;
                    case "custom":
                        RemoveInAccessibleFields (item.Fields, fieldName);
                        break;
                    default:
                        if(item.Name.ToLower() == fieldName.ToLower())
                        {
                            fields.Remove(item);
                            return true;
                        }                        
                        break;
                }
                
            }

            return false;

        }

        void FilterSourceLayout(LayoutModel sourceLayout, string entityName, LayoutModel targetLayout)
        {
            if(sourceLayout != null && sourceLayout.Layout != null && sourceLayout.LayoutType == LayoutType.Form)
            {
                //clone layout fields
                var sourceDeserializedLayout = JsonConvert.DeserializeObject<FormLayoutDetails>(sourceLayout.Layout);
                List<string> sourceFieldList = FormList(sourceDeserializedLayout);

                //entity fields to clean
                var entityDetails = _iMetadataManager.GetEntitityByName(entityName).Fields.Where(a => a.AccessibleLayoutTypes != null);
                //new clone context
                int cloneContext = (int)targetLayout.Context;
                List<string> removableFieldList = new List<string>();

                //check in layout fields
                BuildRemovableFields(sourceFieldList, entityDetails, cloneContext, removableFieldList);

                //copy to variable before cleaning
                //var modifiedDetails = sourceDeserializedLayout;
                if (removableFieldList != null && removableFieldList.Count > 0)
                {
                    foreach (var item in removableFieldList)
                    {
                        RemoveInAccessibleFields(sourceDeserializedLayout.Fields, item);
                    }
                }

                if (sourceDeserializedLayout.Fields.Count > 0)
                {
                    targetLayout.Layout = JsonConvert.SerializeObject(sourceDeserializedLayout, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
            }
            else
            {
                targetLayout.Layout = sourceLayout.Layout;
            }
        }

        private static void BuildRemovableFields(List<string> sourceFieldList, IEnumerable<FieldModel> entityDetails, int cloneContext, List<string> removableFieldList)
        {
            if (sourceFieldList != null && sourceFieldList.Count > 0 && entityDetails != null)
            {
                foreach (var element in sourceFieldList)
                {
                    foreach (var item in entityDetails)
                    {
                        if (element.ToLower() == item.Name.ToLower())
                        {
                            if (item.AccessibleLayoutTypes != null && item.AccessibleLayoutTypes.Count > 0)
                            {
                                //check whether both 21,22 is present
                                bool doubleFormField = false;
                                //check whether field is accessible in form
                                bool formAccessibleField = false;
                                int i = 0;
                                foreach (var data in item.AccessibleLayoutTypes)
                                {
                                    if ((int)data.ToString().Length > 1)
                                    {
                                        i += 1;
                                        formAccessibleField = true;
                                    }
                                    else if ((int)data == (int)LayoutType.Form)
                                    {
                                        formAccessibleField = true;
                                    }
                                }

                                if (i == 2)
                                {
                                    doubleFormField = true;
                                }
                                else
                                {
                                    doubleFormField = false;
                                }

                                foreach (var data in item.AccessibleLayoutTypes)
                                {
                                    //handle 21, 22
                                    if ((int)data.ToString().Length > 1 && doubleFormField == false)
                                    {
                                        int firstDigit = (int)data / 10;
                                        int secondDigit = (int)data % 10;

                                        if (cloneContext != secondDigit)
                                        {
                                            removableFieldList.Add(item.Name);
                                        }
                                    }
                                    else if (formAccessibleField == false)
                                    {
                                        removableFieldList.Add(item.Name);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}