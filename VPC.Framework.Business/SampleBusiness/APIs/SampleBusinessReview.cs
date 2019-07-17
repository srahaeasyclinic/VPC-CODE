using System;
using System.Collections.Generic;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.SampleBusiness.Data;

namespace VPC.Framework.Business.SampleBusiness.APIs
{
    public interface ISampleBusinessReview
    {
        List<SampleEntity> GetAllSampleEntities();
        SampleEntity GetSampleEntityById(Guid id);
    }
    public class SampleBusinessReview: ISampleBusinessReview
    {
        private readonly SampleBusinessData _data = new SampleBusinessData();
        List<SampleEntity> ISampleBusinessReview.GetAllSampleEntities()
        {
            return _data.GetSampleEntities();
        }

        SampleEntity ISampleBusinessReview.GetSampleEntityById(Guid id)
        {
            return _data.GetSampleEntityById(id);
        }
    }
}