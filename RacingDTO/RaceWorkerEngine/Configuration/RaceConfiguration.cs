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
        // track lenght
        private readonly double _trackLenght;
        // chance to get failture
        private readonly int _failtureChance;
        // chance to obtain speed up bonus
        private readonly int _speedUpChance;

        public RaceConfiguration(double distance = 200.0, double trackPercentageOfStraightLines = 0.8, int failtureChance = 50, int speedUpChance=5)
        {
            _trackPercentageOfStraightLines = trackPercentageOfStraightLines;
            _trackLenght = distance;
            _failtureChance = failtureChance;
            _speedUpChance = speedUpChance;
        }

        public double TrackPercentageOfStraightLines => _trackPercentageOfStraightLines;
        public double TrackLenght => _trackLenght;
        public int FailtureChance => _failtureChance;
        public int SpeedUpChance => _speedUpChance;
    }
}
