﻿using AutoMapper;
using RacingDTO.Models;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RacingWeb.Configuration
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<EngineView, EngineDTO>().ReverseMap();
            CreateMap<BrakeView, BrakeDTO>().ReverseMap();
            CreateMap<SuspentionView, SuspentionDTO>().ReverseMap();
            CreateMap<RacingCarView, RacingCarDTO>().ReverseMap();
            CreateMap<SimpleCarView, RacingCarDTO>().ReverseMap();
            CreateMap<RaceView, RaceDTO>().ReverseMap();
            CreateMap<SimpleCarView, CarStatusView>().ReverseMap();
            CreateMap<CarStatusView, CarStatusDTO>().ReverseMap();
            CreateMap<CarStatusView, RacingCarDTO>().ReverseMap();


            CreateMap<RaceDBDTO, RaceViewDB>().ReverseMap();
            CreateMap<CarStatDTO, CarStatView>().ReverseMap();
        }
    }
}

