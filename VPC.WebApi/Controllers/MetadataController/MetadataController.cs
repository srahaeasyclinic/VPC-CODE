using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;
using System.Net;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.MetadataController
{
    [Route("api/metadata")]
    public class MetadataController : Controller
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IMetadataManager _iMetadataManager;

        private readonly IJsonMessage _iJsonMessage;

        public MetadataController(IMetadataManager iMetadataManager, IJsonMessage iJsonMessage)
        {
            _iMetadataManager = iMetadataManager;

            _iJsonMessage = iJsonMessage;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetEntities([FromQuery] string entityType = "")
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MetadataController GetEntities");

                var result = _iMetadataManager.GetEntities(false).OrderBy(t=>t.Name);

                if(entityType.ToLower() == "primaryentity")
                {
                    result = result.Where(a => a.Type.ToLower() == "primaryentity" || a.Type.ToLower() == "detailentity" || a.Type.ToLower() == "intersectentity").OrderBy(t=>t.Name);
                }                
               
                stopwatch.StopAndLog("GetEntities of MetadataController");
                return _iJsonMessage.IgnoreNullableObject(result);

            }
            catch (Exception ex)
            {
                //_log.Error("Error calling MetadataController.GetEntities method.");
                //throw new Exception(ex.Message);
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetentityByName(string name)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MetadataController GetentityByName");
                var result = _iMetadataManager.GetEntitityByName(name);
                if (result.Fields != null)
                    result.Fields = result.Fields.OrderBy(x => x.Name).ToList();

                if (result.Operations != null)
                    result.Operations = result.Operations.OrderBy(x => x.Name).ToList();

                stopwatch.StopAndLog("GetentityByName of MetadataController");
                return _iJsonMessage.IgnoreNullableObject(result);
            }
            catch (Exception ex)
            {
                //_log.Error("Error calling MetadataController.GetentityByName method.");
                //throw new Exception(ex.Message);
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{name}/sub-types")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetEntitySubtypes(string name)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MetadataController GetEntitySubtypes");
                var result = _iMetadataManager.GetSubTypes(name); 

                stopwatch.StopAndLog("GetEntitySubtypes of MetadataController");
                return _iJsonMessage.IgnoreNullableObject(result);
            }
            catch (Exception ex)
            { 
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    }
}
