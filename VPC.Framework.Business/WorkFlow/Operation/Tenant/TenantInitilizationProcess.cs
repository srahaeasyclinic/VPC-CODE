

using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using NLog;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Framework.Business.WorkFlow.Operation.Tenant
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = InfoType.Tenant,
     Key = "TenantInitilization_Process", Id = "4E8EFF8F-76E6-44EC-92AD-F2ECEDEC693D", ProcessType = WorkFlowProcessType.Process)]
    public class TenantInitilization_Process : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            IInitilizeManager _initilizeManager = new InitilizeManager();
            IEntityQueryManager queryManager = new EntityQueryManager();
            IManagerTenantSubscriptionEntity _managerSubscriptionEntity = new ManagerTenantSubscriptionEntity();
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
            try
            {

                objWorkFlowProcessMessage = new WorkFlowProcessMessage { Success = true };
                var workFlowProcessProperties = (WorkFlowProcessProperties)obj[0];
                var tenantId = (Guid)obj[2];

                var subscriptionId = queryManager.GetSpecificIdByQuery(tenantId, workFlowProcessProperties.EntityName, workFlowProcessProperties.resultId, "TenantSubscription");
                if (subscriptionId != null)
                {
                    var subscriptionEntities = _managerSubscriptionEntity.TenantSubscriptionEntities(tenantId, new Guid(subscriptionId.ToString()));
                    if (subscriptionEntities.Any())
                    {
                        var entityIds = subscriptionEntities.Select(p => p.EntityId).ToList();
                        var status = _initilizeManager.Initilize(workFlowProcessProperties.resultId, entityIds, workFlowProcessProperties.UserId, subscriptionId);
                    }
                }


                return objWorkFlowProcessMessage;
            }
            catch (System.Exception ex)
            {
                _log.Error("TenantInitilization_PostProcess  having exception message" + ex.Message);

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
