using AutoMapper;
using RacingDAL.Models;
using RacingDTO.Models;
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
            CreateMap<Engine, EngineDTO>().ReverseMap();
            CreateMap<Brake,BrakeDTO>().ReverseMap();
            CreateMap<Suspention, SuspentionDTO>().ReverseMap();
            CreateMap<RacingCar, RacingCarDTO>().ReverseMap();
        }
    }
}
