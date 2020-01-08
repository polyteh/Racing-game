using AutoMapper;
using RacingDAL.Interfaces;
using RacingDAL.Models;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Services
{
    public class RacingCarService : GenericService<RacingCarDTO, RacingCar>, IRacingCarService
    {
        public RacingCarService(IGeneralDBRepository<RacingCar> rep, IMapper mapper) : base(rep)
        {
            _mapper = mapper;
        }
        public override RacingCarDTO Map(RacingCar entity)
        {
            return _mapper.Map<RacingCarDTO>(entity);
        }

        public override RacingCar Map(RacingCarDTO blmodel)
        {
            return _mapper.Map<RacingCar>(blmodel);
        }

        public override IEnumerable<RacingCarDTO> Map(IEnumerable<RacingCar> entity)
        {
            return _mapper.Map<IEnumerable<RacingCarDTO>>(entity);
        }

        public override IEnumerable<RacingCar> Map(IEnumerable<RacingCarDTO> entity)
        {
            return _mapper.Map<IEnumerable<RacingCar>>(entity);
        }
    }
}
