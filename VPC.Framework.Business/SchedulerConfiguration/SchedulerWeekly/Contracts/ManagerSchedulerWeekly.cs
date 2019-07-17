using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.APIs;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Contracts
{
 public interface IManagerSchedulerWeekly
    {  
      Guid Create(Guid tenantId, SchedulerWeeklyInfo info);
      bool Update(Guid tenantId, SchedulerWeeklyInfo info);
     bool Delete(Guid tenantId, Guid schedulerId);
     SchedulerWeeklyInfo GetSchedulerWeekly(Guid tenantId,Guid schedulerId );

    }
    public  class ManagerSchedulerWeekly : IManagerSchedulerWeekly
    {
        private readonly IAdminSchedulerWeekly _admin = new AdminSchedulerWeekly();
        private readonly IReviewSchedulerWeekly _review = new ReviewSchedulerWeekly();

        Guid IManagerSchedulerWeekly.Create(Guid tenantId, SchedulerWeeklyInfo info)
        {
            info.SchedulerWeeklyId=Guid.NewGuid();
            _admin.Create(tenantId,info);
            return info.SchedulerWeeklyId;
        }

        bool IManagerSchedulerWeekly.Update(Guid tenantId, SchedulerWeeklyInfo info)
        {
            return  _admin.Update(tenantId,info);
        }

        bool IManagerSchedulerWeekly.Delete(Guid tenantId, Guid schedulerId)
        {
            return  _admin.Delete(tenantId,schedulerId);
        }

        SchedulerWeeklyInfo IManagerSchedulerWeekly.GetSchedulerWeekly(Guid tenantId, Guid schedulerId)
        {
           return  _review.GetSchedulerWeekly(tenantId,schedulerId);
        }

        
    }
}