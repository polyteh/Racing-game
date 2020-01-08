using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using RacingDTO.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Configuration
{
    public static class NinjectGlobalConfiguration
    {
        public static void Configure()
        {
            //combine modules from DTO and web layers
            var modules = new INinjectModule[]
                {
                    new WebNinjectConfiguration(),
                    new DTONinjectConfiguration()
               };
            //make new kernel with ctor
            var kernel = new StandardKernel(modules);
            // suppress NinjectModelvalidator for MVC
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}