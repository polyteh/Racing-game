using RacingDTO.RaceWorkerEngine.Configuration;
using RacingDTO.RaceWorkerEngine.Interfaces;
using RacingDTO.RaceWorkerEngine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine
{
    public class RacingCarEngine : IRacingCarEngine
    {
        private RaceConfiguration _curRaceConfiguration;
        private List<string> _curCarStatusMessageList;
        private TimeSpan _timeInTheRace;
        private double _curDistanceCovered;
        private int _curEngineTemp = 30;
        //private bool _hasOverheatingPenalty;
        //private bool _hasBust;
        //private bool _isStopDueOverheating;
        private bool _hasDamage;
        private bool _isStatusSent;
        private bool _isInTheRace;
        private RacingCarWorker _managedCar;
        private object _locker = new object();
        private ManualResetEventSlim mres = new ManualResetEventSlim(true);
        public RacingCarEngine(RacingCarWorker curCar, RaceConfiguration curRaceConf)
        {
            _managedCar = curCar;
            _curRaceConfiguration = curRaceConf;
            _curCarStatusMessageList = new List<string>();
        }

        public void Move()
        {
            Thread countThread = Thread.CurrentThread;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            lock (_locker)
            {
                _isInTheRace = true; ;
            }
            do
            {
                mres.Wait();
                //Console.ForegroundColor = ConsoleColor.Red;
                Debug.WriteLine($"Car {_managedCar.Name} on the thread { countThread.ManagedThreadId}");
                //Console.ForegroundColor = ConsoleColor.White;
                if (IsFailure() || _hasDamage)
                {
                    //Console.WriteLine($"ПОЛОМАЛАСЯ!! АЙ-АЙ");
                    break;
                }
                else if (IsEngineOverheated())
                {
                    //Console.WriteLine($"ПЕРЕГРЕЛСЯ!! АЙ-АЙ");
                    break;
                }
                else if (IsEngineNearOverheated())
                {
                    //_hasOverheatingPenalty = true;
                    for (int i = 0; i < CarConfiguration.MaxBrakingAfterOverheating; i++)
                    {
                        //Console.WriteLine($"Breaking due to overheating {i}\t ");
                        Braking();
                        if (IsFinished())
                        {
                            break;
                        }
                    }
                    //Console.WriteLine($"Engine temperature after braking {_curEngineTemp}\t ");
                }
                else if (IsSpeedUp())
                {
                    for (int i = 0; i < CarConfiguration.MaxAccelerateAfterSpeedUp; i++)
                    {
                        if (IsEngineOverheated())
                        {
                            break;
                        }
                        if (IsFailure())
                        {
                            break;
                        }
                        //Console.WriteLine($"Speed up {i}\t ");
                        Accelerate();
                        if (IsFinished())
                        {
                            break;
                        }
                    }
                    //Console.WriteLine($"Engine temperature after burst {_curEngineTemp}\t ");
                }
                else
                {
                    Random random = new Random(DateTime.Now.Millisecond + (int)_managedCar.Id);
                    int actionNumber = random.Next(1, CarBehaviourWorker.TotalActionWeight + 1);
                    if (actionNumber <= CarBehaviourWorker.Acceleration)
                    {
                        //Console.WriteLine($"action number is {actionNumber}\t Accel");
                        Accelerate();
                    }
                    else if ((actionNumber > CarBehaviourWorker.Acceleration) && (actionNumber <= (CarBehaviourWorker.TotalActionWeight - CarBehaviourWorker.Braking)))
                    {
                        HoldSpeed();
                    }
                    else
                    {
                        Braking();
                    }
                }
                Thread.Sleep(500);
            } while (!IsFinished());
            lock (_locker)
            {
                _isInTheRace = false;
            }
            stopWatch.Stop();
            _timeInTheRace = stopWatch.Elapsed;
        }
        private void Accelerate()
        {
            double distanceTraveled = 10 * ((0.5 + _managedCar.Engine.NormilizedHP) + 0.5 * Convert.ToInt32(_managedCar.Engine.Turbine) +
                (0.5 + _managedCar.Suspention.NormilizedRigidityKoef)) *
              (_curRaceConfiguration.TrackPercentageOfStraightLines - 0.5);
            CalculateActualPosition(distanceTraveled);
            //Console.WriteLine($"Accelerate {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseAccel + 2 * Convert.ToInt32(_managedCar.Engine.Turbine);
        }
        private void HoldSpeed()
        {
            double distanceTraveled = 7 * ((0.5 + _managedCar.Engine.NormilizedHP) * (1.5 - _managedCar.Engine.NormilizedHP));
            CalculateActualPosition(distanceTraveled);
            //Console.WriteLine($"Hold speed {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseHoldSpeed + Convert.ToInt32(_managedCar.Engine.Turbine);
        }
        private void Braking()
        {
            double distanceTraveled = 6 * ((1.5 - _managedCar.Engine.NormilizedHP) + 0.5 * Convert.ToInt32(_managedCar.Engine.Turbine) +
                (0.5 + _managedCar.Suspention.NormilizedRigidityKoef)) *
                (_curRaceConfiguration.TrackPercentageOfStraightLines - 0.5);
            CalculateActualPosition(distanceTraveled);
            //Console.WriteLine($"Breaking {distanceTraveled}");
            this._curEngineTemp += CarConfiguration.TemperatureIncreaseBrake;
        }
        private bool IsFinished()
        {
            if (_curRaceConfiguration.TrackLenght > _curDistanceCovered)
            {
                return false;
            }
            _managedCar.IsFinished = true;
            return true;
        }
        private bool IsEngineOverheated()
        {
            if (this._curEngineTemp >= CarConfiguration.MaxEngineTemperature)
            {
                SetStatus(CarStatusMessageConfiguration.MessageCodes.FailtureDueToEngineOverheating);
                _hasDamage = true;
                return true;
            }
            return false;
        }
        private bool IsEngineNearOverheated()
        {
            if (this._curEngineTemp >= CarConfiguration.MaxEngineTemperature - CarConfiguration.TemperatureTresholdLimit)
            {
                SetStatus(CarStatusMessageConfiguration.MessageCodes.BrakingDueToEngineOverheating);
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
                SetStatus(CarStatusMessageConfiguration.MessageCodes.Failture);
                _hasDamage = true;
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
                SetStatus(CarStatusMessageConfiguration.MessageCodes.SpeedBurst);
                return true;
            }
            return false;
        }
        private void CalculateActualPosition(double distanceTraveled)
        {
            lock (_locker)
            {
                _curDistanceCovered += distanceTraveled;
            }
        }
        private void SetStatus(CarStatusMessageConfiguration.MessageCodes messCode)
        {
            lock (_locker)
            {
                if (!_hasDamage)
                {
                    if (_isStatusSent)
                    {
                        _curCarStatusMessageList.Clear();
                        _isStatusSent = false;
                    }
                    _curCarStatusMessageList.Add(_managedCar.Name + ": " + CarStatusMessageConfiguration.GetMessage(messCode));
                    //Debug.WriteLine(_managedCar.Name + ": " + CarStatusMessageConfiguration.GetMessage(messCode));
                }
            }
        }
        private double GetActualPosition()
        {
            double positionOnTrack;
            lock (_locker)
            {
                positionOnTrack = _curDistanceCovered;
            };
            return positionOnTrack;
        }
        public CarStatusWorker GetStatus()
        {
            lock (_locker)
            {
                CarStatusWorker curCarStatus = new CarStatusWorker()
                {
                    Id = _managedCar.Id,
                    Name = _managedCar.Name,
                    DistanceCovered = (int)this.GetActualPosition(),
                    StatusMessage = new List<string>(),
                    IsFinished = _managedCar.IsFinished,
                    TimeInTheRace = _timeInTheRace,
                    IsInTheRace = _isInTheRace,
                };
                curCarStatus.StatusMessage.AddRange(_curCarStatusMessageList);
                //Debug.WriteLine($"send message from {_managedCar.Name} + {DateTime.Now.ToString()}");
                //foreach (var item in _curCarStatusMessageList)
                //{
                //    Debug.WriteLine(item);
                //}             
                _isStatusSent = true;
                _curCarStatusMessageList.Clear();
                return curCarStatus;
            }
        }
        public bool IsInTheRace()
        {
            bool isInTheRace;
            lock (_locker)
            {
                isInTheRace = _isInTheRace;
            }
            return isInTheRace;
        }
        public void Pause()
        {
            lock (_locker)
            {
                _isInTheRace = false;
            }
            mres.Reset();
        }
        public void Resume()
        {
            lock (_locker)
            {
                if (_hasDamage == false)
                {
                    _isInTheRace = true;
                }
            }
            mres.Set();
        }
    }
}
