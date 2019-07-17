using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.APIs;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Contracts
{
 public interface IManagerSchedulerDaily
    {  
      Guid Create(Guid tenantId, SchedulerDailyInfo info);
      bool Update(Guid tenantId, SchedulerDailyInfo info);
      bool Delete(Guid tenantId,Guid schedulerId);
      SchedulerDailyInfo GetSchedulerDaily(Guid tenantId,Guid schedulerId );

    }
    public  class ManagerSchedulerDaily : IManagerSchedulerDaily
    {
        private readonly IAdminSchedulerDaily _admin = new AdminSchedulerDaily();
        private readonly IReviewSchedulerDaily _review = new ReviewSchedulerDaily();

        Guid IManagerSchedulerDaily.Create(Guid tenantId, SchedulerDailyInfo info)
        {
            info.SchedulerDailyId=Guid.NewGuid();
            _admin.Create(tenantId,info);
            return info.SchedulerDailyId;
        }

        bool IManagerSchedulerDaily.Update(Guid tenantId, SchedulerDailyInfo info)
        {
            return  _admin.Update(tenantId,info);
        }

        SchedulerDailyInfo IManagerSchedulerDaily.GetSchedulerDaily(Guid tenantId, Guid schedulerId)
        {
           return  _review.GetSchedulerDaily(tenantId,schedulerId);
        }

        bool IManagerSchedulerDaily.Delete(Guid tenantId,Guid schedulerId)
        {
            return _admin.Delete(tenantId,schedulerId);
        }

        
    }
}