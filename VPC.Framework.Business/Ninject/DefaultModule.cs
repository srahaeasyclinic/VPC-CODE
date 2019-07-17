using Ninject.Modules;
using VPC.Framework.Business.SampleBusiness.APIs;
using VPC.Framework.Business.SampleBusiness.Contracts;

namespace VPC.Framework.Business.Ninject
{
    public class DefaultModule:NinjectModule
    {
        public override void Load()
        {
            //#region Sample
            //Bind<ISampleBusinessReview>().To<SampleBusinessReview>();
            //Bind<ISampleBusinessManager>().To<SampleBusinessManager>();
            //#endregion
        }
    }
}