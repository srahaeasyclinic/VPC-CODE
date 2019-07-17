using System;
using Microsoft.AspNetCore.Mvc;
using VPC.Framework.Business.Credential.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.Login
{
    [Route("api/credential")]
    public class CredentialController : BaseApiController
    {
        private IManagerCredential _managerCredential;

        public CredentialController(IManagerCredential managerCredential)
        {
            _managerCredential = managerCredential;
        }
    }
}