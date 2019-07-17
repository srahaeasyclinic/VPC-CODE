using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
   
    internal static class RoleMapper
    {   

        internal static List<AccessLevel> GetAccessLevels(List<RoleAccessOption>accessLevel, Guid types)
        {
            List<AccessLevel> levels = new List<AccessLevel>();           
            foreach (var entry in accessLevel)
            {
                var index = entry.AvailableGroups.FindIndex(t => t.Equals(types));
                if (index <= -1) continue;                
                levels.Add(new AccessLevel
                {
                    Id = entry.Id,
                    Name = entry.Name
                });
            }
            return levels;
        } 
    }
}