using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace VPC.WebApi.Ninject
{
    public sealed class DelegatingControllerActivator : IControllerActivator
    {
        private readonly Func<ControllerContext, object> _controllerCreator;
        private readonly Action<ControllerContext, object> _controllerReleaser;

        public DelegatingControllerActivator(Func<ControllerContext, object> controllerCreator,
            Action<ControllerContext, object> controllerReleaser = null)
        {
            this._controllerCreator = controllerCreator ??
                                     throw new ArgumentNullException(nameof(controllerCreator));
            this._controllerReleaser = controllerReleaser ?? ((_, __) => { });
        }

        public object Create(ControllerContext context) => this._controllerCreator(context);
        public void Release(ControllerContext context, object controller) =>
            this._controllerReleaser(context, controller);
    }


    public sealed class DelegatingViewComponentActivator : IViewComponentActivator
    {
        private readonly Func<Type, object> _viewComponentCreator;
        private readonly Action<object> _viewComponentReleaser;

        public DelegatingViewComponentActivator(Func<Type, object> viewComponentCreator,
            Action<object> viewComponentReleaser = null)
        {
            this._viewComponentCreator = viewComponentCreator ??
                                        throw new ArgumentNullException(nameof(viewComponentCreator));
            this._viewComponentReleaser = viewComponentReleaser ?? (_ => { });
        }

        public object Create(ViewComponentContext context) =>this._viewComponentCreator(context.ViewComponentDescriptor.TypeInfo.AsType());

        public void Release(ViewComponentContext context, object viewComponent) =>this._viewComponentReleaser(viewComponent);
    }
}