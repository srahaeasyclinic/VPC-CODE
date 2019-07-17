using System;
using System.Collections.Generic;

namespace VPC.Entities.EntitySecurity
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class AccessLevelAttribute:Attribute
    {
        private string _levelName;
        public AccessLevelAttribute(string levelName)
        {
            _levelName = levelName;
        }

        public string GetAccessLevel()
        {
            return _levelName;
        }
    }

    public static class AccessLevelGuid
    {       
        public const string RoleEntity = "02717D28-815E-4CD4-ACD5-F800627F93EF";
        public const string RoleFunction = "D1B01427-5834-42AB-A7C8-CE9047FE4340";
        public const string RoleReport = "ADBB088A-FCDA-41F1-9C32-C79C3E5DAE2F";
    }
}
