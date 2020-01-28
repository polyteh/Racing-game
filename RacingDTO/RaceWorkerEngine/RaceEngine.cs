using RacingDTO.RaceWorkerEngine.Interfaces;
using RacingDTO.RaceWorkerEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine
{
    public class RaceEngine : IRaceWorker
    {
        // private readonly RaceConfiguration _raceConfiguration;
        RaceWorker _newRace;
        bool _isRaceRunning = false;
        public RaceEngine()
        {

        }
        public async Task StartRace(RaceWorker newRace)
        {
            _newRace = newRace;
            CarNormalization();
            Task[] carsInTheRace = new Task[_newRace.CarList.Count()];

            _isRaceRunning = true;
            //сделать Move асинхронно
            var tasks = _newRace.CarList.Select(car => Task.Run(() =>
            {
                car.Move(new RaceConfiguration());
            })).ToList();

            await Task.WhenAll(tasks);

            //Parallel.For(0, _newRace.CarList.Count(), (i) =>
            //{
            //    carsInTheRace[i] = new Task(() =>
            //    {
            //        _newRace.CarList[i].GetRaceConfiguration(new RaceConfiguration());
            //        _newRace.CarList[i].Move();
            //    });
            //});
            //Parallel.ForEach(carsInTheRace, task => task.Start());
        }
        public void StopRace()
        {
            throw new NotImplementedException();
        }
        public List<CarStatusWorker> GetStatus()
        {
            List<CarStatusWorker> raceStatus = new List<CarStatusWorker>();
            foreach (var item in _newRace.CarList)
            {
                raceStatus.Add(item.GetStatus());
            }
            _isRaceRunning = raceStatus.Any(x => x.IsInTheRace);
            List<CarStatusWorker> sortedRaceStatus = raceStatus.OrderByDescending(x => x.DistanceCovered).ThenBy(x => x.IsFinished).ToList();
            for (int i = 0; i < sortedRaceStatus.Count(); i++)
            {
                sortedRaceStatus[i].Place = (i + 1);
            }
            return sortedRaceStatus;
        }
        public bool IsRaceRunning()
        {
            return _isRaceRunning;
        }
        private void CarNormalization()
        {
            NormilizeEngines();
            NormilizeBrakes();
            NormilizeSuspention();
        }
        private void NormilizeEngines()
        {
            var maxHP = _newRace.CarList.Max(x => x.Engine.HP);
            foreach (var item in _newRace.CarList)
            {
                item.Engine.NormilizedHP = (double)item.Engine.HP / maxHP;
            }
        }
        private void NormilizeBrakes()
        {
            var maxEffCoef = _newRace.CarList.Max(x => x.Brake.EffecientKoef);
            foreach (var item in _newRace.CarList)
            {
                item.Brake.NormilizedEffecientKoef = (double)item.Brake.EffecientKoef / maxEffCoef;
            }
        }
        private void NormilizeSuspention()
        {
            var maxRigidCoef = _newRace.CarList.Max(x => x.Suspention.RigidityKoef);
            foreach (var item in _newRace.CarList)
            {
                item.Suspention.NormilizedRigidityKoef = (double)item.Suspention.RigidityKoef / maxRigidCoef;
            }
        }
    }
}
