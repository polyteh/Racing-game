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
    public class EngineService : GenericService<EngineDTO, Engine>, IEngineService
    {
        public EngineService(IGeneralDBRepository<Engine> rep, IMapper mapper):base(rep)
        {
            _mapper = mapper;
        }
        public override EngineDTO Map(Engine entity)
        {
            return _mapper.Map<EngineDTO>(entity);
        }

        public override Engine Map(EngineDTO blmodel)
        {
            return _mapper.Map<Engine>(blmodel);
        }

        public override IEnumerable<EngineDTO> Map(IEnumerable<Engine> entity)
        {
            return _mapper.Map<IEnumerable<EngineDTO>>(entity);
        }

        public override IEnumerable<Engine> Map(IEnumerable<EngineDTO> entity)
        {
            return _mapper.Map<IEnumerable<Engine>>(entity);
        }
    }
}
