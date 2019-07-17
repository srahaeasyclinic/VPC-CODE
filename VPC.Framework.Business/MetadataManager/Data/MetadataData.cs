

using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;


namespace VPC.Framework.Business.MetadataManager.Data
{
    internal sealed class MetadataData
    {
        //readonly Logger _logger = LogManager.GetCurrentClassLogger();

        // private readonly List<LayoutModel> myList = new List<LayoutModel> {
        //     new LayoutModel
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = "temp1",
        //         CreatedBy = Guid.NewGuid(),
        //         CreatedDate = DateTime.Now,
        //         LayoutType = 1,
        //         Subtype = 1,
        //         Context  = 1
        //     },
        //     new LayoutModel
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = "temp2",
        //         CreatedBy = Guid.NewGuid(),
        //         CreatedDate = DateTime.Now,
        //          LayoutType = 1,
        //         Subtype = 1,
        //         Context  = 1
        //     }
        // };

        public MetadataData()
        {

        }

        public bool SaveLayout(Guid tenantId, LayoutModel layout)
        {
           return false;
        }

        public bool UpdateLayoutDetails(Guid TenantId, LayoutModel templateModel)
        {
            return false;
        }

        public bool DeleteLayoutDetails(Guid TenantId, Guid LayoutId)
        {
            return false;
        }

        public bool UpdateDefaultLayout(Guid TenantId, Guid layoutId, int layoutType, Guid UserId)
        {
            return false;
        }

        public List<LayoutModel> GetLayoutsByEntityName(Guid tenantId, Guid entityContext)
        {
            return null;
        }

        public LayoutModel GetLayoutsDetailsById(Guid tenantId, Guid layoutId)
        {
           return null;
        }

        public LayoutModel GetLayoutByName(Guid tenantId, string templateName, int layoutType)
        {
            return null;
        }

        // // private static LayoutModel ReadLayout(SqlDataReader reader)
        // // {
        // //     return null;
        // // }
    }
}
