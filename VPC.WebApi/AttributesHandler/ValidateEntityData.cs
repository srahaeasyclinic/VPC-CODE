using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.AttributesHandler
{
    public class ValidateEntityJsonObjData : ActionFilterAttribute, IActionFilter
    {

        #region Ctor_and_PrivateVariable
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IValidationService _IValidationService;
        private readonly ILayoutManager _iILayoutManager;
        private readonly IMetadataManager _iMetadataManager;

        //Guid tenentId
        public ValidateEntityJsonObjData()
        {

            this._iILayoutManager = new LayoutManager();
            this._iMetadataManager = new MetadataManager();
            this._IValidationService = new ValidationService();
        }
        #endregion

        #region ActionFilterMethods_Override
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //_log.Info("Called ValidateJsonObjData GetEntityDetails with entityName {0}=", entityName);
            _log.Info("Called ValidateJsonObjData Action filter");

            var baseApiController = ((BaseApiController)context.Controller);
            Guid tenentId = Guid.Empty;
            string methodType = context.HttpContext.Request.Method;
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
                context.ActionArguments.TryGetValue("entityName", out object entityName);
                context.ActionArguments.TryGetValue("subType", out object subType);

                if (jobjectvalue == null || string.IsNullOrEmpty(Convert.ToString(entityName)) || string.IsNullOrEmpty(Convert.ToString(subType)))
                {
                    //throw new Exception("Bad request!");
                    context.ModelState.AddModelError("Parameter", "Bad request!");
                    context.Result = baseApiController.StatusCode(520, "Bad request!");
                    return;
                }

                var objsondict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jobjectvalue.ToString());
                if (objsondict == null || objsondict.Count == 0)
                {
                    context.ModelState.AddModelError("ValidateJsonObjData", string.Format("Jobject value is null in Validation Attributes filter!"));
                    context.Result = baseApiController.StatusCode(520, "Empty data is not acceptable.");
                    return;
                }

                #region EntityFields Validation
                string errors1 = string.Empty;
                string errors2 = string.Empty;

                Entity objentity = _iMetadataManager.GetEntitityByNameExceptsomeFields(Convert.ToString(entityName), false);
                objentity.Fields = objentity.Fields.Where(w => w.ControlType.ToLower() != "label" && w.AccessibleLayoutTypes != null && w.AccessibleLayoutTypes.Contains((int)LayoutType.Form)).ToList();

                if (objentity.Fields != null && objentity.Fields.Count > 0)
                {
                    errors1 = Fieldsloops(objentity.Fields, objsondict, ref context, ref baseApiController);
                }

                //foreach (var field in objentity.Fields)
                //{
                //    errors1 = string.Empty;
                //    errors2 = string.Empty;
                //    // checking Jobject with Entity object to validate Required field

                //    errors1 = _IValidationService.Requiredfieldandvaluecheck(field, objsondict, ref context);

                //    //End Required field Validation

                //    //Field's dataType and its value format check
                //    errors2 = _IValidationService.FieldsdataTypevaluecheck(field, objsondict, ref context);

                //    //End

                //    if (!context.ModelState.IsValid)
                //    {

                //        context.Result = baseApiController.StatusCode(520, (errors1 + "," + errors2).Replace(",", Environment.NewLine));
                //        return;

                //    }
                //}

                //Activity Entityfield validation
                //need to fix this portion
                if (objentity != null && objentity.ActivityEntity != null && objentity.ActivityEntity.Fields != null && objentity.ActivityEntity.Fields.Count > 0 && !methodType.ToUpper().Equals("PUT"))
                {
                    //var activityfields = (!methodType.ToUpper().Equals("PUT")) ? objentity.ActivityEntity.Fields.Where(w => w.ControlType.ToLower() != "label").ToList()
                    //                                                            : objentity.ActivityEntity.Fields.Where(w => w.ControlType.ToLower() != "label" && w.AccessibleLayoutTypes != null && w.AccessibleLayoutTypes.Contains((int)LayoutType.Form)).ToList();
                    var activityfields = objentity.ActivityEntity.Fields.Where(w => w.ControlType.ToLower() != "label" && w.AccessibleLayoutTypes != null && w.AccessibleLayoutTypes.Contains((int)LayoutType.Form)).ToList();
                    errors2 = Fieldsloops(activityfields, objsondict, ref context, ref baseApiController);
                }
                if (!context.ModelState.IsValid)
                {
                    context.Result = baseApiController.StatusCode(520, (errors1 + "," + errors2).Replace(",", Environment.NewLine));
                    return;
                }
                #endregion

                #region Layoutfields Validation

                FormLayoutDetails LayoutField = null;

                if (methodType.ToUpper().Equals("POST"))
                {
                    LayoutField = _iILayoutManager.GetDefaultLayoutForEntity(tenentId, Convert.ToString(entityName), 2, Convert.ToString(subType), 1)?.FormLayoutDetails;
                }
                if (methodType.ToUpper().Equals("PUT"))
                {
                    LayoutField = _iILayoutManager.GetDefaultLayoutForEntity(tenentId, Convert.ToString(entityName), 2, Convert.ToString(subType), 2)?.FormLayoutDetails;
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

            // context.ModelState.AddModelError("Parameter", "The request body cannot be null");
            // context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ModelState);

            // if (context.ActionArguments.TryGetValue("entityName", out object value)&&context.ActionArguments.TryGetValue("value", out object value))
            // {
            //     // NOTE: this assumes all your controllers derive from Controller.
            //     // If they don't, you'll need to set the value in OnActionExecuted instead
            //     // or use an IAsyncActionFilter
            //     // if (context.Controller is Controller controller)
            //     // {
            //     //     controller.ViewData["ReturnUrl"] = value.ToString();
            //     // }
            // }
        }

        public override void OnActionExecuted(ActionExecutedContext context) { }
        #endregion

        private string Fieldsloops(List<FieldModel> fields, Dictionary<string, object> objson, ref ActionExecutingContext context, ref BaseApiController baseApiController)
        {
            string errors1 = string.Empty;
            string errors2 = string.Empty;

            foreach (var field in fields)
            {
                // checking Jobject with Entity object to validate Required field
                errors1 = string.Empty;
                errors2 = string.Empty;

                errors1 = _IValidationService.Requiredfieldandvaluecheck(field, objson, ref context);

                //End Required field Validation

                //Field's dataType and its value format check
                errors2 = _IValidationService.FieldsdataTypevaluecheck(field, objson, ref context);

                //End

                if (!context.ModelState.IsValid)
                {
                    context.Result = baseApiController.StatusCode(520, (errors1 + "," + errors2).Replace(",", Environment.NewLine));
                    return (errors1 + "," + errors2);
                }

            }
            return string.Empty;
        }
    }
}