using RacingDTO.RaceWorkerEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Models
{
    public class RacingCarWorker : IRacingCarWorker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrakeId { get; set; }
        public BrakeWorker Brake { get; set; }
        public int EngineId { get; set; }
        public EngineWorker Engine { get; set; }
        public int SuspentionId { get; set; }
        public SuspentionWorker Suspention { get; set; }

        private double _curDistanceCovered;
        private int _curEngineTemp = 30;
        private RaceConfiguration _curRaceConfiguration;
        private bool _hasOverheatingPenalty = false;




        public void GetRaceConfiguration(RaceConfiguration raceConfiguration)
        {
            _curRaceConfiguration = raceConfiguration;
        }
        private void Accelerate()
        {
            //_curDistanceCovered += 10;
            double distanceTraveled = 10* ((0.5 + this.Engine.NormilizedHP) + 0.5 * Convert.ToInt32(this.Engine.Turbine) + (0.5 + this.Suspention.NormilizedRigidityKoef)) *
              (_curRaceConfiguration.TrackPercentageOfStraightLines - 0.5);
            _curDistanceCovered += distanceTraveled;
            Console.WriteLine($"Accelerate {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseAccel;
        }
        private void HoldSpeed()
        {
           // _curDistanceCovered += 5;
            double distanceTraveled = 7*((0.5 + this.Engine.NormilizedHP) * (1.5 - this.Engine.NormilizedHP));
            _curDistanceCovered += distanceTraveled;
            Console.WriteLine($"Hold speed {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseHoldSpeed;
        }
        private void Braking()
        {
            // _curDistanceCovered += 2;
            _curDistanceCovered += 2;
            double distanceTraveled = 6*((1.5 - this.Engine.NormilizedHP) + 0.5 * Convert.ToInt32(this.Engine.Turbine) + (0.5 + this.Suspention.NormilizedRigidityKoef)) *
                (_curRaceConfiguration.TrackPercentageOfStraightLines - 0.5);
            _curDistanceCovered += distanceTraveled;
            Console.WriteLine($"Breaking {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseBrake;
        }
        private bool IsFinished()
        {
            if (_curRaceConfiguration.TrackLenght > _curDistanceCovered)
            {
                return false;
            }
            return true;
        }
        private bool IsEngineNearOverheated()
        {
            if (this._curEngineTemp >= CarConfiguration.MaxEngineTemperature - CarConfiguration.TemperatureTresholdLimit)
            {
                return true;
            }
            return false;
        }
        private bool IsFailure()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int failtureChance = random.Next(1, _curRaceConfiguration.FailtureChance + 1);
            if (failtureChance == _curRaceConfiguration.FailtureChance)
            {
                return true;
            }
            return false;
        }
        private bool IsSpeedUp()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int speedUpChance = random.Next(1, _curRaceConfiguration.SpeedUpChance + 1);
            if (speedUpChance == _curRaceConfiguration.SpeedUpChance)
            {
                return true;
            }
            return false;
        }
        public void Move()
        {
            Thread countThread = Thread.CurrentThread;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Car {this.Name} on the thread { countThread.ManagedThreadId}");
                Console.ForegroundColor = ConsoleColor.White;
                if (IsFailure())
                {
                    Console.WriteLine($"ПОЛОМАЛАСЯ!! АЙ-АЙ");
                    break;
                }
                if (IsEngineNearOverheated())
                {
                    for (int i = 0; i < CarConfiguration.MaxBrakingAfterOverheating; i++)
                    {
                        Console.WriteLine($"Breaking due to overheating {i}\t ");
                        Braking();
                        if (IsFinished())
                        {
                            break;
                        }
                    }
                    Console.WriteLine($"Engine temperature after braking {_curEngineTemp}\t ");
                }
                else
                {
                    Random random = new Random(DateTime.Now.Millisecond);
                    int actionNumber = random.Next(1, CarBehaviourWorker.TotalActionWeight + 1);
                    if (actionNumber <= CarBehaviourWorker.Acceleration)
                    {
                        Console.WriteLine($"action number is {actionNumber}\t Accel");
                        Accelerate();
                    }
                    else if ((actionNumber > CarBehaviourWorker.Acceleration) && (actionNumber <= (CarBehaviourWorker.TotalActionWeight - CarBehaviourWorker.Braking)))
                    {
                        Console.WriteLine($"action number is {actionNumber}\t Hold");
                        HoldSpeed();
                    }
                    else
                    {
                        Console.WriteLine($"action number is {actionNumber}\t Brake");
                        Braking();
                    }
                    Console.WriteLine($"Engine temperature {this._curEngineTemp}");
                }

                Thread.Sleep(100);
            } while (!IsFinished());
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
        }

    }
}
