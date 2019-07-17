using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.APIs
{
 public interface IReviewSchedulerMonthly
    {        
      SchedulerMonthlyInfo GetSchedulerMonthly(Guid tenantId,Guid schedulerId );
    }
    internal  class ReviewSchedulerMonthly : IReviewSchedulerMonthly
    {
        private readonly DataSchedulerMonthly _data = new DataSchedulerMonthly();

        SchedulerMonthlyInfo IReviewSchedulerMonthly.GetSchedulerMonthly(Guid tenantId, Guid schedulerId)
        {
           return _data.GetSchedulerMonthly(tenantId,schedulerId);
        }
    }
}