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

        //чем ближе к 1, тем больше на трассе прямых
        private readonly double _trackPercentageOfStraightLines;
        private readonly double _trackDistance;
        private readonly int _failtureChance;

        public RaceConfiguration(double distance = 200.0, double trackPercentageOfStraightLines = 0.8, int failtureChance = 50)
        {
            _trackPercentageOfStraightLines = trackPercentageOfStraightLines;
            _trackDistance = distance;
            _failtureChance = failtureChance;
        }
        //public int MaxEngineTemperature { get { return _maxEngineTemperature; } }

        public double TrackPercentageOfStraightLines => _trackPercentageOfStraightLines;
        public double TrackDistance => _trackDistance;
        public int FailtureChance => _failtureChance;
    }
}
