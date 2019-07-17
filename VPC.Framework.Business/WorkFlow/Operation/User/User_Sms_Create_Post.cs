

using System;
using Microsoft.Extensions.Logging;
using NLog;
using VPC.Entities.Credential;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Entities.WorkFlow.Engine;
using VPC.Metadata.Business.DataAnnotations;
using Newtonsoft.Json.Linq;
using VPC.Entities.Common;
using VPC.Framework.Business.Common;

namespace VPC.Framework.Business.WorkFlow.Operation.User
{
     //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create,Context = WorkFlowEngine._user,
     Key = "User_Sms_Create_Password_Post", Id = "9EB3DB47-7888-44D7-9FE8-5B2A3F9BD5AB",ProcessType=WorkFlowProcessType.PostProcess)]
    public class User_Sms_Create_Post  : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
            try{
               
             objWorkFlowProcessMessage = new WorkFlowProcessMessage {Success = true};
            //  var workFlowProcessProperties = (WorkFlowProcessProperties) obj[0]; 
            //   var jObject =  (JObject) obj[1];   
            //   var tenantId = (Guid) obj[2];
            //   var userEntity=EntityMapper<VPC.Entities.EntityCore.Metadata.User>.MapperJObject(jObject);  


            //  var superAdminLoginInfo = new LoginInfo ();
            //         superAdminLoginInfo.TenantCode = tenantId.ToString ();
            //         superAdminLoginInfo.UserName = "admin";
            //         superAdminLoginInfo.Password = "admin";
            //         var sqlMemberShipProvider = new SqlMembershipProvider ();
            //         sqlMemberShipProvider.CreateCredential (tenantId,workFlowProcessProperties.resultId, superAdminLoginInfo);                    

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

       

        public WorkFlowProcessMessage Execute(dynamic obj)
        {
            throw new System.NotImplementedException();
        }
    }
    
}
