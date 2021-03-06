using Newtonsoft.Json;
using SMW_ML.Game.SuperMarioWorld;

namespace SMW_ML.Neural.Scoring
{
    /// <summary>
    /// Represents a single score factor
    /// </summary>
    public interface IScoreFactor
    {
        /// <summary>
        /// The readable name of this score factor
        /// </summary>
        [JsonIgnore]
        string Name { get; }
        /// <summary>
        /// Whether or not this Score Factor can be disabled
        /// </summary>
        [JsonIgnore]
        bool CanBeDisabled { get; }
        /// <summary>
        /// Whether or not this Score Factor is disabled.
        /// </summary>
        bool IsDisabled { get; set; }
        /// <summary>
        /// Whether or not the training for the current level should be stopped, according to this score factor
        /// </summary>
        [JsonIgnore]
        bool ShouldStop { get; }
        /// <summary>
        /// Multiplies the final score by this factor. Should be negative when points subtractions are wanted, positive otherwise.
        /// Can be set to 0 to make it so the score factor doesn't affect the total score
        /// </summary>
        double ScoreMultiplier { get; set; }
        /// <summary>
        /// Updates the state of the Score Factor. To be called every frame
        /// </summary>
        /// <param name="dataFetcher"></param>
        void Update(DataFetcher dataFetcher);
        /// <summary>
        /// The final score of the score factor, after all levels have been processed
        /// </summary>
        /// <returns></returns>
        double GetFinalScore();
        /// <summary>
        /// To be called when a level is over.
        /// </summary>
        void LevelDone();

        /// <summary>
        /// Name of any extra field this score factor has.
        /// </summary>
        ExtraField[] ExtraFields { get; set; }

        /// <summary>
        /// Clones this score factor into a new instance.
        /// </summary>
        /// <returns></returns>
        IScoreFactor Clone();
    }
}
