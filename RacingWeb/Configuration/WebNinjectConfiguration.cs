using AutoMapper;
using Ninject;
using Ninject.Modules;
using RacingDTO.Configuration;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingWeb.Configuration
{
    public class WebNinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEngineService>().To<EngineService>();
            this.Bind<IBrakeService>().To<BrakeService>();
            this.Bind<ISuspentionService>().To<SuspentionService>();
            this.Bind<IRacingCarService>().To<RacingCarService>();
            this.Bind<IRaceService>().To<RaceService>();
            this.Bind<IRaceDBDTOService>().To<RaceDbDTOService>();
            var mapperConfiguration = CreateConfiguration();
            this.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            this.Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        //create common mapper configuration
        private MapperConfiguration CreateConfiguration()
        {
            // var config = new MapperConfiguration(cfg => new DTOAutomapperProfile());
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfile(new DTOAutomapperProfile());
                cfg.AddProfile(new WebAutomapperProfile());
            });
            return config;
        }
    }
}