using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine
{
    //настроечный класс гонки
    public class RaceConfiguration
    {
        private readonly int _maxEngineTemperature;
        //чем ближе к 1, тем больше на трассе прямых
        private readonly  double _trackPercentageOfStraightLines;
        private readonly int _trackDistance;
        public RaceConfiguration()
        {

        }
        public RaceConfiguration(int distance=100, int maxEngineTemperature=80, double trackPercentageOfStraightLines=0.8)
        {
            _maxEngineTemperature = maxEngineTemperature;
            _trackPercentageOfStraightLines = trackPercentageOfStraightLines;
            _trackDistance = distance;
        }
        //public int MaxEngineTemperature { get { return _maxEngineTemperature; } }
        public int MaxEngineTemperature  =>  _maxEngineTemperature;
        public double TrackPercentageOfStraightLines => _trackPercentageOfStraightLines;
        public int TracDistance => _trackDistance;
    }
}
