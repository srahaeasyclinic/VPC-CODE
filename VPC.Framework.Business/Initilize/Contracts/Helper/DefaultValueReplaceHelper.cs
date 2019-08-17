using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.Initilize.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.Initilize.Contracts.Helper
{
    internal class DefaultValueReplaceHelper
    {
        private readonly IInitilizeReview _review;

        public DefaultValueReplaceHelper()
        {
            _review = new InitilizeReview();
        }

        public void Execute(Guid rootTenantId, Guid initilizedTenantCode)
        {
            var layouts = _review.GetAllEntityAndPickListFormLayoutsByTenantId(initilizedTenantCode);

            var updatedList = new List<LayoutModel>();
            if (layouts.Any())
            {
                foreach (var item in layouts)
                {
                    //every results should be form layout.
                    var formDetails = JsonConvert.DeserializeObject<FormLayoutDetails>(item.Layout);
                    if (formDetails != null && formDetails.Fields != null && formDetails.Fields.Any())
                    {
                        FormLayoutDetails cloneForm = formDetails;
                        if (cloneForm.Fields.Any())
                        {
                            UpdateDefaultValue(cloneForm.Fields, rootTenantId, initilizedTenantCode, item.TypeId);
                            item.FormLayoutDetails = cloneForm;
                            updatedList.Add(item);
                        }
                    }
                }
            }

            // foreach (var list in updatedList) {
            //     ILayoutManager _iLayoutManager = new LayoutManager ();
            //     _iLayoutManager.UpdateLayoutDetails (initilizedTenantCode, list.Id, list);
            // }

            if (!updatedList.Any()) return;
            ILayoutManager _iLayoutManager = new LayoutManager();
            _iLayoutManager.UpdateLayoutDetailsXml(initilizedTenantCode, updatedList);
        }

        private void  UpdateDefaultValue(List<FieldModel> fields, Guid rootTenantId, Guid initilizedTenantCode, string typeId)
        {
            foreach (var field in fields)
            {
                if (field.Validators != null && field.Validators.Any())
                {                  
                    foreach (var validator in field.Validators)
                    {
                        if (validator.DefaultValue != null)
                        {
                            GetValueAfterCloning(field, rootTenantId, initilizedTenantCode, validator, typeId);
                        }
                    }
                }
            }
        }

        private void GetValueAfterCloning(FieldModel field, Guid rootTenantId, Guid intialisedTenantId, Validator validator, string typeId)
        {
            if (field != null)
            {
                IMetadataManager _imetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
                var fieldContext = _imetadataManager.GetEntityContextByEntityName(field.TypeOf,field.DataType.ToLower()=="picklist"?true:false);
                // if (!string.IsNullOrEmpty(fieldContext))
                // {
                //     var newId = (field.DataType.ToString().ToLower().Equals(VPC.Metadata.Business.DataAnnotations.DataType.PickList.ToString().ToLower())) ? GetPickListValue(field, rootTenantId, intialisedTenantId, validator.DefaultValue, fieldContext) : GetEntityValue(field, rootTenantId, intialisedTenantId, validator.DefaultValue, fieldContext);
                //     validator.DefaultValue = newId;
                // }

                dynamic newlyCreatedId = null;
                if (field.DataType.ToString().ToLower().Equals(VPC.Metadata.Business.DataAnnotations.DataType.PickList.ToString().ToLower()))
                {
                    newlyCreatedId = GetPickListValue(field, rootTenantId, intialisedTenantId, validator.DefaultValue, fieldContext);
                    if (newlyCreatedId != Guid.Empty)
                    {
                        validator.DefaultValue = newlyCreatedId;
                    }

                }
                else if (field.DataType.ToString().ToLower().Equals(VPC.Metadata.Business.DataAnnotations.DataType.Lookup.ToString().ToLower()))
                {
                    newlyCreatedId = GetEntityValue(field, rootTenantId, intialisedTenantId, validator.DefaultValue, fieldContext);
                    if (newlyCreatedId != Guid.Empty)
                    {
                        validator.DefaultValue = newlyCreatedId;
                    }
                }

            }
        }

        private Guid GetEntityValue(FieldModel field, Guid rootTenantId, Guid intialisedTenantId, dynamic defaultValue, string typeId)
        {
            return _review.GetNewlyCreatedEntityId(rootTenantId, defaultValue, intialisedTenantId, typeId);
        }

        private Guid GetPickListValue(FieldModel field, Guid rootTenantId, Guid intialisedTenantId, dynamic defaultValue, string typeId)
        {
            return _review.GetNewlyCreatedPickListId(rootTenantId, defaultValue, intialisedTenantId, typeId);
        }

    }
}