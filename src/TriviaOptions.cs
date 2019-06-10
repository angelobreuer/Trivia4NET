namespace Trivia4NET
{
    using System;

    /// <summary>
    ///     The options for the <see cref="TriviaService"/>.
    /// </summary>
    public sealed class TriviaOptions
    {
        /// <summary>
        ///     Gets or sets the Trivia API endpoint.
        /// </summary>
        public Uri BaseAddress { get; set; } = new Uri("https://opentdb.com/");

        /// <summary>
        ///     Gets or sets the user-agent for making requests to the Trivia API.
        /// </summary>
        public string UserAgent { get; set; } = "Trivia4NET";

        /// <summary>
        ///     Gets a value indicating whether the token should be auto-reseted when it's empty (
        ///     <see langword="true"/>), or an exception should be thrown ( <see langword="false"/>).
        ///     This property defaults to <see langword="true"/>.
        /// </summary>
        public bool AutoResetToken { get; set; } = true;
    }
}