namespace Trivia4NET.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    ///     The strongly-typed representation of the statistics for a category.
    /// </summary>
    public sealed class CategoryStatistics
    {
        /// <summary>
        ///     Gets the number of total questions in the category.
        /// </summary>
        [JsonRequired, JsonProperty("total_question_count")]
        public int TotalQuestions { get; internal set; }

        /// <summary>
        ///     Gets the number of easy (difficulty) questions in the category.
        /// </summary>
        [JsonRequired, JsonProperty("total_easy_question_count")]
        public int EasyQuestions { get; internal set; }

        /// <summary>
        ///     Gets the number of medium (difficulty) questions in the category.
        /// </summary>
        [JsonRequired, JsonProperty("total_medium_question_count")]
        public int MediumQuestions { get; internal set; }

        /// <summary>
        ///     Gets the number of hard (difficulty) questions in the category.
        /// </summary>
        [JsonRequired, JsonProperty("total_hard_question_count")]
        public int HardQuestions { get; internal set; }
    }
}