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
    public class BrakeService : GenericService<BrakeDTO, Brake>, IBrakeService
    {
        public BrakeService(IGeneralDBRepository<Brake> rep, IMapper mapper) : base(rep)
        {
            _mapper = mapper;
        }
        public override BrakeDTO Map(Brake entity)
        {
            return _mapper.Map<BrakeDTO>(entity);
        }

        public override Brake Map(BrakeDTO blmodel)
        {
            return _mapper.Map<Brake>(blmodel);
        }

        public override IEnumerable<BrakeDTO> Map(IEnumerable<Brake> entity)
        {
            return _mapper.Map<IEnumerable<BrakeDTO>>(entity);
        }

        public override IEnumerable<Brake> Map(IEnumerable<BrakeDTO> entity)
        {
            return _mapper.Map<IEnumerable<Brake>>(entity);
        }
    }
}
