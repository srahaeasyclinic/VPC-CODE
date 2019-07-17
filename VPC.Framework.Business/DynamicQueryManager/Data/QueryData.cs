using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.EntityResourceManager.Data
{
    internal class QueryData : EntityModelData
    {
        
        internal DataTable GetResources(Guid tenantId, string entityName, string query)
        {
          
            var dataTable = new DataTable();
            try
            {
                using (SqlConnection cn = CreateConnection())
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = cn;
                    cn.Open();
                    using (SqlDataReader rs = cmd.ExecuteReader())
                    {
                        dataTable.Load(rs);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "QueryData::GetResources");
            }
            return dataTable;
        }

        internal bool ExecuteUpdateQuery(string queryRes)
        {
            try
            {
                return ExecuteQuery(queryRes);
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "QueryData::UpdateResult");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        internal bool DeleteResult(Guid tenantId, string resourceName, string query)
        {
            try
            {
                return ExecuteQuery(query);
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "QueryData::UpdateResult");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        internal bool SaveResult(Guid tenantId, string entityName, string query)
        {
            try
            {
               return ExecuteQuery(query);
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "QueryData::SaveResult");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

      internal bool UpdateResult(Guid tenantId, string entityName, string query)
        {
            try
            {
                return ExecuteQuery(query);
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "QueryData::UpdateResult");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        private bool ExecuteQuery(string query)
        {
            using (SqlConnection cn = CreateConnection())
            {
                var cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}