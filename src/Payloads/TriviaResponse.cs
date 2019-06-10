namespace Trivia4NET.Payloads
{
    using Newtonsoft.Json;
    using Trivia4NET.Entities;

    /// <summary>
    ///     The base implementation of a response from the Trivia API.
    /// </summary>
    public class TriviaResponse
    {
        /// <summary>
        ///     Gets the response code that was returned by the Trivia API.
        /// </summary>
        [JsonProperty("response_code")]
        public ResponseCode ResponseCode { get; internal set; }
    }
}