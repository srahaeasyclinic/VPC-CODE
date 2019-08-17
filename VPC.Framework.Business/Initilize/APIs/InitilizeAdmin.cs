
using System;
using System.Collections.Generic;
using VPC.Framework.Business.Initilize.Data;

namespace VPC.Framework.Business.Initilize.APIs
{
    public interface IInitilizeAdmin
    {
      bool InitializePickListLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode);
      bool InitializeMedataLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode);
      void InitializePicklistValue(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode, Guid userId);
      void InitializeMenu(Guid rootTenantCode, Guid initilizedTenantCode);
    }


    public class InitilizeAdmin : IInitilizeAdmin
    {
        private readonly InitilizeData _data = new InitilizeData();

        public bool InitializeMedataLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
            return _data.InitializeMedataLayoutsByIds(picklists, rootTenantCode, initilizedTenantCode);
        }

        public bool InitializePickListLayoutsByIds(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode)
        {
           return _data.InitializePickListLayoutsByIds(picklists, rootTenantCode, initilizedTenantCode);
        }

        public void InitializePicklistValue(List<string> picklists, Guid rootTenantCode, Guid initilizedTenantCode, Guid userId)
        {
           _data.InitializePicklistValue(picklists, rootTenantCode, initilizedTenantCode, userId);
        }

        public void InitializeMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
           _data.InitializeMenu (rootTenantCode, initilizedTenantCode);
        }

    }
}