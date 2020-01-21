using AutoMapper;
using Ninject;
using Ninject.Modules;
using RacingDAL;
using RacingDAL.Interfaces;
using RacingDAL.Models;
using RacingDTO.Configuration;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
using RacingWeb.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ////db tests
            //IGeneralDBRepository<Engine> engineRepo = new EngineRacingRepository(new RacingDBContext());
            ////Engine myFirstEngine = new Engine() { Name = "R10", HP = 250, Turbine = false, Manufacurer="Renault",Price=1000 };
            ////Engine mySecondEngine = new Engine() { Name = "H10", HP = 200, Turbine = true, Manufacurer = "Honda", Price = 900 };
            ////engineRepo.Create(myFirstEngine);
            ////engineRepo.Create(mySecondEngine);
            //var engine = engineRepo.GetAllAsync();
            //foreach (var item in engine.Result)
            //{
            //    Console.WriteLine(item.HP);
            //}


            ////IGeneralDBRepository<Brake> brakeRepo = new GenericRacingRepository<Brake>(new RacingDBContext());
            ////Brake myFirstBrake = new Brake() { Name = "Br4", EffecientKoef = 4, Manufacurer = "Brembo", Price = 50 };
            ////Brake mySecondBrake = new Brake() { Name = "Br10", EffecientKoef = 10, Manufacurer = "Brembo", Price = 90 };
            ////brakeRepo.Create(myFirstBrake);
            ////brakeRepo.Create(mySecondBrake);

            ////IGeneralDBRepository<Suspention> suspentionRepo = new GenericRacingRepository<Suspention>(new RacingDBContext());
            ///
            //NinjectGlobalConfiguration.Configure();
            var modules = new INinjectModule[]
             {
                    new WebNinjectConfiguration(),
                    new DTONinjectConfiguration()
             };
            //make new kernel with ctor
            var kernel = new StandardKernel(modules);
            var raceService = kernel.Get<RaceService>();

            List<RacingCarDTO> carList = new List<RacingCarDTO>() { new RacingCarDTO() { Id = 1 }, new RacingCarDTO() { Id = 2 }, new RacingCarDTO() { Id = 3 } };
            //List<RacingCarDTO> carList = new List<RacingCarDTO>() { new RacingCarDTO() { Id = 1 } };
            RaceDTO newRace = new RaceDTO() { CarList = carList };
            raceService.StartRace(newRace);

            Thread.Sleep(5000);

            raceService.GetRaceStatus();


            Console.ReadKey();
        }
    }

}
