namespace Trivia4NET.Entities
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    ///     A set of the different difficulty levels supported by the Trivia API.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Difficulty
    {
        /// <summary>
        ///     The difficulty Easy ("easy").
        /// </summary>
        [EnumMember(Value = "easy")]
        Easy,

        /// <summary>
        ///     The difficulty Medium ("medium").
        /// </summary>
        [EnumMember(Value = "medium")]
        Medium,

        /// <summary>
        ///     The difficulty Hard ("hard").
        /// </summary>
        [EnumMember(Value = "hard")]
        Hard
    }
}