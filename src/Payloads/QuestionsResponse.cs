namespace Trivia4NET.Payloads
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Trivia4NET.Entities;

    /// <summary>
    ///     The strongly-typed representation of the response of the "api.php" api endpoint.
    /// </summary>
    public sealed class QuestionsResponse : TriviaResponse
    {
        /// <summary>
        ///     Gets the questions returned in the response.
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyList<Question> Questions { get; internal set; }
    }
}