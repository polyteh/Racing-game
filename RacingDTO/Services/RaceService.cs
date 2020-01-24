using AutoMapper;
using Ninject;
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
        private IRaceWorker newRaceWorker;
        private bool _isRunning;
        [Inject]
        public RaceService(IGeneralDBRepository<RacingCar> rep, IMapper mapper)
        {
            _repository = rep;
            _mapper = mapper;
        }
        public async Task StartRace(RaceDTO newRace)
        {
            newRaceWorker = new RaceEngine();
            GetCarsForRacing(newRace);
            var raceToStart = _mapper.Map<RaceWorker>(newRace);
            _isRunning = true;
            await newRaceWorker.StartRace(raceToStart);
           // _isRunning = false;
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
        public List<CarStatusDTO> GetRaceStatus()
        {
            List<CarStatusDTO> raceStatus = _mapper.Map<List<CarStatusDTO>>(newRaceWorker.GetStatus());
            return raceStatus;
        }

        public void InitRace(RaceDTO newRace)
        {
            throw new NotImplementedException();
        }
        public bool isRunning() => _isRunning;
    }
}
