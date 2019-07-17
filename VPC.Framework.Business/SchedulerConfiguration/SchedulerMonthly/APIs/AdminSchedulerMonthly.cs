using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.APIs
{
 public interface IAdminSchedulerMonthly
    {        
      bool Create(Guid tenantId, SchedulerMonthlyInfo info);
      bool Update(Guid tenantId, SchedulerMonthlyInfo info);
      bool Delete(Guid tenantId, Guid schedulerId);
    }
    internal  class AdminSchedulerMonthly : IAdminSchedulerMonthly
    {
        private readonly DataSchedulerMonthly _data = new DataSchedulerMonthly();

        bool IAdminSchedulerMonthly.Create(Guid tenantId, SchedulerMonthlyInfo info)
        {
            return _data.Create(tenantId,info);
        }

        bool IAdminSchedulerMonthly.Update(Guid tenantId, SchedulerMonthlyInfo info)
        {
            return _data.Update(tenantId,info);
        }

        bool IAdminSchedulerMonthly.Delete(Guid tenantId, Guid schedulerId)
        {
            return  _data.Delete(tenantId,schedulerId);
        }
    }
}