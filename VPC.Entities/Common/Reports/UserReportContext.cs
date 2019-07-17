
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Reports
{
  public partial class ReportContext
    {        
        [EntityGroup(InfoType.User, "User Report1")]    
        public static Guid UserReport1 = new Guid(UserReportContext.UserReport1);

        [EntityGroup(InfoType.User, "User Report2")]    
        public static Guid UserReport2 = new Guid(UserReportContext.UserReport2);

        [EntityGroup(InfoType.User, "User Report3")]    
        public static Guid UserReport3 = new Guid(UserReportContext.UserReport3);
    }

    internal static class UserReportContext
    {
         internal const string UserReport1 = "E2ED907E-55E4-4F56-A875-CBA4D39A8A11";
         internal const string UserReport2 = "7CF2D7DE-D8A2-41E1-B929-2FFB190BBF5C";
         internal const string UserReport3 = "4F2E153D-D11B-420E-82BF-9485A97F80C5";


    }
}
