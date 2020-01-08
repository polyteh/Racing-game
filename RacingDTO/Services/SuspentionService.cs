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
    public class SuspentionService : GenericService<SuspentionDTO, Suspention>, ISuspentionService
    {
        public SuspentionService(IGeneralDBRepository<Suspention> rep, IMapper mapper) : base(rep)
        {
            _mapper = mapper;
        }
        public override SuspentionDTO Map(Suspention entity)
        {
            return _mapper.Map<SuspentionDTO>(entity);
        }

        public override Suspention Map(SuspentionDTO blmodel)
        {
            return _mapper.Map<Suspention>(blmodel);
        }

        public override IEnumerable<SuspentionDTO> Map(IEnumerable<Suspention> entity)
        {
            return _mapper.Map<IEnumerable<SuspentionDTO>>(entity);
        }

        public override IEnumerable<Suspention> Map(IEnumerable<SuspentionDTO> entity)
        {
            return _mapper.Map<IEnumerable<Suspention>>(entity);
        }
    }
}
