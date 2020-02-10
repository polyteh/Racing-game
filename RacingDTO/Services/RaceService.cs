using AutoMapper;
using Newtonsoft.Json;
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
using System.Diagnostics;
using System.IO;
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
            Debug.WriteLine($"race from service. Start race Working: {_isRunning }");

            //just create file
            string path = @"d:\Education\A-Level\Temp\JSON\raceStatus.json";
            using (FileStream fs = File.Create(path))
            {

            }


            await newRaceWorker.StartRace(raceToStart);
            _isRunning = false;
            Debug.WriteLine($"race from service, Working: {_isRunning }");
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
            string JSONresult = JsonConvert.SerializeObject(raceStatus);
            Debug.WriteLine("before write task");
            Task task1 = new Task(() =>
            {
                string temp_path = @"d:\Education\A-Level\Temp\JSON\raceStatus_temp.json";
                string path = @"d:\Education\A-Level\Temp\JSON\raceStatus.json";
                using (var aFile = new FileStream(temp_path, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (var tw = new StreamWriter(aFile))
                    {
                        //without ToString also works
                        tw.WriteLine(JSONresult.ToString());
                        tw.Close();
                        Debug.WriteLine("write JSON");                     
                    }
                }
                File.Copy(temp_path, path, true);
            });



            //Task task1 = new Task(() => SaveRaceStatusJSON(raceStatus));
            // SaveRaceStatusJSON(raceStatus);
            task1.Start();
            return raceStatus;
        }

        public void InitRace(RaceDTO newRace)
        {
            throw new NotImplementedException();
        }
        public bool IsRunning() => newRaceWorker.IsRaceRunning();

        public void PauseRace()
        {
            newRaceWorker.PauseRace();
        }

        public void ResumeRace()
        {
            newRaceWorker.ResumeRace();
        }
        public void SaveRaceStatusJSON(List<CarStatusDTO> raceStatus)
        {
            //List<CarStatusDTO> raceStatus = _mapper.Map<List<CarStatusDTO>>(newRaceWorker.GetStatus());
            string JSONresult = JsonConvert.SerializeObject(raceStatus);
            string path = @"d:\Education\A-Level\Temp\JSON\raceStatus.json";
            using (var tw = new StreamWriter(path, true))
            {
                //without ToString also works
                tw.WriteLineAsync(JSONresult.ToString());
                tw.Close();
                Debug.WriteLine("write JSON");
            }
        }
    }
}
