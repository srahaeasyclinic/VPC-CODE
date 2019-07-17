using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.APIs
{
 public interface IReviewSchedulerYearly
    {        
      SchedulerYearlyInfo GetSchedulerYearly(Guid tenantId,Guid schedulerId );
    }
    internal  class ReviewSchedulerYearly : IReviewSchedulerYearly
    {
        private readonly DataSchedulerYearly _data = new DataSchedulerYearly();

        SchedulerYearlyInfo IReviewSchedulerYearly.GetSchedulerYearly(Guid tenantId, Guid schedulerId)
        {
           return _data.GetSchedulerYearly(tenantId,schedulerId);
        }
    }
}