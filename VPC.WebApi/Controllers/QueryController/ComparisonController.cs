using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;
using NLog;

namespace VPC.WebApi.Controllers.EntityResourceController {
    [Route ("api/comparisons")]
    public class ComparisonController : BaseApiController {

        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IEntityResourceManager _iEntityResourceManager;
        private readonly IPicklistManager _iPicklistManager;
        public ComparisonController (IEntityResourceManager iEntityResourceManager, IPicklistManager iPicklistManager) {
            _iEntityResourceManager = iEntityResourceManager;
            _iPicklistManager = iPicklistManager;
        }

        public object Enumerations { get; private set; }

        [HttpGet ("")]
        public IActionResult GetQuery () {
            try {
          //      var result = new Dictionary<string, dynamic>();

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ComparisonController GetQuery");

                var result = new List<dynamic>();
                Type enumType = typeof(Comparison);     
                var enumValues = enumType.GetEnumValues();    
                foreach (Comparison value in enumValues)    
                {    
                    MemberInfo memberInfo =enumType.GetMember(value.ToString()).First();     
                    var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                    var id = value.ToString();
                    var description = (descriptionAttribute != null)?descriptionAttribute.Description:value.ToString();
                    var myObj = new { id,  description};
                    result.Add(myObj);  

                }    

                stopwatch.StopAndLog("GetQuery of ComparisonController");

                return Ok (new { result });
            } catch (FieldAccessException fx) {
                //return BadRequest (fx.Message);
                _log.Error(ExceptionFormatter.SerializeToString(fx));
                return StatusCode((int)HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


        //  private Dictionary<string, dynamic> GetPickListData(Type t)
        // {
        //     var statusEnum = new Dictionary<string, dynamic>();
        //     Array enumValueArray = Enum.GetValues(t);
        //     foreach (int enumValue in enumValueArray)
        //     {
        //         var Name = Enum.GetName(t, enumValue);
        //         statusEnum.Add(Name, enumValue);
        //     }
        //     return statusEnum;
        // }
    }
}