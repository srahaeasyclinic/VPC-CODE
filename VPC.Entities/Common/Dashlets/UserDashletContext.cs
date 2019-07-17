
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Dashlets
{
  public partial class DashletContext
    {        
        [EntityGroup(InfoType.User, "User Dashlet1")]    
        public static Guid UserDashlet1 = new Guid(UserDashletContext.UserDashlet1);

        [EntityGroup(InfoType.User, "User Dashlet2")]    
        public static Guid UserDashlet2 = new Guid(UserDashletContext.UserDashlet2);

        [EntityGroup(InfoType.User, "User Dashlet3")]    
        public static Guid UserDashlet3 = new Guid(UserDashletContext.UserDashlet3);
    }

    internal static class UserDashletContext
    {
         internal const string UserDashlet1 = "94AB4942-E2EA-4777-8436-12EE0ABC6779";
         internal const string UserDashlet2 = "BFAD4630-E103-4000-8046-606DAB67566D";
         internal const string UserDashlet3 = "283B1E53-E82D-4BC8-9B73-4CB67ABF6F91";


    }
}
