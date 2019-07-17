
using System;
using System.Collections.Generic;
using System.Data;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.SampleBusiness.APIs;

namespace VPC.Framework.Business.SampleBusiness.Contracts
{
    public interface ISampleBusinessManager
    {
        Guid Create(SampleEntity sampleEntity);
        bool Update(Guid id, SampleEntity sampleEntity);
        bool Delete(Guid id);
        List<SampleEntity> GetAll();
        DataTable GetDynamicData();
        SampleEntity GetById(Guid id);
    }

    public sealed class SampleBusinessManager: ISampleBusinessManager
    {
        private readonly ISampleBusinessAdmin _admin;
        private readonly ISampleBusinessReview _review;

        public SampleBusinessManager()
        {
            _admin = new SampleBusinessAdmin();
            _review = new SampleBusinessReview();
        } 
        Guid ISampleBusinessManager.Create(SampleEntity sampleEntity)
        {
            var id = sampleEntity.Id;
            _admin.CreateSample(sampleEntity);
            return id;
        }

        bool ISampleBusinessManager.Update(Guid id, SampleEntity sampleEntity)
        {
            _admin.UpdateSample(id, sampleEntity);
            return true;
        }

        bool ISampleBusinessManager.Delete(Guid id)
        {
            _admin.DeleteSampleEntity(id);
            return true;
        }

        List<SampleEntity> ISampleBusinessManager.GetAll()
        {
            return _review.GetAllSampleEntities();
        }

        DataTable ISampleBusinessManager.GetDynamicData()
        {
            //var query = "SELECT Id,Name,Age FROM dbo.Sample ";
            //return DataUtility.ExecuteDynamicReader(query);
            throw new NotImplementedException();
        }

        SampleEntity ISampleBusinessManager.GetById(Guid id)
        {
            return _review.GetSampleEntityById(id);
        }
    }
}