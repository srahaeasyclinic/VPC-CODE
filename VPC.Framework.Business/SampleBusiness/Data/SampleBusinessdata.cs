using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.SampleBusiness.Data
{
    internal class SampleBusinessData: EntityModelData
    {
        internal List<SampleEntity> GetSampleEntities()
        {
            var lstSanpleEntities= new List<SampleEntity>();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetAllSample");
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        var info = new SampleEntity()
                        {
                            Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                            FullName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            Age = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        };
                        lstSanpleEntities.Add(info);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "SampleEntity::GetAllSample");
            } 
            return lstSanpleEntities;
        }

        internal string getRootTenantCode()
        {
            throw new NotImplementedException();
        }

        internal SampleEntity GetSampleEntityById(Guid id)
        {
            var sanpleEntitiy = new SampleEntity();
            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.GetSampleById");
                cmd.AppendGuid("@guidId", id);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    { 
                        sanpleEntitiy.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        sanpleEntitiy.FullName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        sanpleEntitiy.Age = reader.IsDBNull(2) ? 0 : reader.GetInt32(2); 
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "SampleEntity::GetAllSample");
            }
            return sanpleEntitiy;
        }

        public void CreateSampleEntity(SampleEntity sampleEntity)
        {
            SqlProcedureCommand cmd = CreateProcedureCommand("CreateSample");
            cmd.AppendGuid("@guidId", sampleEntity.Id);
            //cmd.AppendNVarChar("@strFullname", sampleEntity.FullName,100,ParameterDirection.Input);
            cmd.AppendInt("@intAge", sampleEntity.Age); 
            ExecuteCommand(cmd);
        }

        public void UpdateSampleEntity(Guid id, SampleEntity sampleEntity)
        {
            SqlProcedureCommand cmd = CreateProcedureCommand("UpdateSample");
            cmd.AppendGuid("@guidId", sampleEntity.Id);
            //cmd.AppendNVarChar("@strFullname", sampleEntity.FullName, 100, ParameterDirection.Input);
            cmd.AppendInt("@intAge", sampleEntity.Age);
            ExecuteCommand(cmd);
        }

        public void DeleteSampleEntity(Guid id)
        {
            SqlProcedureCommand cmd = CreateProcedureCommand("DeleteSample");
            cmd.AppendGuid("@guidId", id);
            ExecuteCommand(cmd);
        }
    }
}