

using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.Common;
using VPC.Entities.EntityCore;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;

namespace VPC.Framework.Business.WorkFlow.Operation.Tenant
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = InfoType.Tenant, Key = "Email", Id = "496D004E-382D-4CF8-924F-8D0429371BAF", ProcessType = WorkFlowProcessType.PostProcess)]
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

                var userObj = jObject.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals ("user"));
                var userJObject = userObj.First ().ToObject<JObject> ();

                var userInfo = EntityMapper<VPC.Entities.EntityCore.Metadata.User>.MapperJObject(userJObject);
                var tenantCode = _queryManager.GetSpecificIdByQuery(tenantId, workFlowProcessProperties.EntityName, workFlowProcessProperties.resultId, "Code");
                userJObject.Add(new JProperty("TenantCode", tenantCode));
                var template = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "Emailtemplate", "Tenant", (int)ContextTypeEnum.NewTenantCredential, userJObject);
                var returnVal = DataUtility.SaveEmail(tenantId, workFlowProcessProperties.UserId, template, userInfo.UserCredential.Username.Value,"NewTenantCredential",InfoType.Tenant);
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
