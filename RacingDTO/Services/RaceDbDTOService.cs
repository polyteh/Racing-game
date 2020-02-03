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
    public class RaceDbDTOService : GenericService<RaceDBDTO, Race>, IRaceDBDTOService
    {
        public RaceDbDTOService(IGeneralDBRepository<Race> rep, IMapper mapper) : base(rep)
        {
            _mapper = mapper;
        }
        public override RaceDBDTO Map(Race entity)
        {
            return _mapper.Map<RaceDBDTO>(entity);
        }

        public override Race Map(RaceDBDTO blmodel)
        {
            return _mapper.Map<Race>(blmodel);
        }

        public override IEnumerable<RaceDBDTO> Map(IEnumerable<Race> entity)
        {
            return _mapper.Map<IEnumerable<RaceDBDTO>>(entity);
        }

        public override IEnumerable<Race> Map(IEnumerable<RaceDBDTO> entity)
        {
            return _mapper.Map<IEnumerable<Race>>(entity);
        }
    }
}
