using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using VPC.Entities.Credential;
using VPC.Framework.Business.Credential;
using System.Collections.Generic;
namespace VPC.WebApi.Controllers.Login
{
    [Route("api/login")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public IActionResult Login([FromBody] LoginInfo login)
        {
            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            // true-->locked
            if (sqlMembership.AuthenticateUserNameTenantCode(login))
            {
                var passwordpolicy = sqlMembership.getPasswordPolicy(login.TenantCode, true);

                if (passwordpolicy != null)
                {
                    if (!sqlMembership.CheckIfLocked(login, passwordpolicy))
                    {
                        var claims = sqlMembership.AuthenticateUser(login);
                        if (claims != null)
                        {
                            bool boolIsChangePassEnable = sqlMembership.IsChangedPassEnabled(login);
                            var tokenString = GenerateJSONWebToken(claims);
                            if (sqlMembership.CheckPasswordAgeValidity(login, passwordpolicy))
                            {
                                if (boolIsChangePassEnable)
                                {
                                    // response = Ok(new { IsChangedPassEnabled = true, token = tokenString, message = "Your password has been reset upon request" });
                                    response = Ok(new { IsChangedPassEnabled = true, token = tokenString });
                                }
                                else
                                {
                                    response = Ok(new { token = tokenString });
                                }
                            }
                            else
                            {
                                // response = Ok(new { IsChangedPassEnabled = true, token = tokenString, message = "Password has not been changed for a long time, considering changing your password" });
                                response = Ok(new { IsChangedPassEnabled = true, token = tokenString });
                            }

                        }
                        else
                        {
                            if (sqlMembership.LockUserAccount(login, passwordpolicy))
                            {
                                return response = StatusCode((int)HttpStatusCode.InternalServerError, new { message = "You account has been locked down " });
                            }
                        }
                    }
                    else
                    {
                        return response = StatusCode((int)HttpStatusCode.InternalServerError, new { message = "You account has been locked down " });
                    }
                }
                else
                {
                    var claims = sqlMembership.AuthenticateUser(login);
                    if (claims != null)
                    {
                        bool boolIsChangePassEnable = sqlMembership.IsChangedPassEnabled(login);
                        var tokenString = GenerateJSONWebToken(claims);
                        if (boolIsChangePassEnable)
                        {
                            response = Ok(new { IsChangedPassEnabled = true, token = tokenString, message = "Your password has been reset upon request" });
                        }
                        else
                        {
                            response = Ok(new { token = tokenString });
                        }
                    }
                }



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

        [AllowAnonymous]
        [HttpPost]
        [Route("forgotpass")]
        public IActionResult ForgotPassword([FromBody] LoginInfo login)
        {
            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();

            if (sqlMembership.checkAuthorization(login))
            {
                var passwordpolicy = sqlMembership.getPasswordPolicy(login.TenantCode, true);

                if (passwordpolicy != null)
                {

                    if (sqlMembership.CheckPasswordRecoveryStatus(passwordpolicy))
                    {
                        if (sqlMembership.ForgetPasswordUpdateCredential(login, passwordpolicy))
                        {
                            response = Ok(new { message = "Please check inbox" });
                        }
                    }
                    else
                    {
                        return response = StatusCode((int)HttpStatusCode.InternalServerError, new { message = "You cannot recover you password via mail" });
                    }


                }
                else
                {

                    if (sqlMembership.ForgetPasswordUpdateCredential(login, passwordpolicy))
                    {
                        response = Ok(new { message = "Please check inbox" });
                    }


                }
            }



            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("changepass")]
        public IActionResult ChangePassword([FromBody] ChangePasswordInfo changepassword)
        {
            if (string.IsNullOrEmpty(changepassword.TenantCode) || string.IsNullOrEmpty(changepassword.UserName) || string.IsNullOrEmpty(changepassword.OldPassword) || string.IsNullOrEmpty(changepassword.NewPassword))
            {
                return BadRequest("Invalid parameter");
            }
            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            if (sqlMembership.checkAuthorization(changepassword))
            {

                var passwordpolicy = sqlMembership.getPasswordPolicy(changepassword.TenantCode, true);
                List<String> ErrorList = new List<String>();
                if (passwordpolicy != null)
                {
                    ErrorList = sqlMembership.ValidatePassword(changepassword, passwordpolicy);
                }

                if (ErrorList.Count == 0)
                {
                    if (sqlMembership.ChangePasswordUpdateCredential(changepassword))
                    {
                        response = Ok();
                    }

                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorList = ErrorList });
                }
            }


            return response;
        }

        // [HttpPost]
        // [Route("changepassafterlogin")]
        //  public IActionResult ChangePasswordAfterLogin([FromBody]LoginInfo data)
        // {
        //     IActionResult response = Unauthorized();
        //     SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
        //     if(sqlMembership.ChangePasswordSetIsNewAfterLogin(data))
        //     {                
        //         response = Ok();
        //     }
        //     return response;
        // }
        [AllowAnonymous]
        [HttpGet]
        [Route("passwordpolicy/{tenentCode}")]
        public IActionResult PasswordPolicy(string tenentCode)
        {

            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            var passwordpolicy = sqlMembership.getPasswordPolicy(tenentCode, false);
            if (passwordpolicy != null)
            {
                response = Ok(new { data = passwordpolicy });
            }
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("credential/passwordchangedon/{tenentcode}/{username}")]
        public IActionResult UserPasswordChangedOn(string tenentCode, string username)
        {

            IActionResult response = Unauthorized();
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            LoginInfo login = new LoginInfo();
            login.TenantCode = tenentCode;
            login.UserName = username;
            var data = sqlMembership.UserCredentailInfo(login);
         
            if (data != null)
            {

                if (data.PasswordChangedOn == DateTime.MinValue)
                {
                    response = Ok(new { PasswordChangedOn = "",IsNew="" });
                }
                else
                {
                    response = Ok(new { PasswordChangedOn = data.PasswordChangedOn ,IsNew=data.IsNew});
                }

            }
            return response;
        }


        //     [AllowAnonymous]
        //     [Route("checkaccess")]
        //     [HttpPost]
        //    public IActionResult checkAccess([FromBody]LoginInfo data)
        // {
        //     IActionResult response = Unauthorized();
        //     SqlMembershipProvider sqlMembership = new SqlMembershipProvider();          
        //     response = Ok(new{isAccess=sqlMembership.checkAccess(data)});
        //     return response;
        // }

    }
}