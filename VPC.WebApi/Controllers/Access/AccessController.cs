using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Metadata;
using VPC.WebApi.Utility;
using VPC.Framework.Business.Credential;
namespace VPC.WebApi.Controllers.Menu
{
    [Route("api/user")]
    public class AccessController : BaseApiController
    {
        [HttpGet]
        [Route("checkpasswordchangeaccess")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            bool result = sqlMembership.CheckPasswordChangeAccess(TenantCode);
            // var a=UserId;
            return Ok(result);
        }
        [HttpGet]
        [Route("username")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UserInfo()
        {
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            User result = sqlMembership.UserInfo(UserId, TenantCode);    
            return Ok(new { username = result.FirstName.Value + " " + result.LastName.Value });
        }
    }
}