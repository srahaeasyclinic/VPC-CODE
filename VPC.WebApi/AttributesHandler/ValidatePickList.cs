using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.AttributesHandler
{
    public class ValidatePickList : ActionFilterAttribute, IActionFilter
    {
        #region Ctor_and_PrivateVariable
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IMetadataManager _iMetadataManager;
        private readonly IPicklistManager _picklistManager;
        private readonly IValidationService _IValidationService;
        private readonly ILayoutManager _iILayoutManager;
        private List<string> SkipFields = new List<string>()
        {
            "Flagged",
            "IsDeletetd",
             "Active",
            "UpdatedDate",
             "UpdatedBy",
            "InternalId",
            "TenantId"
        };

        //Guid tenentId
        public ValidatePickList()
        {
            this._picklistManager = new PicklistManager();
            this._IValidationService = new ValidationService();
            this._iMetadataManager = new MetadataManager();
            this._iILayoutManager = new LayoutManager();
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _log.Info("Called ValidateJsonObjData Action filter");

            var baseApiController = ((BaseApiController)context.Controller);
            Guid tenentId = Guid.Empty;

            if (baseApiController != null)
            {
                tenentId = new Guid(Convert.ToString(baseApiController.TenantCode));
            }

            if (tenentId == Guid.Empty)
            {
                context.ModelState.AddModelError("Parameter", "Bad request!");
                context.Result = baseApiController.StatusCode(520, "Bad request!");
                return;
            }

            try
            {

                context.ActionArguments.TryGetValue("value", out object jobjectvalue);
                context.ActionArguments.TryGetValue("name", out object picklistName);


                if (jobjectvalue == null || string.IsNullOrEmpty(Convert.ToString(picklistName)))
                {
                    //throw new Exception("Bad request!");
                    context.ModelState.AddModelError("Parameter", "Bad request!");
                    context.Result = baseApiController.StatusCode(520, "Bad request!");
                    return;
                }

                var objsondict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jobjectvalue.ToString());
                if (objsondict == null || objsondict.Count == 0)
                {
                    context.ModelState.AddModelError("ValidateJsonObjData", string.Format("Null value is not acceptable."));
                    context.Result = baseApiController.StatusCode(520, "Empty data is not acceptable.");
                    return;
                }

                string errors1 = string.Empty;
                string errors2 = string.Empty;

                Entity objentity = _iMetadataManager.GetEntitityByNameExceptsomeFields(Convert.ToString(picklistName), false);
                objentity.Fields = objentity.Fields.Where(w => w.ControlType.ToLower() != "label" && !SkipFields.Contains(w.Name) && w.AccessibleLayoutTypes != null && w.AccessibleLayoutTypes.Contains((int)LayoutType.Form)).ToList();
                foreach (var field in objentity.Fields)
                {
                    // checking Jobject with Entity object to validate Required field
                    errors1 = string.Empty;
                    errors2 = string.Empty;

                    errors1 = _IValidationService.Requiredfieldandvaluecheck(field, objsondict, ref context);

                    //End Required field Validation

                    //Field's dataType and its value format check
                    errors2 = _IValidationService.FieldsdataTypevaluecheck(field, objsondict, ref context);

                    //End

                    if (!context.ModelState.IsValid)
                    {
                        context.Result = baseApiController.StatusCode(520, (errors1 + "," + errors2).Replace(",", Environment.NewLine));
                        return;
                    }

                }

                #region Layoutfields Validation

                FormLayoutDetails LayoutField = null;
                string methodType = context.HttpContext.Request.Method;

                if (methodType.ToUpper().Equals("POST"))
                {
                    LayoutField = _iILayoutManager.GetDefaultPicklistLayout(tenentId, Convert.ToString(picklistName), LayoutType.Form, 1)?.FormLayoutDetails;
                }
                if (methodType.ToUpper().Equals("PUT"))
                {
                    LayoutField = _iILayoutManager.GetDefaultPicklistLayout(tenentId, Convert.ToString(picklistName), LayoutType.Form, 2)?.FormLayoutDetails;
                }

                errors1 = string.Empty;
                errors2 = string.Empty;

                if (LayoutField != null)
                {


                    errors1 = _IValidationService.Requiredfieldandvaluecheck(LayoutField, objsondict, ref context);

                    //End Required field Validation

                    //Field's dataType and its value format check
                    errors2 = _IValidationService.FieldsdataTypevaluecheck(LayoutField, objsondict, ref context);

                    //End
                }
                if (!context.ModelState.IsValid)
                {
                    context.Result = baseApiController.StatusCode(520, (errors1 + "," + errors2).Replace(",", Environment.NewLine));
                    return;
                }

                #endregion
            }
            catch (Exception ex)
            {
                _log.Info("Exception in the ValidateJsonObjData Action filter= {0}", ex.Message);
                //throw new Exception(string.Format("Exception due to {0}", ex.Message));
                context.ModelState.AddModelError("Parameter", "The request body cannot be null");
                context.Result = baseApiController.StatusCode(520, ex.Message);
            }
        }
    }
}
