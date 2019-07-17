using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.APIs;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Contracts
{
 public interface IManagerSchedulerYearly
    {  
      Guid Create(Guid tenantId, SchedulerYearlyInfo info);
      bool Update(Guid tenantId, SchedulerYearlyInfo info);
       bool Delete(Guid tenantId, Guid schedulerId );
      SchedulerYearlyInfo GetSchedulerYearly(Guid tenantId,Guid schedulerId );

    }
    public  class ManagerSchedulerYearly : IManagerSchedulerYearly
    {
        private readonly IAdminSchedulerYearly _admin = new AdminSchedulerYearly();
        private readonly IReviewSchedulerYearly _review = new ReviewSchedulerYearly();

        Guid IManagerSchedulerYearly.Create(Guid tenantId, SchedulerYearlyInfo info)
        {
            info.SchedulerYearlyId=Guid.NewGuid();
            _admin.Create(tenantId,info);
            return info.SchedulerYearlyId;
        }

        bool IManagerSchedulerYearly.Update(Guid tenantId, SchedulerYearlyInfo info)
        {
            return  _admin.Update(tenantId,info);
        }

        bool IManagerSchedulerYearly.Delete(Guid tenantId, Guid schedulerId)
        {
            return  _admin.Delete(tenantId,schedulerId);
        }

        SchedulerYearlyInfo IManagerSchedulerYearly.GetSchedulerYearly(Guid tenantId, Guid schedulerId)
        {
           return  _review.GetSchedulerYearly(tenantId,schedulerId);
        }

       
    }
}