using AutoMapper;
using RacingDAL.Models;
using RacingDTO.Models;
using RacingDTO.RaceWorkerEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Configuration
{
    public class DTOAutomapperProfile:Profile
    {
        public DTOAutomapperProfile()
        {
            //mapping for DAL
            CreateMap<Engine, EngineDTO>().ReverseMap();
            CreateMap<Brake,BrakeDTO>().ReverseMap();
            CreateMap<Suspention, SuspentionDTO>().ReverseMap();
            CreateMap<RacingCar, RacingCarDTO>().ReverseMap();
            CreateMap<Race, RaceDBDTO>().ReverseMap();
            CreateMap<CarStat, CarStatDTO>().ReverseMap();

            //mapping for calculation
            CreateMap<EngineWorker, EngineDTO>().ReverseMap();
            CreateMap<BrakeWorker, BrakeDTO>().ReverseMap();
            CreateMap<SuspentionWorker, SuspentionDTO>().ReverseMap();
            CreateMap<RacingCarWorker, RacingCarDTO>().ReverseMap();
            CreateMap<RaceWorker, RaceDTO > ().ReverseMap();
            CreateMap<CarStatusWorker, CarStatusDTO>().ReverseMap();

        }
    }
}
