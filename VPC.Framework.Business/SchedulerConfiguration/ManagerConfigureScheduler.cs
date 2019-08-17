// using System;
// using System.Transactions;
// using VPC.Entities.SchedulerConfiguration;
// using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts;
// using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Contracts;
// using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Contracts;
// using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Contracts;
// using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Contracts;
// using VPC.Framework.Business.DynamicQueryManager.Contracts;

// namespace VPC.Framework.Business.SchedulerConfiguration
// {
//     public interface IManagerConfigureScheduler
//     {
//         Guid ConfigureScheduler(Guid tenantId, SchedulerInfo info);
//         SchedulerInfo GetConfigureScheduler(Guid tenantId, Guid batchTypeId);
//         DateTime GetNextStartDateTime(Guid tenantId, SchedulerInfo syncSchedulerInfo);

//     }
//     public class ManagerConfigureScheduler : IManagerConfigureScheduler
//     {
//         private readonly IManagerScheduler _managerScheduler = new ManagerScheduler();
//         private readonly IManagerSchedulerDaily _managerDaily = new ManagerSchedulerDaily();
//         private readonly IManagerSchedulerMonthly _managerMonthly = new ManagerSchedulerMonthly();
//         private readonly IManagerSchedulerWeekly _managerWeekly = new ManagerSchedulerWeekly();
//         private readonly IManagerSchedulerYearly _managerYearly = new ManagerSchedulerYearly();
//         IEntityQueryManager queryManager = new EntityQueryManager ();

//         Guid IManagerConfigureScheduler.ConfigureScheduler(Guid tenantId, SchedulerInfo info)
//         {
//             try
//             {
//                 using (var scope1 = new TransactionScope(TransactionScopeOption.Required))
//                 {
//                     if (info.SchedulerId == Guid.Empty)
//                         info.SchedulerId = _managerScheduler.Create(tenantId, info);
//                     else
//                         _managerScheduler.Update(tenantId, info);

//                     _managerDaily.Delete(tenantId, info.SchedulerId);
//                     _managerWeekly.Delete(tenantId, info.SchedulerId);
//                     _managerMonthly.Delete(tenantId, info.SchedulerId);
//                     _managerYearly.Delete(tenantId, info.SchedulerId);

//                     if (info.RecurrenceType == RecurrencePattern.Daily)
//                     {
//                         info.Daily.SchedulerId = info.SchedulerId;
//                         _managerDaily.Create(tenantId, info.Daily);

//                     }
//                     else if (info.RecurrenceType == RecurrencePattern.Weekly)
//                     {
//                         info.Weekly.SchedulerId = info.SchedulerId;
//                         _managerWeekly.Create(tenantId, info.Weekly);
//                     }
//                     else if (info.RecurrenceType == RecurrencePattern.Monthly)
//                     {
//                         info.Monthly.SchedulerId = info.SchedulerId;
//                         _managerMonthly.Create(tenantId, info.Monthly);
//                     }
//                     else if (info.RecurrenceType == RecurrencePattern.Yearly)
//                     {
//                         info.Yearly.SchedulerId = info.SchedulerId;
//                         _managerYearly.Create(tenantId, info.Yearly);
//                     }
//                     scope1.Complete();
//                 }
//             }

//             catch
//             {
//                 throw;
//             }
//             return info.SchedulerId;
//         }

//         SchedulerInfo IManagerConfigureScheduler.GetConfigureScheduler(Guid tenantId, Guid batchTypeId)
//         {
//             var info = _managerScheduler.GetScheduler(tenantId, batchTypeId);
//             if (info == null)
//             {
//                 info = new SchedulerInfo { BatchTypeId = batchTypeId };
//                 info.Daily = new SchedulerDailyInfo();
//                 info.Weekly = new SchedulerWeeklyInfo();
//                 info.Monthly = new SchedulerMonthlyInfo();
//                 info.Yearly = new SchedulerYearlyInfo();
//             }

//             if (info != null)
//             {
//                 info.Daily = new SchedulerDailyInfo();
//                 info.Weekly = new SchedulerWeeklyInfo();
//                 info.Monthly = new SchedulerMonthlyInfo();
//                 info.Yearly = new SchedulerYearlyInfo();
//                 if (info.RecurrenceType == RecurrencePattern.Daily)
//                 {
//                     info.Daily = _managerDaily.GetSchedulerDaily(tenantId, info.SchedulerId);
//                 }
//                 else if (info.RecurrenceType == RecurrencePattern.Weekly)
//                 {
//                     info.Weekly = _managerWeekly.GetSchedulerWeekly(tenantId, info.SchedulerId);
//                 }
//                 else if (info.RecurrenceType == RecurrencePattern.Monthly)
//                 {
//                     info.Monthly = _managerMonthly.GetSchedulerMonthly(tenantId, info.SchedulerId);
//                 }
//                 else if (info.RecurrenceType == RecurrencePattern.Yearly)
//                 {
//                     info.Yearly = _managerYearly.GetSchedulerYearly(tenantId, info.SchedulerId);
//                 }

