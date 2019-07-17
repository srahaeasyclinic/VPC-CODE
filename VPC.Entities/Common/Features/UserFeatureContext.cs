
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Features
{
  public partial class FeatureContext
    {        
        [EntityGroup(InfoType.User, "User Feature1")]    
        public static Guid UserFeature1 = new Guid(UserFeatureContext.UserFeature1);

        [EntityGroup(InfoType.User, "User Feature2")]    
        public static Guid UserFeature2 = new Guid(UserFeatureContext.UserFeature2);

        [EntityGroup(InfoType.User, "User Feature3")]    
        public static Guid UserFeature3 = new Guid(UserFeatureContext.UserFeature3);
    }

    internal static class UserFeatureContext
    {
         internal const string UserFeature1 = "571CB021-9F2B-4F80-BC88-9B6FADF4DB92";
         internal const string UserFeature2 = "00D2296C-6599-4F6F-853D-03984E37DDD5";
         internal const string UserFeature3 = "F6BD6294-DF73-41CD-9428-57002FD4B0A2";


    }
}
