using System;
using VPC.Entities.SchedulerConfiguration;

using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.APIs
{
 public interface IAdminSchedulerWeekly
    {        
       bool Create(Guid tenantId, SchedulerWeeklyInfo info);
       bool Update(Guid tenantId, SchedulerWeeklyInfo info);

       bool Delete(Guid tenantId, Guid schedulerId);
    }
    internal  class AdminSchedulerWeekly : IAdminSchedulerWeekly
    {
        private readonly DataSchedulerWeekly _data = new DataSchedulerWeekly();

        bool IAdminSchedulerWeekly.Create(Guid tenantId, SchedulerWeeklyInfo info)
        {
            return _data.Create(tenantId,info);
        }

        bool IAdminSchedulerWeekly.Update(Guid tenantId, SchedulerWeeklyInfo info)
        {
            return _data.Update(tenantId,info);
        }

        bool IAdminSchedulerWeekly.Delete(Guid tenantId, Guid schedulerId)
        {
            return  _data.Delete(tenantId,schedulerId);
        }

        
    }
}