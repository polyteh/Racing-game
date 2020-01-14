using AutoMapper;
using RacingDAL.Interfaces;
using RacingDAL.Models;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.RaceWorkerEngine;
using RacingDTO.RaceWorkerEngine.Interfaces;
using RacingDTO.RaceWorkerEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.Services
{
    public class RaceService : IRaceService
    {
        protected IMapper _mapper;
        private readonly IGeneralDBRepository<RacingCar> _repository;
        public RaceService(IGeneralDBRepository<RacingCar> rep, IMapper mapper)
        {
            _repository = rep;
            _mapper = mapper;
        }
        public void StartRace(RaceDTO newRace)
        {
            IRaceWorker newRaceWorker = new RaceEngine();
            GetCarsForRacing(newRace);
            var raceToStart = _mapper.Map<RaceWorker>(newRace);
            newRaceWorker.StartRace(raceToStart);
        }
        private void GetCarsForRacing(RaceDTO newRace)
        {
            List<RacingCarDTO> fullCarList = new List<RacingCarDTO>();
            foreach (var item in newRace.CarList)
            {
                RacingCar carToRace = _repository.FindById(item.Id);
                fullCarList.Add(_mapper.Map<RacingCarDTO>(carToRace));
            }
            newRace.CarList = fullCarList;
        }
    }
}