//             }
//             return info;
//         }

//          DateTime IManagerConfigureScheduler.GetNextStartDateTime(Guid tenantId, SchedulerInfo syncSchedulerInfo)
//         {
//             DateTime nextStartTime = DateTime.MinValue;
//             var seletedTime = GetTime(syncSchedulerInfo.SyncTime); 
//             var currentDate = DateTime.UtcNow.ToShortDateString() + " " + seletedTime;
//             DateTime nextDateTime = DateTime.Parse(currentDate);
//           //  var subscriptionId = queryManager.GetSpecificIdByQuery(tenantId, "tenant", tenantId, "TenantSubscription");
//             var objTimeZone = System.TimeZone.CurrentTimeZone.StandardName; //_timeZoneManager.GetDefaultTimeZone(tenantId);
//             var defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById(objTimeZone);
//             var defaultDateTime = TimeZoneInfo.ConvertTimeToUtc(nextDateTime, defaultTimeZone);

//             if (syncSchedulerInfo.RecurrenceType == RecurrencePattern.Daily)
//             {
//                 if (syncSchedulerInfo.Daily.Unit == 1)
//                 {
//                     nextStartTime = defaultDateTime.AddHours(24 * syncSchedulerInfo.Daily.Value.GetValueOrDefault());
//                 }
//                 else
//                 {
//                     var daynumber = (int)defaultDateTime.DayOfWeek;
//                     if (daynumber == 5)
//                     {
//                         nextStartTime = defaultDateTime.AddDays(3);
//                     }else if (daynumber == 6)
//                     {
//                         nextStartTime = defaultDateTime.AddDays(2);
//                     }
//                     else
//                     {
//                         nextStartTime = defaultDateTime.AddDays(1);
//                     }

//                 }
//             }
//             else if (syncSchedulerInfo.RecurrenceType == RecurrencePattern.Weekly)
//             {
//                 var daynumber = (int)getSelectedDay(syncSchedulerInfo.Weekly);
//                 nextStartTime = defaultDateTime.AddDays((7 * (syncSchedulerInfo.Weekly.Value.HasValue ? syncSchedulerInfo.Weekly.Value.Value :0))-1);
//                 nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + daynumber);
//             }
//             else if (syncSchedulerInfo.RecurrenceType == RecurrencePattern.Monthly)
//             {
//                 if (syncSchedulerInfo.Monthly.Unit == 1)
//                 {
//                     nextStartTime = defaultDateTime.AddMonths(syncSchedulerInfo.Monthly.DayValue2.GetValueOrDefault());
//                     int daysInMonth = DateTime.DaysInMonth(nextStartTime.Year, nextStartTime.Month);
//                     if (syncSchedulerInfo.Monthly.DayValue1.GetValueOrDefault() > daysInMonth)
//                     {
//                         syncSchedulerInfo.Monthly.DayValue1 = daysInMonth;
//                     }
//                     nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, syncSchedulerInfo.Monthly.DayValue1.GetValueOrDefault(), nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
//                 }
//                 else
//                 {
//                     nextStartTime = defaultDateTime.AddMonths(syncSchedulerInfo.Monthly.TheValue3.GetValueOrDefault());
//                     nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, 1, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
//                     nextStartTime = nextStartTime.AddDays((7 * syncSchedulerInfo.Monthly.TheValue1.GetValueOrDefault())-1);
//                     nextStartTime = FirstDateOfWeek(nextStartTime);
//                     nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + syncSchedulerInfo.Monthly.TheValue2.GetValueOrDefault());
//                 }
//             }
//             else if (syncSchedulerInfo.RecurrenceType == RecurrencePattern.Yearly)
//             {
//                 if (syncSchedulerInfo.Yearly.Unit == 1)
//                 {
//                     nextStartTime = defaultDateTime.AddYears(syncSchedulerInfo.Yearly.RecurrenceValue.GetValueOrDefault());
//                     nextStartTime = new DateTime(nextStartTime.Year, syncSchedulerInfo.Yearly.OnValue1.GetValueOrDefault(), nextStartTime.Day, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
//                     int daysInMonth = DateTime.DaysInMonth(nextStartTime.Year, nextStartTime.Month);
//                     if (syncSchedulerInfo.Yearly.OnValue2.GetValueOrDefault() > daysInMonth)
//                     {
//                         syncSchedulerInfo.Yearly.OnValue2 = daysInMonth;
//                     }
//                     nextStartTime = new DateTime(nextStartTime.Year, nextStartTime.Month, syncSchedulerInfo.Yearly.OnValue2.GetValueOrDefault(), nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);

//                 }
//                 else
//                 {
//                     nextStartTime = defaultDateTime.AddYears(syncSchedulerInfo.Yearly.RecurrenceValue.GetValueOrDefault());
//                     nextStartTime = new DateTime(nextStartTime.Year, syncSchedulerInfo.Yearly.TheValue3.GetValueOrDefault(), 1, nextStartTime.Hour, nextStartTime.Minute, nextStartTime.Second);
//                     nextStartTime = nextStartTime.AddDays((7 * syncSchedulerInfo.Yearly.TheValue1.GetValueOrDefault())-1);
//                     nextStartTime = FirstDateOfWeek(nextStartTime);
//                     nextStartTime = nextStartTime.AddDays(-(int)nextStartTime.DayOfWeek + syncSchedulerInfo.Yearly.TheValue2.GetValueOrDefault());
//                 }
//             }

