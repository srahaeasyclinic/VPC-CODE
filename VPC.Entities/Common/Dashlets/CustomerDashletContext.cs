
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Dashlets
{
  public partial class DashletContext
    {        
        [EntityGroup(InfoType.Customer, "Customer Dashlet1")]    
        public static Guid CustomerDashlet1 = new Guid(CustomerDashletContext.CustomerDashlet1);

        [EntityGroup(InfoType.Customer, "Customer Dashlet2")]    
        public static Guid CustomerDashlet2 = new Guid(CustomerDashletContext.CustomerDashlet2);

        [EntityGroup(InfoType.Customer, "Customer Dashlet3")]    
        public static Guid CustomerDashlet3 = new Guid(CustomerDashletContext.CustomerDashlet3);
    }

    internal static class CustomerDashletContext
    {
         internal const string CustomerDashlet1 = "7644C62E-E4B8-42AF-89FA-B004CF401727";
         internal const string CustomerDashlet2 = "E59E8D46-4EDD-4685-B476-F783FA4AA533";
         internal const string CustomerDashlet3 = "DE39C3A5-40DD-427B-A740-C887FE9245AF";


    }
}
