namespace Trivia4NET.Payloads
{
    using Newtonsoft.Json;

    /// <summary>
    ///     The response for the token endpoint.
    /// </summary>
    public sealed class TokenResponse : TriviaResponse
    {
        /// <summary>
        ///     Gets the response message, on failure this can contain a helpful message.
        /// </summary>
        [JsonRequired, JsonProperty("response_message")]
        public string ResponseMessage { get; internal set; }

        /// <summary>
        ///     Gets the session token.
        /// </summary>
        [JsonRequired, JsonProperty("token")]
        public string SessionToken { get; internal set; }
    }
}