namespace Trivia4NET.Payloads
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Trivia4NET.Entities;

    /// <summary>
    ///     The strongly-typed representation of the response of the "api_count_global.php" api endpoint.
    /// </summary>
    public sealed class StatisticsResponse : TriviaResponse
    {
        /// <summary>
        ///     Gets the overall completion of all categories.
        /// </summary>
        [JsonRequired, JsonProperty("overall")]
        public CompletionStatus Overall { get; internal set; }

        /// <summary>
        ///     Gets the completions states for all available categories. The key is the identifier
        ///     number of the category ( <see cref="TriviaCategory.Id"/>). The value the completion
        ///     status of the category.
        /// </summary>
        [JsonRequired, JsonProperty("categories")]
        public IReadOnlyDictionary<int, CompletionStatus> CategoryStates { get; internal set; }
    }
}