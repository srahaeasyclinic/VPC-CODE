using System;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.Common;
using VPC.Entities.EntityCore;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Entities.EntityCore.Metadata.Product.Entity;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Query;
using System.Data;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.WorkFlow.Operation.Product
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = InfoType.Product, Key = "ProductActiveVersion", Id = "2C0B96A9-7C2E-411E-ADBE-87FE06CF559C", ProcessType = WorkFlowProcessType.Process)]
    public class ProductCreationPostProcess : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
            IEntityQueryManager _queryManager = new EntityQueryManager();
           // IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage();
        
            try
            {

                objWorkFlowProcessMessage = new WorkFlowProcessMessage { Success = true };
                var workFlowProcessProperties = (WorkFlowProcessProperties)obj[0];
                var jObject = (JObject)obj[1];
                var tenantId = (Guid)obj[2];

                var queryFilter1 = new List<QueryFilter> ();           
                queryFilter1.Add (new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString () });
                queryFilter1.Add (new QueryFilter { FieldName = "ProductId", Operator = "Equal", Value = workFlowProcessProperties.resultId.ToString() });
                var queryContext1 = new QueryContext { Fields = "VersionNo", Filters = queryFilter1, PageSize = 100, PageIndex = 1 };
                DataTable productVersionDataTable = _queryManager.GetResult (tenantId, "productversion", queryContext1);


            
                var productVersionInfo = EntityMapper<ProductVersion>.Mapper(productVersionDataTable);
                var updateStatus = _queryManager.UpdateSpecificField (tenantId, "Product", workFlowProcessProperties.resultId, "ActiveVersion", productVersionInfo.InternalId.Value.ToString());

                //Update WorkFlowFirst step
                IOperationFlowEngine operationEngine = new OperationFlowEngine ();
                var subTypeCode = iMetadataManager.GetSubTypeId ("productversion", "standard");
                var properties = new WorkFlowProcessProperties {resultId=Guid.Parse(productVersionInfo.InternalId.Value), EntityName = "productversion", SubTypeCode = subTypeCode, UserId = workFlowProcessProperties.UserId, IsSuperAdmin = false };
                operationEngine.FirstOperation (tenantId, properties);
                 return objWorkFlowProcessMessage;
            }
            catch (System.Exception ex)
            {
                _log.Error("TenantEmailPostProcess  having exception message" + ex.Message);

                objWorkFlowProcessMessage.Success = false;
                objWorkFlowProcessMessage.ErrorMessage = new ErrorMessage
                {
                    Code = WorkFlowMessage.ApplicationError,
                    Description = ex.Message
                };
                return objWorkFlowProcessMessage;
            }

        }

    }

}
