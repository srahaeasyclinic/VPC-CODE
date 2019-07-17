using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.APIs
{
 public interface IAdminSchedulerDaily
    {        
      bool Create(Guid tenantId, SchedulerDailyInfo info);
       bool Update(Guid tenantId, SchedulerDailyInfo info);
       bool Delete(Guid tenantId, Guid schedulerId );
    }
    internal  class AdminSchedulerDaily : IAdminSchedulerDaily
    {
        private readonly DataSchedulerDaily _data = new DataSchedulerDaily();

        bool IAdminSchedulerDaily.Create(Guid tenantId, SchedulerDailyInfo info)
        {
            return _data.Create(tenantId,info);
        }

        bool IAdminSchedulerDaily.Update(Guid tenantId, SchedulerDailyInfo info)
        {
            return _data.Update(tenantId,info);
        }

         bool IAdminSchedulerDaily.Delete(Guid tenantId,Guid schedulerId)
        {
            return _data.Delete(tenantId,schedulerId);
        }
    }
}