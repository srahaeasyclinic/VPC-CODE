using System;
using Microsoft.AspNetCore.Mvc;
using VPC.Framework.Business.Credential.Contracts;
using VPC.WebApi.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using VPC.Entities.Credential;
using VPC.Framework.Business.Credential;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.Generic;
namespace VPC.WebApi.Controllers.Login
{
    [Route("api/security")]
    public class AuthorizationController : BaseApiController
    {
        [HttpGet]
        [Route("renewauthorization")]
        public IActionResult Get()
        {
            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            var claims = sqlMembership.RevokeAuthorization(TenantCode, UserId);
            var tokenString = GenerateJSONWebToken(claims);
            if (claims != null)
            {
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        private string GenerateJSONWebToken(Claim[] claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(LoginConfiguration.Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(LoginConfiguration.Configuration["Jwt:Issuer"],
                LoginConfiguration.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}