//             if (nextStartTime < DateTime.UtcNow)
//                 nextStartTime = DateTime.UtcNow.AddMinutes(1);

//             return nextStartTime;
//         }
//         private static DateTime FirstDateOfWeek(DateTime nextdate)
//         {
//             var firstDate = new DateTime(nextdate.Year, nextdate.Month, nextdate.Day);
//             while (firstDate.DayOfWeek != DayOfWeek.Monday)
//                 firstDate = firstDate.AddDays(-1);

//             return firstDate;
//         }

//         private string GetTime(int value)
//         {
//             string time = null;
//             switch (value)
//             {
//                 case 100:
//                     time = "01:00";
//                     break;
//                 case 130:
//                     time = "01:30";
//                     break;
//                 case 200:
//                     time = "02:00";
//                     break;
//                 case 230:
//                     time = "02:30";
//                     break;
//                 case 300:
//                     time = "03:00";
//                     break;
//                 case 330:
//                     time = "03:30";
//                     break;
//                 case 400:
//                     time = "04:00";
//                     break;
//                 case 430:
//                     time = "04:30";
//                     break;
//                 case 500:
//                     time = "05:00";
//                     break;
//                 case 530:
//                     time = "05:30";
//                     break;
//                 case 600:
//                     time = "06:00";
//                     break;
//                 case 630:
//                     time = "06:30";
//                     break;
//                 case 700:
//                     time = "07:00";
//                     break;
//                 case 730:
//                     time = "7:30";
//                     break;
//                 case 800:
//                     time = "08:00";
//                     break;
//                 case 830:
//                     time = "08:30";
//                     break;
//                 case 900:
//                     time = "09:00";
//                     break;
//                 case 930:
//                     time = "09:30";
//                     break;
//                 case 1000:
//                     time = "10:00";
//                     break;
//                 case 1030:
//                     time = "10:30";
//                     break;
//                 case 1100:
//                     time = "11:00";
//                     break;
//                 case 1130:
//                     time = "11:30";
//                     break;
//                 case 1200:
//                     time = "12:00";
//                     break;
//                 case 1230:
//                     time = "12:30";
//                     break;
//                 case 1300:
//                     time = "13:00";
//                     break;
//                 case 1330:
//                     time = "13:30";
//                     break;
//                 case 1400:
//                     time = "14:00";
//                     break;
//                 case 1430:
//                     time = "14:30";
//                     break;
//                 case 1500:
//                     time = "15:00";
//                     break;
//                 case 1530:
//                     time = "15:30";
//                     break;
//                 case 1600:
//                     time = "16:00";
//                     break;
//                 case 1630:
//                     time = "16:30";
//                     break;
//                 case 1700:
//                     time = "17:00";
//                     break;
//                 case 1730:
//                     time = "17:30";
//                     break;
//                 case 1800:
//                     time = "18:00";
//                     break;
//                 case 1830:
//                     time = "18:30";
//                     break;
//                 case 1900:
//                     time = "19:00";
//                     break;
//                 case 2000:
//                     time = "20:00";
//                     break;
//                 case 2030:
//                     time = "20:30";
//                     break;
//                 case 2100:
//                     time = "21:00";
//                     break;
//                 case 2130:
//                     time = "21:30";
//                     break;
//                 case 2200:
//                     time = "22:00";
//                     break;
//                 case 2230:
//                     time = "22:30";
//                     break;
//                 case 2300:
//                     time = "23:00";
//                     break;
//                 case 2330:
//                     time = "23:30";
//                     break;
//                 case 0:
//                     time = "00:00";
//                     break;
//                 case 30:
//                     time = "00:30";
//                     break;
//             }
//             return time;
//         }

//         private DayOfWeek getSelectedDay(SchedulerWeeklyInfo weekly)
//         {
//             DayOfWeek weekday = 0;
//             if (weekly.Monday)
//             {
//                 weekday = DayOfWeek.Monday;
//             }
//             else if (weekly.Tuesday)
//             {
//                 weekday = DayOfWeek.Tuesday;
//             }
//             else if (weekly.Wednesday)
//             {
//                 weekday = DayOfWeek.Wednesday;
//             }
//             else if (weekly.Thrusday)
//             {
//                 weekday = DayOfWeek.Thursday;
//             }
//             else if (weekly.Friday)
//             {
//                 weekday = DayOfWeek.Friday;
//             }
//             else if (weekly.Saturday)
//             {
//                 weekday = DayOfWeek.Saturday;
//             }
//             else if (weekly.Sunday)
//             {
//                 weekday = DayOfWeek.Sunday;
//             }

//             return weekday;
//         }

//     }
// }