using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using VPC.Entities.Common;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Credential.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;
using VPC.Entities.Email;
using VPC.Framework.Business.Credential.APIs;
using System.Text.RegularExpressions;

namespace VPC.Framework.Business.Credential
{
    public class SqlMembershipProvider
    {

        public bool AuthenticateUserNameTenantCode(dynamic login)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();

            if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return false;

            //Get tenant id with code
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            if (tenantId == Guid.Empty)
                return false;

            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId == Guid.Empty)
                return false;

            return true;
        }
        public bool checkAuthorization(dynamic login)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();

            // if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName))
            // {
            //     return false;
            // }

            //Get tenant id with code
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            if (tenantId == Guid.Empty)
            {
                return false;
            }
            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId == Guid.Empty)
            {
                return false;
            }

            return true;
        }
        public Claim[] AuthenticateUser(LoginInfo login)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();

            if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return null;

            //Get tenant id with code
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            if (tenantId == Guid.Empty)
                return null;

            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId == Guid.Empty)
                return null;

            //Validate UserName
            var passwordSaved = crd.GetPassword(tenantId, login.UserName);
            if (passwordSaved == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(login.Password, Convert.FromBase64String(passwordSaved.PasswordHash), Convert.FromBase64String(passwordSaved.PasswordSalt)))
                return null;
            //Get user detail

            var userDetails = roleManager.GetUserDetails(tenantId, userId);

            if (userDetails != null)
            {
                var claims = new[] {
                new Claim ("UserId", userDetails.Id.ToString ()),
                new Claim ("UserName", userDetails.Name),
                new Claim ("TenantId", tenantId.ToString ()),
                new Claim ("IsSuperAdmin", userDetails.IsSuperadmin.ToString ()),
                new Claim ("IsSystemAdmin", userDetails.IsSystemAdmin.ToString ()),
                new Claim ("Jti", Guid.NewGuid ().ToString ())
                };

                return claims;
            }

            return null;
        }



        public Claim[] RevokeAuthorization(Guid tenantId, Guid userId)
        {
            IManagerRole roleManager = new ManagerRole();

            var userDetails = roleManager.GetUserDetails(tenantId, userId);

            if (userDetails != null)
            {
                var claims = new[] {
                new Claim ("UserId", userDetails.Id.ToString ()),
                new Claim ("UserName", userDetails.Name),
                new Claim ("TenantId", tenantId.ToString ()),
                new Claim ("IsSuperAdmin", userDetails.IsSuperadmin.ToString ()),
                new Claim ("IsSystemAdmin", userDetails.IsSystemAdmin.ToString ()),
                new Claim ("Jti", Guid.NewGuid ().ToString ())
                };

                return claims;
            }

            return null;
        }
        public bool IsChangedPassEnabled(LoginInfo login)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            if (tenantId == Guid.Empty)
                return false;
            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId == Guid.Empty)
                return false;
            var credentialData = crd.GetCredential(tenantId, userId);
            return credentialData.IsNew;
        }

        public bool CreateCredential(Guid tenantId, Guid userGuid, LoginInfo login)
        {

            IManagerCredential crd = new ManagerCredential();

            if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return false;

            //Get tenant id with code
            // Guid tenantId=new Guid("1C083115-7DB3-4B92-A449-D57FD1D2A52A");
            if (tenantId == null)
                return false;

            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId != Guid.Empty)
                return false;

            //Validate UserName
            var passwordSaved = crd.GetPassword(tenantId, login.UserName);
            if (passwordSaved != null)
                return false;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(login.Password, out passwordHash, out passwordSalt);

            bool isnew = CheckResetOnFirstLogin(tenantId);
            return crd.Create(tenantId, new CredentialInfo
            {
                CredentialId = Guid.NewGuid(),
                // ParentId=new Guid("E6C7AA71-3C94-46BF-A392-260A14667F95"),
                ParentId = userGuid,
                UserName = login.UserName,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                IsNew = isnew
            });

        }

        internal static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public bool ForgetPasswordUpdateCredential(LoginInfo login, PasswordPolicy passwordpolicy)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();

            if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName))
                return false;
            //Get tenant id with code
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            if (tenantId == Guid.Empty)
                return false;
            //Validate UserName
            var userId = crd.GetUserName(tenantId, login.UserName);
            if (userId == Guid.Empty)
                return false;
            byte[] passwordHash, passwordSalt;
            Random random = new Random();
            int pass = random.Next(1000000);
            //pass = 111;
            CreatePasswordHash(pass.ToString(), out passwordHash, out passwordSalt);
            var userDetails = roleManager.GetUserDetails(tenantId, userId);
            if (userDetails.Id == Guid.Empty)
                return false;
            var credentialData = crd.GetCredential(tenantId, userDetails.Id);


            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            queryFilter.Add(new QueryFilter { FieldName = "InternalId", Operator = "Equal", Value = userDetails.Id.ToString() });
            var queryContext = new QueryContext { Fields = "FirstName,LastName,MiddleName,ContactInformation.WorkEmail1", Filters = queryFilter, PageSize = 100, PageIndex = 1 };
            //  var queryContext = new QueryContext { Fields = "FirstName,LastName", Filters = queryFilter, PageSize = 100, PageIndex = 1 };
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            var dataTableUser = _iEntityResourceManager.GetResultById(tenantId, "user", userDetails.Id, queryContext);
            var userEntity = EntityMapper<VPC.Entities.EntityCore.Metadata.User>.Mapper(dataTableUser);
            var jObject = DataUtility.ConvertToJObjectList(dataTableUser);
            CredentialInfo usercredentialinfo = UserCredentailInfo(login);
            jObject[0].Add(new JProperty("UserCredential.Username", usercredentialinfo.UserName.ToString()));
            jObject[0].Add(new JProperty("UserCredential.Password", pass.ToString()));
            jObject[0].Add(new JProperty("TenantCode", login.TenantCode.ToString()));

            var emailTemplate = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "emailtemplate", "user", (int)ContextTypeEnum.Forgotpassword, jObject[0]);
            if (emailTemplate != null && emailTemplate.Body != null)
            {
                var isnew = false;
                if (passwordpolicy != null)
                {
                    isnew = passwordpolicy.ResetOnFirstLogin.Value;
                }

                crd.Update(tenantId, new CredentialInfo
                {
                    CredentialId = credentialData.CredentialId,
                    ParentId = userDetails.Id,
                    PasswordHash = Convert.ToBase64String(passwordHash),
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    IsNew = isnew
                });
                var returnVal = DataUtility.SaveEmail(tenantId, userDetails.Id, emailTemplate, usercredentialinfo.UserName.ToString(), "ForgetPassword", InfoType.User);
                // SendMail(pass.ToString(),emailTemplate,jdata[0],tenantId,userDetails.Id);
            }
            else
            {
                return false;
            }

            return true;
        }


        // public  bool ChangePasswordSetIsNewAfterLogin(LoginInfo login)
        // {
        //     IManagerCredential crd = new ManagerCredential();
        //     ILayoutManager layoutManager = new LayoutManager();
        //     IManagerRole roleManager = new ManagerRole();

        //     if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName))
        //     return false;
        //     //Get tenant id with code
        //     Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
        //     if (tenantId == Guid.Empty)
        //     return false;
        //     //Validate UserName
        //     var userId = crd.GetUserName(tenantId, login.UserName);
        //     if (userId == Guid.Empty)
        //     return false;
        //     var userDetails = roleManager.GetUserDetails(tenantId, userId);
        //     if (userDetails.Id == Guid.Empty)
        //     return fase;
        //     var credentialData=crd.GetCredential(tenantId,userDetails.Id);
        //   return  crd.SetIsNew(tenantId, new CredentialInfo
        //     {
        //     CredentialId = credentialData.CredentialId,
        //     // ParentId=new Guid("E6C7AA71-3C94-46BF-A392-260A14667F95"),
        //     ParentId = userDetails.Id,
        //     UserName = "",
        //     PasswordHash = "",
        //     PasswordSalt = "",
        //     IsNew = true
        //     });
        // }

        //  private void SendMail(string parampass,Email emailTemplate,JObject data,Guid tenantId, Guid userId)
        //  {
        //      IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager ();
        //     var emailSubType = _iMetadataManager.GetSubTypes ("email");
        //      IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();            
        //     dynamic jsonObject = new JObject ();
        //     jsonObject.Body = emailTemplate.Body.Value;            
        //     jsonObject.Sender = "";
        //     jsonObject.Recipient = data["ContactInformation_Email"];            
        //     jsonObject.Date = HelperUtility.GetCurrentUTCDate();
        //     jsonObject.Subject = emailTemplate.Subject;
        //      var superAdminId = _iEntityResourceManager.SaveResult (tenantId, userId, "email", jsonObject, emailSubType[0].Name.ToString ());

        public User UserInfo(ChangePasswordInfo changepassword)
        {

            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, changepassword.TenantCode);
            var userId = crd.GetUserName(tenantId, changepassword.UserName);
            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            queryFilter.Add(new QueryFilter { FieldName = "InternalId", Operator = "Equal", Value = userId.ToString() });
            var queryContext = new QueryContext { Fields = "FirstName,LastName", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            User userinfo = null;
            DataTable user = _iEntityResourceManager.GetResult(tenantId, "User", queryContext);
            if (user.Rows.Count > 0)
            {
                userinfo = EntityMapper<User>.Mapper(user);
            }
            return userinfo;
        }
        public User UserInfo(Guid userId, Guid tenantId)
        {
            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            queryFilter.Add(new QueryFilter { FieldName = "InternalId", Operator = "Equal", Value = userId.ToString() });
            var queryContext = new QueryContext { Fields = "FirstName,LastName", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            User userinfo = null;
            DataTable user = _iEntityResourceManager.GetResult(tenantId, "User", queryContext);
            if (user.Rows.Count > 0)
            {
                userinfo = EntityMapper<User>.Mapper(user);
            }
            return userinfo;
        }
        public bool ChangePasswordUpdateCredential(ChangePasswordInfo changepassword)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();

            if (string.IsNullOrEmpty(changepassword.TenantCode) || string.IsNullOrEmpty(changepassword.UserName) || string.IsNullOrEmpty(changepassword.OldPassword) || string.IsNullOrEmpty(changepassword.NewPassword))
                return false;

            //Get tenant id with code
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, changepassword.TenantCode);
            if (tenantId == Guid.Empty)
                return false;

            //Validate UserName
            var userId = crd.GetUserName(tenantId, changepassword.UserName);
            if (userId == Guid.Empty)
                return false;

            //Validate UserName
            var passwordSaved = crd.GetPassword(tenantId, changepassword.UserName);
            if (passwordSaved == null)
                return false;
            // check if password is correct
            if (!VerifyPasswordHash(changepassword.OldPassword, Convert.FromBase64String(passwordSaved.PasswordHash), Convert.FromBase64String(passwordSaved.PasswordSalt)))
                return false;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(changepassword.NewPassword, out passwordHash, out passwordSalt);
            CredentialInfo credentialData = crd.GetCredential(tenantId, userId);
            return crd.Update(tenantId, new CredentialInfo
            {
                CredentialId = credentialData.CredentialId,
                ParentId = userId,
                UserName = changepassword.UserName,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                IsNew = false
            });

        }

        public dynamic getPasswordPolicy(string TenantCode, bool ConvertToEntity)
        {
            if (string.IsNullOrEmpty(TenantCode))
                return null;
            ILayoutManager layoutManager = new LayoutManager();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, TenantCode);
            if (tenantId == Guid.Empty)
                return null;
            return getPasswordPolicy(tenantId, ConvertToEntity);
        }

        public dynamic getPasswordPolicy(Guid tenantId, bool ConvertToEntity)
        {
            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            var queryContext = new QueryContext { Fields = "LockoutAttempt,LockoutDuration,PreviousPasswordDifference,ResetOnFirstLogin,IsUppercase,IsLowercase,IsNumber,IsNonAlphaNumeric,CanUserChangeOwnPassword,AllowFirstLastName,PasswordLength,PasswordAge,AllowRecoveryViaMail", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };
            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            DataTable data1 = _iEntityResourceManager.GetResult(tenantId, "passwordpolicy", queryContext);

            if (data1.Rows.Count == 0)
            {
                return null;
            }
            // if (data1.Rows[0]["AllowRecoveryViaMail"] == System.DBNull.Value)
            // {
            //     data1.Rows[0]["AllowRecoveryViaMail"] = true;
            // }
            // data1.Rows[0]["AllowRecoveryViaMail"] = ConvertToTrue(data1.Rows[0]["AllowRecoveryViaMail"]);
            // data1.Rows[0]["isLowercase"] = ConvertToTrue(data1.Rows[0]["isLowercase"]);
            // data1.Rows[0]["isUppercase"] = ConvertToTrue(data1.Rows[0]["isUppercase"]);
            // data1.Rows[0]["allowFirstLastName"] = ConvertToTrue(data1.Rows[0]["allowFirstLastName"]);
            // data1.Rows[0]["isNumber"] = ConvertToTrue(data1.Rows[0]["isNumber"]);
            // data1.Rows[0]["isNonAlphaNumeric"] = ConvertToTrue(data1.Rows[0]["isNonAlphaNumeric"]);
            // if (data1.Rows[0]["PreviousPasswordDifference"] == System.DBNull.Value)
            // {
            //     data1.Rows[0]["PreviousPasswordDifference"] = false;
            // }
            // else
            // {
            //     data1.Rows[0]["PreviousPasswordDifference"] = (bool)data1.Rows[0]["PreviousPasswordDifference"];
            // }
            dynamic passwordpolicy = null;
            if (ConvertToEntity)
            {
                passwordpolicy = EntityMapper<PasswordPolicy>.Mapper(data1);
            }
            else
            {
                passwordpolicy = data1;
            }
            return passwordpolicy;
        }
        internal bool CheckResetOnFirstLogin(Guid TenantCode)
        {
            PasswordPolicy passwordpolicy = getPasswordPolicy(TenantCode, true);
            var isnew = false;
            if (passwordpolicy != null)
            {
                isnew = passwordpolicy.ResetOnFirstLogin.Value;
            }
            return isnew;
        }
        public bool ConvertToTrue(Object data)
        {
            if (data == System.DBNull.Value)
            {
                return true;
            }
            else
            {
                return (bool)data;
            }
        }

        // public bool checkAccess(LoginInfo login)
        // {
        //     IManagerCredential crd = new ManagerCredential();
        //     ILayoutManager layoutManager = new LayoutManager();
        //     IManagerRole roleManager = new ManagerRole();

        //     if (string.IsNullOrEmpty(login.TenantCode) || string.IsNullOrEmpty(login.UserName))
        //     return false;
        //     //Get tenant id with code
        //     Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
        //     if (tenantId == Guid.Empty)
        //     return false;
        //     //Validate UserName
        //     var userId = crd.GetUserName(tenantId, login.UserName);
        //     if (userId == Guid.Empty)
        //     return false;
        //     // var userDetails = roleManager.GetUserDetails(tenantId, userId);
        //     // if (userDetails.Id == Guid.Empty)
        //     // return false;
        //     var credentialData=crd.GetCredential(tenantId,userId);
        //     return credentialData.IsNew;        
        // }

        public List<CredentialHistory> GetCredentialHistory(string tenentCode, string username, int count)
        {
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IReviewCredential ReviewCredential = new ReviewCredential();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, tenentCode);
            Guid userId = crd.GetUserName(tenantId, username);
            List<CredentialHistory> result = ReviewCredential.GetCredentialHistory(tenantId, userId, count);
            return result;
        }

        public CredentialInfo UserCredentailInfo(LoginInfo login)
        {

            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IManagerRole roleManager = new ManagerRole();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
            Guid userId = crd.GetUserName(tenantId, login.UserName);
            CredentialInfo credentialData = crd.GetCredential(tenantId, userId);
            return credentialData;
        }
        public List<String> ValidatePassword(ChangePasswordInfo changepassword, PasswordPolicy passwordpolicy)
        {
            User userInfo = UserInfo(changepassword);
            string pass = changepassword.NewPassword;
            List<String> error = new List<String>();

            if (userInfo != null && passwordpolicy.AllowFirstLastName != null)
            {
                if (!string.IsNullOrEmpty(userInfo.FirstName.Value) || !string.IsNullOrEmpty(userInfo.LastName.Value))
                {
                    if (passwordpolicy.AllowFirstLastName.Value)
                    {
                        if (pass.ToLower().Contains(userInfo.FirstName.Value.ToLower()) || pass.ToLower().Contains(userInfo.LastName.Value.ToLower()))
                        {
                            error.Add("validationFirstLastName");
                        }
                    }
                }

            }

            if (passwordpolicy.IsUppercase != null)
            {
                if (passwordpolicy.IsUppercase.Value)
                {
                    Regex rx = new Regex(@"(?=.*[A-Z])");
                    if (!rx.IsMatch(pass))
                    {
                        error.Add("validationUppercase");
                    }
                }

            }

            if (passwordpolicy.IsLowercase != null)
            {
                if (passwordpolicy.IsLowercase.Value)
                {
                    Regex rx = new Regex(@"(?=.*[a-z])");
                    if (!rx.IsMatch(pass))
                    {
                        error.Add("validationLowercase");
                    }
                }
            }
            if (passwordpolicy.IsNumber != null)
            {
                if (passwordpolicy.IsNumber.Value)
                {
                    Regex rx = new Regex(@"(?=.*\d)");
                    if (!rx.IsMatch(pass))
                    {
                        error.Add("validationNumber");
                    }

                }

            }

            if (passwordpolicy.IsNonAlphaNumeric != null)
            {
                if (passwordpolicy.IsNonAlphaNumeric.Value)
                {
                    Regex rx = new Regex(@"(?=.*[#$@!%&*?])");
                    if (!rx.IsMatch(pass))
                    {
                        error.Add("validationNonAlphaNumeric");
                    }
                }
            }
            if (passwordpolicy.PasswordLength.Value != null)
            {
                // if (!Convert.ToBoolean (result.PasswordLength.Value)) {
                if (pass.Length < Convert.ToInt32(passwordpolicy.PasswordLength.Value.ToString()))
                {
                    error.Add("validationPasswordLength");
                }
                // }

            }
            if (passwordpolicy.PreviousPasswordDifference != null)
            {
                if (passwordpolicy.PreviousPasswordDifference.Value != "")
                {
                    bool isValid = true;
                    int count = Convert.ToInt32(passwordpolicy.PreviousPasswordDifference.Value);
                    List<CredentialHistory> credentiallist = GetCredentialHistory(changepassword.TenantCode, changepassword.UserName, count);
                    if (credentiallist.Count > 0)
                    {
                        for (int i = 0; i < credentiallist.Count; i++)
                        {
                            if (VerifyPasswordHash(pass, Convert.FromBase64String(credentiallist[i].PasswordHash), Convert.FromBase64String(credentiallist[i].PasswordSalt)))
                            {
                                isValid = false;
                                break;
                            }
                        }
                    }
                    if (!isValid)
                        error.Add("validationSamePassword");
                }
            }


            return error;

        }
        public bool CheckPasswordChangeAccess(Guid tenantId)
        {
            PasswordPolicy passwordpolicy = getPasswordPolicy(tenantId, true);
            bool isValid = true;
            List<String> error = new List<String>();
            if (passwordpolicy != null)
            {
                isValid = passwordpolicy.CanUserChangeOwnPassword.Value;
            }

            return isValid;
        }
        public bool CheckIfLocked(LoginInfo login, PasswordPolicy passwordpolicy)
        {
            CredentialInfo usercredentialinfo = UserCredentailInfo(login);
            if (passwordpolicy.LockoutDuration.Value == null)
            {
                return false;
            }
            if (usercredentialinfo.IsLocked)
            {
                //user was locked down now as time is over he is locked out
                if (DateTime.UtcNow > usercredentialinfo.LockedOn.AddMinutes(Convert.ToInt32(passwordpolicy.LockoutDuration.Value)))
                {
                    IManagerCredential managecredential = new ManagerCredential();
                    IManagerCredential crd = new ManagerCredential();
                    ILayoutManager layoutManager = new LayoutManager();
                    IReviewCredential ReviewCredential = new ReviewCredential();
                    Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);
                    var userId = crd.GetUserName(tenantId, login.UserName);
                    managecredential.UpdateLockedStatus(tenantId, usercredentialinfo.CredentialId, false, 0, null);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }
        public bool CheckPasswordRecoveryStatus(PasswordPolicy passwordpolicy)
        {
            bool isValid = false;
            List<String> error = new List<String>();

            if (passwordpolicy.AllowRecoveryViaMail != null)
            {
                isValid = passwordpolicy.AllowRecoveryViaMail.Value;

            }

            return isValid;
        }
        public bool LockUserAccount(LoginInfo login, PasswordPolicy passwordpolicy) //if incorect only
        {
            var isLocked = false;
            IManagerCredential crd = new ManagerCredential();
            ILayoutManager layoutManager = new LayoutManager();
            IReviewCredential ReviewCredential = new ReviewCredential();
            Guid tenantId = layoutManager.GetTenantId(InfoType.Tenant, login.TenantCode);

            CredentialInfo usercredentialinfo = UserCredentailInfo(login);
            if (passwordpolicy.LockoutAttempt.Value != null)
            {
                int? userpermissablelockoutAttemptcount = 1;
                if (usercredentialinfo.InvalidAttemptCount == null || usercredentialinfo.InvalidAttemptCount == 0)
                {
                    usercredentialinfo.InvalidAttemptCount = 1;
                }
                userpermissablelockoutAttemptcount = Convert.ToInt32(passwordpolicy.LockoutAttempt.Value);
                if (usercredentialinfo.InvalidAttemptCount >= userpermissablelockoutAttemptcount)
                {//lock the user
                    IManagerCredential managecredential = new ManagerCredential();
                    managecredential.UpdateLockedStatus(tenantId, usercredentialinfo.CredentialId, true, usercredentialinfo.InvalidAttemptCount, DateTime.UtcNow);
                    isLocked = true;
                }
                else //invalid attempt count increses
                {
                    IManagerCredential managecredential = new ManagerCredential();
                    managecredential.UpdateLockedStatus(tenantId, usercredentialinfo.CredentialId, false, usercredentialinfo.InvalidAttemptCount + 1, null);
                }


            }
            return isLocked;
        }

        public bool CheckPasswordAgeValidity(LoginInfo login, PasswordPolicy passwordpolicy) //if incorect only
        {
            CredentialInfo usercredentialinfo = UserCredentailInfo(login);
            if (passwordpolicy.PasswordAge.Value == null || passwordpolicy.PasswordAge.Value == "" || usercredentialinfo.PasswordChangedOn == DateTime.MinValue)
            {
                return true;
            }

            if (((DateTime.UtcNow - usercredentialinfo.PasswordChangedOn).TotalDays) > Convert.ToInt32(passwordpolicy.PasswordAge.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}