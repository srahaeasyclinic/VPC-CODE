
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Features
{
  public partial class FeatureContext
    {        
        [EntityGroup(InfoType.Customer, "Customer Feature1")]    
        public static Guid CustomerFeature1 = new Guid(CustomerFeatureContext.CustomerFeature1);

        [EntityGroup(InfoType.Customer, "Customer Feature2")]    
        public static Guid CustomerFeature2 = new Guid(CustomerFeatureContext.CustomerFeature2);

        [EntityGroup(InfoType.Customer, "Customer Feature3")]    
        public static Guid CustomerFeature3 = new Guid(CustomerFeatureContext.CustomerFeature3);
    }

    internal static class CustomerFeatureContext
    {
         internal const string CustomerFeature1 = "0118831D-73F2-4C74-A3E5-D5B63B8C9454";
         internal const string CustomerFeature2 = "ECC1BB60-F44C-440E-AA8E-E10B98536D02";
         internal const string CustomerFeature3 = "24CAE8FC-C35F-44F1-B3A6-85FAF157A242";


    }
}
