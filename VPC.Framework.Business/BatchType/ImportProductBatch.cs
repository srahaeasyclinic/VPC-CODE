using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using GemBox.Spreadsheet;
using VPC.Entities.BatchType;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;
using VPC.Entities.EntityCore;
using VPC.Framework.Business.Common;
using Newtonsoft.Json.Linq;

namespace VPC.Framework.Business.BatchType
{
   [BatchType((int)BatchTypeContextEnum.ExportUser)]
    public partial class ImportProductBatch : IBatchTypes
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IEntityQueryManager _queryManager = new EntityQueryManager ();
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
        IManagerBatchItem _managerBatchItem=new ManagerBatchItem();

        void IBatchTypes.OnExecute(dynamic obj)
        {
            VPC.Entities.BatchType.BatchType batchType = (VPC.Entities.BatchType.BatchType)obj[0];
            var tenantId=Guid.Parse(batchType.TenantId.Value);

             try
                 {
                     var allBatchItems=_managerBatchItem.GetBatchItems(tenantId,new Guid(batchType.InternalId.Value),(batchType.ItemRetryCount.Value.Length>0 ? (int?)Int32.Parse(batchType.ItemRetryCount.Value) : (int?)null ));
                      foreach(var allBatchItem in allBatchItems)
                    {                    
                            try
                            {                             
                            
                                 var queryFilter1 = new List<QueryFilter> ();           
                                    queryFilter1.Add (new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString () });            
                                    var queryContext1 = new QueryContext { Fields = "FirstName,LastName", Filters = queryFilter1, PageSize = 100, PageIndex = 1 };
                                    DataTable templatedt = _queryManager.GetResult (tenantId, "user", queryContext1);
                                    

                                _managerBatchItem.BatchItemUpdateStartTime(tenantId,allBatchItem.BatchItemId);                               
                               var excelByte= GenerateExcel(tenantId,batchType,templatedt);
                               
                                //Update batch History
                                _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
                                    BatchHistoryId=Guid.NewGuid(),
                                    BatchItemId=allBatchItem.BatchItemId,
                                    EntityId=allBatchItem.EntityId,
                                    ReferenceId=allBatchItem.ReferenceId,
                                    Status=EmailEnum.Send,
                                    RunTime=allBatchItem.NextRunTime
                                });

                                //Send email for attachment
                                JObject userJObject =new JObject();
                                userJObject.Add(new JProperty("FirstName", "Ajay chouhan"));
                                 var template = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "Emailtemplate", "User", (int)ContextTypeEnum.ExportUser, userJObject);
                                 var returnVal = DataUtility.SaveEmail(tenantId, Guid.Empty, template,"acfidaworld@gmail.com","ExportUser",InfoType.User);

                                _managerBatchItem.BatchContentCreate(tenantId,new Entities.BatchType.BatchItemContent{
                                BatchItemContentId=Guid.NewGuid(),
                                BatchItemId=returnVal,
                                Content=Convert.ToBase64String(excelByte) ,
                                Name="ExportUser",
                                MimeType="application/vnd.ms-excel",
                            
                                });


                            }
                            catch (System.Exception ex)
                            {
                                _log.Error("An error has occurred while sending email", ex.Message);
                                                              
                                //Update batch History
                                 _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
                                    BatchHistoryId=Guid.NewGuid(),
                                      BatchItemId=allBatchItem.BatchItemId,
                                    EntityId=allBatchItem.EntityId,
                                    ReferenceId=allBatchItem.ReferenceId,
                                    Status=EmailEnum.Fail,
                                    RunTime=allBatchItem.NextRunTime,
                                    FailedReason=ex.Message
                                });
                                //Update Batch item status
                                _managerBatchItem.BatchItemUpdate(tenantId,(batchType.ItemRetryCount.Value.Length>0 ? (int?)Int32.Parse(batchType.ItemRetryCount.Value) : (int?)null ),
                                new BatchItem{
                                    BatchItemId=allBatchItem.BatchItemId,
                                    Status=EmailEnum.Fail,                           
                                    FailedReason=ex.Message                                
                                    });
                                //Update Email Status                                
                               // _iEntityResourceManager.UpdateSpecificField(tenantId, "email", allBatchItem.ReferenceId, "Status", ((int)EmailEnum.Fail).ToString());
                                throw ex;
                            }
                         
                      //Update Batch item status
                      _managerBatchItem.BatchItemUpdateStatus(tenantId,new BatchItem{Status=EmailEnum.Send,BatchItemId=allBatchItem.BatchItemId});
                    }

                    
                 }
                catch (System.Exception ex)
                {
                    _log.Error("ImportProductBatch failed", ex.Message);
                }
          
          
        }

    private static byte[] GetBytes(ExcelFile file, SaveOptions options)
    {
        using (var stream = new MemoryStream())
        {
            file.Save(stream, options);
            return stream.ToArray();
        }
    }

    private static SaveOptions GetSaveOptions(string format)
    {
        switch (format.ToUpperInvariant())
        {
            case "XLSX":
                return SaveOptions.XlsxDefault;
            case "XLS":
                return SaveOptions.XlsDefault;
            case "ODS":
                return SaveOptions.OdsDefault;
            case "CSV":
                return SaveOptions.CsvDefault;
            default:
                throw new NotSupportedException("Format '" + format + "' is not supported.");
        }
    }

    private byte[] GenerateExcel(Guid tenantId,VPC.Entities.BatchType.BatchType batchType,DataTable dataTable)
    {
         SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

       // if (!ModelState.IsValid)
         //   return View(model);

         var options = GetSaveOptions("XLSX");
        var workbook = new ExcelFile();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        //var style = worksheet.Rows[0].Style;
        //style.Font.Weight = ExcelFont.BoldWeight;
       // style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
       // worksheet.Columns[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

       // worksheet.Columns[0].SetWidth(50, LengthUnit.Pixel);
        //worksheet.Columns[1].SetWidth(150, LengthUnit.Pixel);
       
        worksheet.Cells["A1"].Value = "First Name";
        worksheet.Cells["B1"].Value = "Last Name";
        

        int rowCount=0;
        foreach (DataRow row in dataTable.Rows)
        {
            int i=0;
            foreach (DataColumn col in dataTable.Columns)
            {
                if(col.ColumnName=="FirstName")
                    worksheet.Cells[rowCount,i].Value = row[col].ToString();

                if(col.ColumnName=="LastName")
                    worksheet.Cells[rowCount,i].Value = row[col].ToString();
                    
                i++;
            } 
            rowCount++;
        }
        var fileBytes=GetBytes(workbook, options);

        //byte[] fileBytes= new byte[stream.Length];

        //stream.Read(fileBytes, 0, fileBytes.Length);
       // stream.Close();
        //Begins the process of writing the byte array back to a file

        // using (Stream file = File.OpenWrite(@"D:\UserExport.XLSX"))
        // {
        // file.Write(fileBytes, 0, fileBytes.Length);
        // }

        return fileBytes;


        //  if (!File.Exists("Create.XLSX"))
        //     {
        //         using (var sw = File.CreateText(Path))
        //         {
        //             sw.WriteLine("Ajay chouhan");
        //         }
        //     }

     

       // return File(GetBytes(workbook, options), options.ContentType, "Create." + model.SelectedFormat.ToLowerInvariant());
  
    }


    }
} 