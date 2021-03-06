using SMW_ML.Game.SuperMarioWorld;
using System;

namespace SMW_ML.Neural.Scoring
{
    internal class DiedScoreFactor : IScoreFactor
    {
        private bool shouldStop = false;
        private double currScore;

        public DiedScoreFactor()
        {
            ExtraFields = Array.Empty<ExtraField>();
        }

        public bool ShouldStop => shouldStop;
        public double ScoreMultiplier { get; set; }

        public string Name => "Died";

        public bool CanBeDisabled => false;

        public bool IsDisabled { get => false; set { } }

        public ExtraField[] ExtraFields { get; set; }

        public double GetFinalScore() => currScore;

        public void Update(DataFetcher dataFetcher)
        {
            if (dataFetcher.IsDead())
            {
                shouldStop = true;
                currScore += ScoreMultiplier;
                return;
            }
        }

        public void LevelDone()
        {
            shouldStop = false;
        }

        public IScoreFactor Clone()
        {
            return new DiedScoreFactor() { ScoreMultiplier = ScoreMultiplier };
        }
    }
}
