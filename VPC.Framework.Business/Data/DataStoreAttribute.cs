using System;
using System.Security.Permissions;
using VPC.Framework.Business.Configuration;

namespace VPC.Framework.Business.Data
{ 
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class DataStoreAttribute : Attribute
    {
        private readonly string _dataStore;

        public DataStoreAttribute(string dataStore)
        {
            _dataStore = dataStore;
        }

        public string DataStore
        {
            get { return _dataStore; }
        }

        public string GetConnectionString()
        {
            return ConnectionString.GetConnectionString();
        }
    }
}