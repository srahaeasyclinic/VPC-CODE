using System;
using VPC.Entities.BatchType;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.APIs;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts
{
    public interface IManagerScheduler
    { 
        DateTime GetNextRunDateTime(Guid tenantId, Guid schedulerId);
    }
    public  class ManagerScheduler : IManagerScheduler
    {
        
        private readonly IReviewScheduler _reviewScheduler = new ReviewScheduler();
        
        DateTime IManagerScheduler.GetNextRunDateTime(Guid tenantId, Guid schedulerId)
        {
            var schedulerInfo=_reviewScheduler.GetScheduler(tenantId,schedulerId);  
            DateTime nextStartTime = DateTime.MinValue;
            var seletedTime = string.Format("{0}:{1}",schedulerInfo.Hour.Value,schedulerInfo.Minute.Value); 
            var currentDate = DateTime.UtcNow.ToShortDateString() + " " + seletedTime;
            DateTime nextDateTime = DateTime.Parse(currentDate);
          //  var subscriptionId = queryManager.GetSpecificIdByQuery(tenantId, "tenant", tenantId, "TenantSubscription");
            var objTimeZone = System.TimeZone.CurrentTimeZone.StandardName; //_timeZoneManager.GetDefaultTimeZone(tenantId);
            var defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById(objTimeZone);
            var defaultDateTime = TimeZoneInfo.ConvertTimeToUtc(nextDateTime, defaultTimeZone);

            if ((BatchIntervalType)(Convert.ToInt32(schedulerInfo.Interval.Value)) == BatchIntervalType.Daily)
            {
                if (Convert.ToInt32(schedulerInfo.DailyUnit.Value) == 1)
                {
                    var dailyEveryDay=!string.IsNullOrEmpty(schedulerInfo.DailyEveryDay.Value) ?  Convert.ToInt32(schedulerInfo.DailyEveryDay.Value) :1;
                    nextStartTime = defaultDateTime.AddHours(24 * (dailyEveryDay));
                }
                else
                {
                    var daynumber = (int)defaultDateTime.DayOfWeek;
                    if (daynumber == 5)
                    {
                        nextStartTime = defaultDateTime.AddDays(3);
                    }else if (daynumber == 6)
                    {
                        nextStartTime = defaultDateTime.AddDays(2);
                    }
                    else
                    {
                        nextStartTime = defaultDateTime.AddDays(1);
                    }

                }
            }
            else if ((BatchIntervalType)(Convert.ToInt32(schedulerInfo.Interval.Value)) == BatchIntervalType.Weekly)
            {
                var weeklyReccurance=!string.IsNullOrEmpty(schedulerInfo.WeeklyReccurance.Value) ? Convert.ToInt32(schedulerInfo.WeeklyReccurance.Value) :1;
                var daynumber = (int)getSelectedDay(schedulerInfo);
                nextStartTime = defaultDateTime.AddDays((7 * (weeklyReccurance))-1);
                nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + daynumber);
            }
            else if ((BatchIntervalType)(Convert.ToInt32(schedulerInfo.Interval.Value)) == BatchIntervalType.Monthly)
            {
                if (Convert.ToInt32(schedulerInfo.MonthlyUnit.Value) == 1)
                {
                    var monthlySpecificDay=!string.IsNullOrEmpty(schedulerInfo.MonthlySpecificDay.Value) ? Convert.ToInt32(schedulerInfo.MonthlySpecificDay.Value) : 1;
                    var monthlySpecificMonthly=!string.IsNullOrEmpty(schedulerInfo.MonthlySpecificMonth.Value) ? Convert.ToInt32(schedulerInfo.MonthlySpecificMonth.Value) : 1;


                    nextStartTime = defaultDateTime.AddMonths(monthlySpecificMonthly);
                    int daysInMonth = DateTime.DaysInMonth(nextStartTime.Year, nextStartTime.Month);
                    if (monthlySpecificDay > daysInMonth)
                    {
                        schedulerInfo.MonthlySpecificDay.Value = daysInMonth.ToString();
                    }
                    nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, monthlySpecificDay, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
                }
                else
                {

                    var monthlyInferredMonth=!string.IsNullOrEmpty(schedulerInfo.MonthlyInferredMonth.Value) ? Convert.ToInt32(schedulerInfo.MonthlyInferredMonth.Value) : 1;
                    var monthlyInferredWeekGroup=!string.IsNullOrEmpty(schedulerInfo.MonthlyInferredWeekGroup.Value) ? Convert.ToInt32(schedulerInfo.MonthlyInferredWeekGroup.Value) : 1;
                    var monthlyInferredDay=!string.IsNullOrEmpty(schedulerInfo.MonthlyInferredDay.Value) ? Convert.ToInt32(schedulerInfo.MonthlyInferredDay.Value) : 1;

                    nextStartTime = defaultDateTime.AddMonths(monthlyInferredMonth);
                    nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, 1, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
                    nextStartTime = nextStartTime.AddDays((7 * monthlyInferredWeekGroup)-1);
                    nextStartTime = FirstDateOfWeek(nextStartTime);
                    nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + monthlyInferredDay);
                }
            }
            else if ((BatchIntervalType)(Convert.ToInt32(schedulerInfo.Interval.Value)) == BatchIntervalType.Yearly)
            {
                var YearlyReccurance=!string.IsNullOrEmpty(schedulerInfo.YearlyReccurance.Value) ? Convert.ToInt32(schedulerInfo.YearlyReccurance.Value) : 1;
                if (Convert.ToInt32(schedulerInfo.YearlyUnit.Value) == 1)
                {
                    var yearlySpecificMonth=!string.IsNullOrEmpty(schedulerInfo.YearlySpecificMonth.Value) ? Convert.ToInt32(schedulerInfo.YearlySpecificMonth.Value) : 1;
                    var yearlySpecificYear=!string.IsNullOrEmpty(schedulerInfo.YearlySpecificYear.Value) ? Convert.ToInt32(schedulerInfo.YearlySpecificYear.Value) : 1;

                    nextStartTime = defaultDateTime.AddYears(YearlyReccurance);
                    nextStartTime = new DateTime(nextStartTime.Year, yearlySpecificMonth, nextStartTime.Day, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
                    int daysInMonth = DateTime.DaysInMonth(nextStartTime.Year, nextStartTime.Month);
                    if (yearlySpecificYear> daysInMonth)
                    {
                        schedulerInfo.YearlySpecificYear.Value = daysInMonth.ToString();
                    }
                    nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, yearlySpecificYear, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);

                }
                else
                {
                    var yearlyInferredMonth=!string.IsNullOrEmpty(schedulerInfo.YearlyInferredMonth.Value) ? Convert.ToInt32(schedulerInfo.YearlyInferredMonth.Value) : 1;
                    var yearlyInferredWeekGroup=!string.IsNullOrEmpty(schedulerInfo.YearlyInferredWeekGroup.Value) ? Convert.ToInt32(schedulerInfo.YearlyInferredWeekGroup.Value) : 1;
                    var yearlyInferredDay=!string.IsNullOrEmpty(schedulerInfo.YearlyInferredDay.Value) ? Convert.ToInt32(schedulerInfo.YearlyInferredDay.Value) : 1;

                    nextStartTime = defaultDateTime.AddYears(YearlyReccurance);
                    nextStartTime = new DateTime(nextStartTime.Year, yearlyInferredMonth, 1, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
                    nextStartTime = nextStartTime.AddDays((7 * yearlyInferredWeekGroup)-1);
                    nextStartTime = FirstDateOfWeek(nextStartTime);
                    nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + yearlyInferredDay);
                }
            }

            if (nextStartTime < DateTime.UtcNow)
                nextStartTime = DateTime.UtcNow.AddMinutes(1);

            return nextStartTime;
        }

        private static DateTime FirstDateOfWeek(DateTime nextdate)
        {
            var firstDate = new DateTime(nextdate.Year, nextdate.Month, nextdate.Day);
            while (firstDate.DayOfWeek != DayOfWeek.Monday)
                firstDate = firstDate.AddDays(-1);

            return firstDate;
        }

         private DayOfWeek getSelectedDay(BatchTypeScheduler weekly)
        {
            DayOfWeek weekday = 0;
            if (weekly.WeeklyMonday.Value)
            {
                weekday = DayOfWeek.Monday;
            }
            else if (weekly.WeeklyTuesday.Value)
            {
                weekday = DayOfWeek.Tuesday;
            }
            else if (weekly.WeeklyWednesday.Value)
            {
                weekday = DayOfWeek.Wednesday;
            }
            else if (weekly.WeeklyThrusday.Value)
            {
                weekday = DayOfWeek.Thursday;
            }
            else if (weekly.WeeklyFriday.Value)
            {
                weekday = DayOfWeek.Friday;
            }
            else if (weekly.WeeklySaturday.Value)
            {
                weekday = DayOfWeek.Saturday;
            }
            else if (weekly.WeeklySunday.Value)
            {
                weekday = DayOfWeek.Sunday;
            }

            return weekday;
        }


       


        
    }
}