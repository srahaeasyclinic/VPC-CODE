using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.RelationManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.EntityController {
    [Route ("api/relations")]
    //   [Authorize(Policy = "AuthRole")]   
    public class RelationController : BaseApiController {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly IEntityResourceManager _iEntityResourceManager;
        private readonly ILayoutManager _iILayoutManager;
        private readonly IMetadataManager _iMetadataManager;
        private readonly IJsonMessage _iJsonMessage;
        private readonly IInitilizeManager _initilizeManager;

        private readonly IRelationManager _iRelationManager;

        public RelationController (IEntityResourceManager iEntityResourceManager, ILayoutManager iILayoutManager, IMetadataManager iMetadataManager, IJsonMessage iJsonMessage,
            IInitilizeManager initilizeManager,
            IRelationManager iRelationManager
        ) {
            _iEntityResourceManager = iEntityResourceManager;
            _iILayoutManager = iILayoutManager;
            _iMetadataManager = iMetadataManager;
            _iJsonMessage = iJsonMessage;
            _initilizeManager = initilizeManager;
            _iRelationManager = iRelationManager;
        }

        [HttpPut]
        [Route ("{entityName}/{id:guid}/{intersectEntityName}/{linkerEntityName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult UpdateIntersectEntity ([FromRoute] string entityName, Guid id, string intersectEntityName, string linkerEntityName, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called UpdateIntersectEntity");
                if (value == null) return BadRequest ("Value required");
                Dictionary<string, string> payload = ((IDictionary<string, JToken>) (JObject) value).ToDictionary (pair => pair.Key, pair => (string) pair.Value);
                if (payload == null) return BadRequest ("Value required");

                var relationsString = payload[intersectEntityName];
                var childList = new List<Guid>();
                if(!string.IsNullOrEmpty(relationsString)){
                    string[] authorsList = relationsString.Split(",");
                    foreach (var item in authorsList)
                    {
                        Guid newGuid = Guid.Parse(item);
                        childList.Add(newGuid);
                    }
                }
                if(childList.Any()){
                     var result = _iRelationManager.AddRelations (TenantCode, intersectEntityName, entityName, id, linkerEntityName, childList);
                     return Ok (result);
                }
                return BadRequest ("Linker required");
            } catch (FieldAccessException ex) {
                //return BadRequest(ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    }
}