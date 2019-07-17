

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.Common;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;

namespace VPC.Framework.Business.WorkFlow.Operation.Tenant
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = InfoType.Tenant,
     Key = "TenantEmailPostProcess", Id = "496D004E-382D-4CF8-924F-8D0429371BAF", ProcessType = WorkFlowProcessType.PostProcess)]
    public class TenantEmailPostProcess : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            IEntityQueryManager _queryManager = new EntityQueryManager();
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
            try
            {

                objWorkFlowProcessMessage = new WorkFlowProcessMessage { Success = true };
                var workFlowProcessProperties = (WorkFlowProcessProperties)obj[0];
                var jObject = (JObject)obj[1];
                var tenantId = (Guid)obj[2];

                var userInfo = EntityMapper<VPC.Entities.EntityCore.Metadata.User>.MapperJObject(jObject);
                var tenantCode = _queryManager.GetSpecificIdByQuery(tenantId, workFlowProcessProperties.EntityName, workFlowProcessProperties.resultId, "Code");


                jObject.Add(new JProperty("TenantCode", tenantCode));

                var template = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "Emailtemplate", "Tenant", (int)ContextTypeEnum.NewTenantCredential, jObject);

                // if (template != null)
                // {
                //     if (template.Body != null && !string.IsNullOrEmpty(template.Body.Value))
                //     {
                //         string json = @"{'TenantCode': '" + tenantCode + "'}";
                //         JObject jObjectTenantCode = JObject.Parse(json);

                //         //  var item = new JObject("TenantCode", JToken.Parse(tenantCode));                        
                //         template.Body.Value = iMetadataManager.GetTemplateBodyWithTagablesValue(template.Body.Value, jObjectTenantCode);
                //     }
                // }

                var returnVal = DataUtility.SaveEmail(tenantId, workFlowProcessProperties.UserId, template, userInfo.UserCredential.Username.Value);


                return objWorkFlowProcessMessage;
            }
            catch (System.Exception ex)
            {
                _log.Error("TenantEmailPostProcess  having exception message" + ex.Message);

                objWorkFlowProcessMessage.Success = false;
                objWorkFlowProcessMessage.ErrorMessage = new ErrorMessage
                {
                    Code = WorkFlowMessage.ApplicationError,
                    Description = ex.Message
                };
                return objWorkFlowProcessMessage;
            }

        }

    }

}
