namespace Trivia4NET.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    ///     The strongly-typed representation of a Trivia API category.
    /// </summary>
    public sealed class TriviaCategory
    {
        /// <summary>
        ///     Gets the unique identifier of the category.
        /// </summary>
        [JsonRequired, JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        ///     Gets the english name of the category.
        /// </summary>
        [JsonRequired, JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        ///     Builds a <see cref="string"/> representation of the <see cref="TriviaCategory"/>.
        /// </summary>
        /// <returns>a <see cref="string"/> representation of the <see cref="TriviaCategory"/></returns>
        public override string ToString() => $"{Name} ({Id})";
    }
}