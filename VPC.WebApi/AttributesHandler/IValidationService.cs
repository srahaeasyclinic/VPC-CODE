using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;

namespace VPC.WebApi.AttributesHandler
{
    public interface IValidationService
    {
        string Requiredfieldandvaluecheck(FieldModel objent, Dictionary<string, object> objson, ref ActionExecutingContext context);
        string FieldsdataTypevaluecheck(FieldModel objent, Dictionary<string, object> objson, ref ActionExecutingContext context);
    }
    public sealed class ValidationService : IValidationService
    {
        private readonly IValidationManager _iValidationManager;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        //private readonly IResourceManager _resourceManager;
      
        public ValidationService()
        {
            this._iValidationManager = new ValidationManager();
            //this._resourceManager = new ResourceManager();
        }
        public string FieldsdataTypevaluecheck(FieldModel objent, Dictionary<string, object> objson, ref ActionExecutingContext context)
        {
            _log.Info("Called ValidateJsonObjData's RequiredFieldCheck");

            //var objsondict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objson.ToString());
            //if (objent.ControlType.ToLower() != "section" && objent.ControlType.ToLower() != "tabs" && !Skipfields.Contains(objent.Name.ToLower()))
            if (objent.ControlType.ToLower() != "section" && objent.ControlType.ToLower() != "tabs")
            {
                if (objson.ContainsKey(objent.Name))
                {

                    var Keyvalue = Convert.ToString(objson[objent.Name]);

                    var Isvalid = _iValidationManager.Entityfieldsvalidation(objent, Keyvalue);
                    if (!Isvalid)
                    {
                        _log.Info("The {0}'s validation failed in the ValidateJsonObjData Action filter ", objent.Name);
                        context.ModelState.AddModelError("ValidateJsonObjData", "");
                        return string.Format("{0} is not valid. please try again.", objent.Name);
                    }

                }
            }
            if (objent.Fields != null && objent.Fields.Count > 0)
            {
                foreach (var f in objent.Fields)
                {
                    FieldsdataTypevaluecheck(f, objson, ref context);
                }

            }
            if (objent.Tabs!=null && objent.Tabs.Count > 0)
            {
                foreach (var f in objent.Tabs)
                {
                    FieldsdataTypevaluecheck(f, objson, ref context);
                }

            }

            return string.Empty;

        }

        public string Requiredfieldandvaluecheck(FieldModel objent, Dictionary<string, object> objson, ref ActionExecutingContext context)
        {
            //bool Isvalid = true;
            _log.Info("Called ValidateJsonObjData's RequiredFieldCheck");

            //if (objent.ControlType.ToLower() != "section" && objent.ControlType.ToLower() != "tabs" && !Skipfields.Contains(objent.Name.ToLower()))
            if (objent.ControlType.ToLower() != "section" && objent.ControlType.ToLower() != "tabs")
            {
                if (objent.Required)
                {
                    if (objson.ContainsKey(objent.Name))
                    {

                        var Keyvalue = Convert.ToString(objson[objent.Name]);

                        if (string.IsNullOrEmpty(Keyvalue))
                        {
                            _log.Info("The {0}'s value is null in the ValidateJsonObjData's RequiredField_NoOfRequiredField_Check", objent.Name);
                            context.ModelState.AddModelError("ValidateJsonObjData", "");
                            return string.Format("{0} is required.", objent.Name);
                        }
                    }
                    else
                    {
                        _log.Info("The {0}  in the Jobject in the ValidateJsonObjData's RequiredField_NoOfRequiredField_Check", objent.Name);
                        context.ModelState.AddModelError("ValidateJsonObjData", "");
                        return string.Format("{0} is required.", objent.Name);

                    }
                }

            }
            if (objent.Fields != null && objent.Fields.Count > 0)
            {
                foreach (var f in objent.Fields)
                {
                    Requiredfieldandvaluecheck(f, objson, ref context);
                }

            }
            if (objent.Tabs != null && objent.Tabs.Count > 0)
            {
                foreach (var f in objent.Tabs)
                {
                    FieldsdataTypevaluecheck(f, objson, ref context);
                }

            }
            return string.Empty;

        }
    }
}
