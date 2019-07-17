
using System;
using System.ComponentModel;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Functions
{
  public partial class FunctionContext
    {        
       [EntityGroup(InfoType.User, "Password")]    
        public static Guid Password = new Guid(UserFunctionContexts.Password);

        [EntityGroup(InfoType.User, "Qualification")]    
        public static Guid Qualification = new Guid(UserFunctionContexts.Qualification);

        [EntityGroup(InfoType.User, "Designation")]    
        public static Guid Designation = new Guid(UserFunctionContexts.Designation);
    }

    internal static class UserFunctionContexts
    {
        internal const string Password = "1EC0B1F5-F9B6-4D47-B361-40EFEA258FD0";

         internal const string Qualification = "A369BCF2-A11E-49AF-B72E-B799EF49E653";
         internal const string Designation = "74511CCA-1814-4E8E-B922-1A9F1B1D45F7";


    }
}
