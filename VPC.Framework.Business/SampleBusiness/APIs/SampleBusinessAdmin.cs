using System;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.SampleBusiness.Data;

namespace VPC.Framework.Business.SampleBusiness.APIs
{
    public interface ISampleBusinessAdmin
    {
        void CreateSample(SampleEntity sampleEntity);
        void UpdateSample(Guid id, SampleEntity sampleEntity); 
        void DeleteSampleEntity(Guid id);
    }


    public class SampleBusinessAdmin : ISampleBusinessAdmin
    {
        private readonly SampleBusinessData _data = new SampleBusinessData();
        void ISampleBusinessAdmin.CreateSample(SampleEntity sampleEntity)
        {
            _data.CreateSampleEntity(sampleEntity);
        }

        void ISampleBusinessAdmin.UpdateSample(Guid id, SampleEntity sampleEntity)
        {
            _data.UpdateSampleEntity(id, sampleEntity);
        }

        void ISampleBusinessAdmin.DeleteSampleEntity(Guid id)
        {
            _data.DeleteSampleEntity(id);
        }
    }
}