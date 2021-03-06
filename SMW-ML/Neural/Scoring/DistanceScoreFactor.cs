using SMW_ML.Game.SuperMarioWorld;

namespace SMW_ML.Neural.Scoring
{
    internal class DistanceScoreFactor : IScoreFactor
    {
        private const string EAST_DISTANCE = "East Mult";
        private const string WEST_DISTANCE = "West Mult";
        private const string UP_DISTANCE = "Up Mult";
        private const string DOWN_DISTANCE = "Down Mult";

        private double currScore;
        private uint minYPosition = 0;
        private uint maxYPosition = 0;
        private uint minXPosition = 0;
        private uint maxXPosition = 0;
        private bool inited = false;
        private uint levelUID;

        public DistanceScoreFactor()
        {
            ExtraFields = new ExtraField[]
            {
                new ExtraField(EAST_DISTANCE, 1.0),
                new ExtraField(WEST_DISTANCE, 0.0),
                new ExtraField(UP_DISTANCE, 0.5),
                new ExtraField(DOWN_DISTANCE, 0.25)
            };
        }

        public bool ShouldStop => false;
        public double ScoreMultiplier { get; set; }

        public string Name => "Distance travelled";

        public bool CanBeDisabled => true;

        public bool IsDisabled { get; set; }

        public ExtraField[] ExtraFields { get; set; }

        public double GetFinalScore() => currScore;

        public void Update(DataFetcher dataFetcher)
        {
            if (inited && levelUID != dataFetcher.GetLevelUID()) return; // Return if not in same area anymore

            uint newPosX = dataFetcher.GetPositionX();
            uint newPosY = dataFetcher.GetPositionY();

            if (!inited)
            {
                inited = true;

                minXPosition = newPosX;
                maxXPosition = newPosX;
                minYPosition = newPosY;
                maxYPosition = newPosY;

                levelUID = dataFetcher.GetLevelUID();
            }

            if (dataFetcher.IsOnGround() || dataFetcher.IsInWater())
            {
                //TODO : Do something about entering sub-areas

                double totalSubScore = 0;
                if (newPosX > maxXPosition)
                {
                    totalSubScore += (newPosX - maxXPosition) * ExtraField.GetValue(ExtraFields, EAST_DISTANCE);
                    maxXPosition = newPosX;
                }
                if (newPosX < minXPosition)
                {
                    totalSubScore += (minXPosition - newPosX) * ExtraField.GetValue(ExtraFields, WEST_DISTANCE);
                    minXPosition = newPosX;
                }
                if (newPosY > maxYPosition)
                {
                    totalSubScore += (newPosY - maxYPosition) * ExtraField.GetValue(ExtraFields, DOWN_DISTANCE);
                    maxYPosition = newPosY;
                }
                if (newPosY < minYPosition)
                {
                    totalSubScore += (minYPosition - newPosY) * ExtraField.GetValue(ExtraFields, UP_DISTANCE);
                    minYPosition = newPosY;
                }

                currScore += totalSubScore / 16.0 * ScoreMultiplier;
            }
        }

        public void LevelDone()
        {
            minXPosition = 0;
            maxXPosition = 0;
            minYPosition = 0;
            maxYPosition = 0;
            inited = false;
        }

        public IScoreFactor Clone()
        {
            return new DistanceScoreFactor()
            {
                IsDisabled = IsDisabled,
                ScoreMultiplier = ScoreMultiplier,
                ExtraFields = ExtraFields
            };
        }
    }
}
