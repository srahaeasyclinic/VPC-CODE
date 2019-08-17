using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.APIs
{
 public interface IAdminScheduler
    {        
       //bool Create(Guid tenantId, SchedulerInfo info);
       //bool Update(Guid tenantId, SchedulerInfo info);
    }
    internal  class AdminScheduler : IAdminScheduler
    {
        private readonly DataScheduler _data = new DataScheduler();

        // bool IAdminScheduler.Create(Guid tenantId, SchedulerInfo info)
        // {
        //    return _data.Create(tenantId,info);
        // }
        // bool IAdminScheduler.Update(Guid tenantId, SchedulerInfo info)
        // {
        //     return _data.Update(tenantId,info);
        // }
    }
}