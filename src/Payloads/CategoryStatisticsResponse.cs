namespace Trivia4NET.Payloads
{
    using Newtonsoft.Json;
    using Trivia4NET.Entities;

    /// <summary>
    ///     The strongly-typed representation of the response of the "api_count.php" api endpoint.
    /// </summary>
    public sealed class CategoryStatisticsResponse : TriviaResponse
    {
        /// <summary>
        ///     Gets the category identifier.
        /// </summary>
        [JsonRequired, JsonProperty("category_id")]
        public int CategoryId { get; internal set; }

        /// <summary>
        ///     Gets the statistics for the category.
        /// </summary>
        [JsonRequired, JsonProperty("category_question_count")]
        public CategoryStatistics Statistics { get; internal set; }
    }
}