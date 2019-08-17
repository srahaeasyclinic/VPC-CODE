using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Email;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.BatchType;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.InitilizationController
{
    [Route("api/initialization")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public class InitilizationController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IInitilizeManager _initilizeManager;
        private readonly IManagerBatchType batch=new ManagerBatchType();
        public InitilizationController(IInitilizeManager initilizeManager)
        {
            _initilizeManager = initilizeManager;
        }


        //  [HttpPost("ajay")]
        // [ProducesResponseType(200)]
        // [ProducesResponseType(500)]
        // [ProducesResponseType(404)]
        // public IActionResult Initialize()
        // {
        //     try
        //     {
        //           try
        //         {
        //             var tenantId=TenantCode;

        //       ISettingManager _iSettingManager = new SettingManager();
        //         IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();
        //         IManagerBatchItem _managerBatchItem=new ManagerBatchItem();

        //         var batchType=batch.GetBatchTypeByContext(tenantId,BatchTypeContextEnum.Email.ToString());           
            
        //         var result = _iSettingManager.GetSettingsByContext(tenantId, SettingContextTypeEnum.EMAIL);
        //         if (result != null)
        //         {
        //             EmailSenderOptions options = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailSenderOptions>(result.Content);
        //             //Get Batch Item

        //             var allBatchItems=_managerBatchItem.GetBatchItems(tenantId,(batchType.ItemRetryCount.HasValue ? batchType.ItemRetryCount.Value : (int?)null ));
        //             foreach(var allBatchItem in allBatchItems)
        //             {
        //               var batchContents=  _managerBatchItem.GetBatchContents(tenantId,allBatchItem.BatchItemId);
        //               if(batchContents.Count>0)
        //               {
        //                   foreach(var batchContent in batchContents)
        //                   {
        //                     try
        //                     {
        //                         var email= JsonConvert.DeserializeObject<Email>(batchContent.Content);                                
        //                         SendEmail(email.Recipient.Value, email.Subject.Value, email.Body.Value, options);
        //                         //Update Batch Content
        //                         _managerBatchItem.BatchContentStatus(tenantId,new BatchItemContent{Status=EmailEnum.Send,BatchItemContentId=batchContent.BatchItemContentId});
        //                         //Update batch History
        //                         _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
        //                             BatchHistoryId=Guid.NewGuid(),
        //                             BatchItemContentId=batchContent.BatchItemContentId,
        //                             EntityId=allBatchItem.EntityId,
        //                             ReferenceId=allBatchItem.ReferenceId,
        //                             Status=EmailEnum.Send,
        //                             RunTime=allBatchItem.NextRunTime
        //                         });
        //                         //Update Email Status                                
        //                         _iEntityResourceManager.UpdateSpecificField(tenantId, "email", allBatchItem.ReferenceId, "Status", ((int)EmailEnum.Send).ToString());

        //                     }
        //                     catch (System.Exception ex)
        //                     {
        //                         _log.Error("An error has occurred while sending email", ex.Message);

        //                         //Update Batch Content
        //                         _managerBatchItem.BatchContentFailMessage(tenantId,new BatchItemContent{FailedReason=ex.Message});
        //                          //Update Batch Content
        //                         _managerBatchItem.BatchContentStatus(tenantId,new BatchItemContent{Status=EmailEnum.Fail,BatchItemContentId=batchContent.BatchItemContentId});
        //                         //Update batch History
        //                          _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
        //                             BatchHistoryId=Guid.NewGuid(),
        //                             BatchItemContentId=batchContent.BatchItemContentId,
        //                             EntityId=allBatchItem.EntityId,
        //                             ReferenceId=allBatchItem.ReferenceId,
        //                             Status=EmailEnum.Fail,
        //                             RunTime=allBatchItem.NextRunTime,
        //                             FailedReason=ex.Message
        //                         });
        //                         //Update Batch item status
        //                         _managerBatchItem.BatchItemUpdate(tenantId,(batchType.ItemTimeout.HasValue ? batchType.ItemTimeout.Value : (int?)null),
        //                         new BatchItem{
        //                             BatchItemId=allBatchItem.BatchItemId,
        //                             Status=EmailEnum.Fail,                           
        //                             FailedReason=ex.Message                                
        //                             });
        //                         //Update Email Status                                
        //                         _iEntityResourceManager.UpdateSpecificField(tenantId, "email", allBatchItem.ReferenceId, "Status", ((int)EmailEnum.Fail).ToString());
        //                         throw ex;
        //                     }
        //                   }
        //               }
        //               //Update Batch item status
        //               _managerBatchItem.BatchItemUpdateStatus(tenantId,new BatchItem{Status=EmailEnum.Send,BatchItemId=allBatchItem.BatchItemId});
        //             }
        //         }
        //         else
        //         {
        //             _log.Error("Email gateway not configured");
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _log.Error("Email send failed", ex.Message);
        //     }
      

              
        //        return Ok(true);
        //     }
        //     catch (FieldAccessException ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

        [HttpPost("tenants/{tenantCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Initialize([FromRoute] Guid tenantCode)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called InitilizationController Initialize with tenantCode {0}=", tenantCode);

                if (tenantCode != Guid.Empty)
                {
                    return BadRequest("tenant code required!");
                }

                //string tenantcode = "GCTT001";
                _initilizeManager.Initilize(tenantCode, new List<string> { "EN10003" }, UserId,Guid.Empty);

                stopwatch.StopAndLog("Initialize method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

         [HttpPost("workflows")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult InitializeRootTenantWorkFlow()
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);              
                _initilizeManager.InitializeRootTenantWorkFlow(TenantCode);
                stopwatch.StopAndLog("InitializeRootTenantWorkFlow method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost("workflows/{entityName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult InitializeRootTenantWorkFlow(string entityName)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);              
                _initilizeManager.InitializeRootTenantWorkFlow(TenantCode,entityName);
                stopwatch.StopAndLog("InitializeRootTenantWorkFlow method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

          private void SendEmail(string toEmail, string subject, string message, EmailSenderOptions Options)
        {
            MailMessage mail = GetMailMessage(toEmail, subject, message,
                Options.emailSender, Options.emailUserName, Options.useHtml);
            SmtpClient client = GetSmtpClient(Options.emailServer, Options.emailPort, Options.requiresAuthentication,
                Options.emailEmail, Options.emailPassword, Options.useSsl);

            client.Send(mail);
        }

        private static MailMessage GetMailMessage(string toEmail, string subject, string message,
            string defaultSenderEmail, string defaultSenderDisplayName = null, bool useHtml = true)
        {
            MailAddress sender;

            if (string.IsNullOrEmpty(defaultSenderEmail))
            {
                throw new ArgumentException("No sender mail address was provided");
            }
            else
            {
                sender = !string.IsNullOrEmpty(defaultSenderDisplayName) ?
                    new MailAddress(defaultSenderEmail, defaultSenderDisplayName) : new MailAddress(defaultSenderEmail);
            }

            MailMessage mail = new MailMessage()
            {
                From = sender,
                Subject = subject,
                Body = message,
                IsBodyHtml = useHtml
            };
            mail.To.Add(toEmail);
            return mail;
        }

        private static SmtpClient GetSmtpClient(string host, int port, bool requiresAuthentication = true,
            string userName = null, string userKey = null, bool useSsl = false)
        {
            SmtpClient client = new SmtpClient();

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("No domain was provided");
            }

            client.Host = host;

            if (port > -1)
            {
                client.Port = port;
            }

            client.UseDefaultCredentials = !requiresAuthentication;

            if (requiresAuthentication)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException("No user name was provided");
                }

                client.Credentials = new NetworkCredential(userName, userKey);
            }

            client.EnableSsl = useSsl;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            return client;
        }
   
   

    }
}