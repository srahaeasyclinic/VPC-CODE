// using Microsoft.AspNetCore.Mvc;
// using NLog;
// using System;
// using System.Net;
// using VPC.Entities.Common;
// using VPC.Framework.Business.Task.UserTask;
// using VPC.WebApi.Utility;


// namespace VPC.WebApi.Controllers.WorkFlow
// {
//     [Route("api/user")]
//     public class ProductTaskController : BaseApiController
//     {
//         private readonly Logger _log= LogManager.GetCurrentClassLogger();
//         private readonly  IUserTaskManager _userTaskManager;
//         public ProductTaskController(IUserTaskManager userTaskManager)
//         {
//             _userTaskManager=userTaskManager; 
//         }   

//         [HttpPut]
//         [Route("tasks/productVersionCheckout")]
//         public IActionResult ProductVersionCheckout([FromBody] ItemName info)
//         {
//             try
//             { 
//                // var retVal = _userTaskManager.ResetPassword(TenantCode,info.Id);             
//                 return Ok("ok");
//             }
//             catch (Exception ex)
//             {
//                 _log.Error(ExceptionFormatter.SerializeToString(ex));
//                 return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
//             }
//         }
     
        
//     }
// }