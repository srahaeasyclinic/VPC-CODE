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
using VPC.Entities.WorkFlow.Engine;
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

namespace VPC.Framework.Business.WorkFlow.Operation.User
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = WorkFlowEngine._user,
     Key = "User_Email_Create_Post", Id = "96DB04D6-8F2F-4B41-95DF-2DFBB2449DE4", ProcessType = WorkFlowProcessType.PostProcess)]
    public class User_Email_Create_Post : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            IEntityQueryManager _queryManager = new EntityQueryManager();
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
            try
            {

                objWorkFlowProcessMessage = new WorkFlowProcessMessage { Success = true };
                var workFlowProcessProperties = (WorkFlowProcessProperties)obj[0];
                var jObject = (JObject)obj[1];
                var tenantId = (Guid)obj[2];


                
                var userObj = jObject.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals ("user"));
                var details = userObj.First ().ToObject<JObject> ();
             

                var userEntity = EntityMapper<VPC.Entities.EntityCore.Metadata.User>.MapperJObject(details);

                var tenantCode = _queryManager.GetSpecificIdByQuery(tenantId, "Tenant", tenantId, "Code");

                details.Add(new JProperty("TenantCode", tenantCode));
                var template = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "Emailtemplate", "User", (int)ContextTypeEnum.Welcome, details);
                var returnVal = DataUtility.SaveEmail(tenantId, workFlowProcessProperties.UserId, template, userEntity.UserCredential.Username.Value,"NewUserCredential",InfoType.User);
                return objWorkFlowProcessMessage;
            }
            catch (System.Exception ex)
            {
                _log.Error("User_Email_Create_Post  having exception message" + ex.Message);

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
