using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.SampleBusiness.Contracts; 
using VPC.WebApi.Utility;
using VPC.Framework.Business.Initilize.Contracts;

namespace VPC.WebApi.Controllers.SampleController
{
    [Route("api/samples")]
    public class SampleController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly ISampleBusinessManager _sampleBusinessManager;
        private readonly IMetadataManager _iMetadataManager;

        public SampleController(ISampleBusinessManager sampleBusinessManager,IMetadataManager iMetadataManager )
        {
            this._sampleBusinessManager = sampleBusinessManager;
            _iMetadataManager = iMetadataManager;
        }




        [HttpPost]
        [Route("Entities")]
        public IActionResult CreateSampleEntities()
        {

            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
               
                var sampleEntity = new SampleEntity();
                sampleEntity.Id = Guid.NewGuid();
                sampleEntity.FullName = "Chakarborty Marani Ashok";
                sampleEntity.Age = 56;

                _log.Info("Called SampleController CreateSampleEntities with SampleEntity {0}=", JsonConvert.SerializeObject(sampleEntity));
                 
                var retVal = _sampleBusinessManager.Create(sampleEntity);
                stopwatch.StopAndLog("CreateSampleEntities method of SampleController");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("Entities")]
        public IActionResult UpdateSampleEntities()
        {

            try
            {
                var sampleEntity = new SampleEntity();
                sampleEntity.Id =Guid.Parse("D4B98C2C-46DE-4A2A-9335-5735851F28EF");
                sampleEntity.FullName = "Madan kumar Mallik";
                sampleEntity.Age = 96;

                var retVal = _sampleBusinessManager.Update(sampleEntity.Id,sampleEntity);
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("Entities/{id:guid}")]
        public IActionResult DeleteSampleEntity([FromRoute] Guid id)
        { 
            try
            { 
                var retVal = _sampleBusinessManager.Delete(id);
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("Entities")]
        public IActionResult GetAllSampleEntities()
        {
            
            try
            {
                var retVal = _sampleBusinessManager.GetAll();
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("Entities/{id:guid}")]
        public IActionResult GetAllSampleEntity([FromRoute] Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                var retVal = _sampleBusinessManager.GetById(id);
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


        [HttpGet]
        [Route("delete-test/{entityName}/{id:guid}")]
        public IActionResult GetDefaultColumnName([FromRoute] string entityName, Guid id)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                // //  IEntityQueryManager queryManager = new EntityQueryManager ();
                // // var subscriptionId = queryManager.GetSpecificIdByQuery(TenantCode, "Tenant", "EA3A7E30-AFD8-4A17-9B83-1247A4EDB87C", "Code");
                // // return this.Ok(subscriptionId);
                // var columns = _iMetadataManager.GetColumnNameByEntityName(name, null);

                // // var list = new List<dynamic>();
                // // foreach (var item in columns)
                // // {
                // //     if(string.IsNullOrEmpty(item.InversePrefixName))continue;
                // //     list.Add(item);
                // // }
                // var entity =  _iMetadataManager.GetEntitityByName(name);
                // var fieldsStr = string.Empty;
                // foreach (var item in entity.Fields)
                // {
                //     fieldsStr+=item.Name+",";
                // }
                // return this.Ok(new { columns, fieldsStr});

                // IDeleteHelper delete = new DeleteHelper();
        
                // var res = delete.BuildDeleteQuery(id, TenantCode, entityName, UserId);
                // return this.Ok(res);
                return this.Ok("test");
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


        [HttpGet]
        [Route("value-replace/{rootTenantId:guid}/{initTenantId:guid}")]
        public IActionResult GetDefaultColumnName(Guid rootTenantId, Guid initTenantId)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                var replacer = new InitilizeManager();
                replacer.Test(rootTenantId, initTenantId);
                return this.Ok("test");
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

       
    }
}