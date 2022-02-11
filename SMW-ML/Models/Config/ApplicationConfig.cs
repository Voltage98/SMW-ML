﻿using SMW_ML.Neural.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW_ML.Models.Config
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            StopTrainingCondition = "";
            ArduinoCommunicationPort = "COM3";
            ScoreFactors = new List<IScoreFactor>()
            {
                new DiedScoreFactor() { ScoreMultiplier = -10 },
                new DistanceScoreFactor() { ScoreMultiplier = 3 },
                new StopMovingScoreFactor() { ScoreMultiplier = -10 },
                new TimeTakenScoreFactor() { ScoreMultiplier = 1 },
                new WonLevelScoreFactor() { ScoreMultiplier = 100 }
            };
        }

        public int Multithread { get; set; }
        public string ArduinoCommunicationPort { get; set; }
        public string StopTrainingCondition { get; set; }
        public int? StopTrainingConditionValue { get; set; }
        public List<IScoreFactor> ScoreFactors { get; set; }
    }
}
