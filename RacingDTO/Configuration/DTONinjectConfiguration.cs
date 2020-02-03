using AutoMapper;
using Ninject;
using Ninject.Modules;
using RacingDAL;
using RacingDAL.Interfaces;
using RacingDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Configuration
{
    public class DTONinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<RacingDBContext>();
            this.Bind<IGeneralDBRepository<RacingCar>>().To<CarRacingRepository>();
            this.Bind<IGeneralDBRepository<Engine>>().To<EngineRacingRepository>();
            this.Bind<IGeneralDBRepository<Brake>>().To<BrakesRacingRepository>();
            this.Bind(typeof(IGeneralDBRepository<>)).To(typeof(GenericRacingRepository<>));
        }
    }
}
