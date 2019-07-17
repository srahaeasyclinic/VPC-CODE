using System;
using VPC.Entities.SchedulerConfiguration;

using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.APIs
{
 public interface IAdminSchedulerYearly
    {        
      bool Create(Guid tenantId, SchedulerYearlyInfo info);
       bool Update(Guid tenantId, SchedulerYearlyInfo info);
       bool Delete(Guid tenantId, Guid schedulerId );
    }
    internal  class AdminSchedulerYearly : IAdminSchedulerYearly
    {
        private readonly DataSchedulerYearly _data = new DataSchedulerYearly();

        bool IAdminSchedulerYearly.Create(Guid tenantId, SchedulerYearlyInfo info)
        {
            return _data.Create(tenantId,info);
        }

        bool IAdminSchedulerYearly.Delete(Guid tenantId, Guid schedulerId)
        {
         return _data.Delete(tenantId,schedulerId);
        }

        bool IAdminSchedulerYearly.Update(Guid tenantId, SchedulerYearlyInfo info)
        {
            return _data.Update(tenantId,info);
        }
    }
}