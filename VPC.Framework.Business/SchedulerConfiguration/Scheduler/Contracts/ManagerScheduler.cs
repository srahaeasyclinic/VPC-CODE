using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.APIs;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts
{
 public interface IManagerScheduler
    {  
       Guid Create(Guid tenantId, SchedulerInfo info);
       bool Update(Guid tenantId, SchedulerInfo info);      
       SchedulerInfo GetScheduler(Guid tenantId,Guid batchTypeId );
    }
    public  class ManagerScheduler : IManagerScheduler
    {
        private readonly IAdminScheduler _adminScheduler = new AdminScheduler();
        private readonly IReviewScheduler _reviewScheduler = new ReviewScheduler();

        Guid IManagerScheduler.Create(Guid tenantId, SchedulerInfo info)
        {
            info.SchedulerId=Guid.NewGuid();
            _adminScheduler.Create(tenantId,info);
            return info.SchedulerId;
        }

        bool IManagerScheduler.Update(Guid tenantId, SchedulerInfo info)
        {
            return  _adminScheduler.Update(tenantId,info);
        }

        SchedulerInfo IManagerScheduler.GetScheduler(Guid tenantId, Guid batchTypeId)
        {            
           return  _reviewScheduler.GetScheduler(tenantId,batchTypeId);        
        }

        
    }
}