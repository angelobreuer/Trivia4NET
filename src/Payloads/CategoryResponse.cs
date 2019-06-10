namespace Trivia4NET.Payloads
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Trivia4NET.Entities;

    /// <summary>
    ///     The strongly-typed representation of the response of the "api_category.php" api endpoint.
    /// </summary>
    public sealed class CategoryResponse : TriviaResponse
    {
        /// <summary>
        ///     Gets the questions returned in the response.
        /// </summary>
        [JsonRequired, JsonProperty("trivia_categories")]
        public IReadOnlyList<TriviaCategory> Categories { get; internal set; }
    }
}