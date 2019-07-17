using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.APIs;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Contracts
{
 public interface IManagerSchedulerMonthly
    {  
      Guid Create(Guid tenantId, SchedulerMonthlyInfo info);
      bool Update(Guid tenantId, SchedulerMonthlyInfo info);

      bool Delete(Guid tenantId, Guid schedulerId);
      SchedulerMonthlyInfo GetSchedulerMonthly(Guid tenantId,Guid schedulerId );

    }
    public  class ManagerSchedulerMonthly : IManagerSchedulerMonthly
    {
        private readonly IAdminSchedulerMonthly _admin = new AdminSchedulerMonthly();
        private readonly IReviewSchedulerMonthly _review = new ReviewSchedulerMonthly();

        Guid IManagerSchedulerMonthly.Create(Guid tenantId, SchedulerMonthlyInfo info)
        {
            info.SchedulerMonthlyId=Guid.NewGuid();
            _admin.Create(tenantId,info);
            return info.SchedulerMonthlyId;
        }

        bool IManagerSchedulerMonthly.Update(Guid tenantId, SchedulerMonthlyInfo info)
        {
            return  _admin.Update(tenantId,info);
        }

        bool IManagerSchedulerMonthly.Delete(Guid tenantId, Guid schedulerId)
        {
            return  _admin.Delete(tenantId,schedulerId);
        }


        SchedulerMonthlyInfo IManagerSchedulerMonthly.GetSchedulerMonthly(Guid tenantId, Guid schedulerId)
        {
           return  _review.GetSchedulerMonthly(tenantId,schedulerId);
        }

        
    }
}