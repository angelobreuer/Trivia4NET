namespace Trivia4NET.Util
{
    using System;
    using System.Web;
    using Newtonsoft.Json;

    /// <summary>
    ///     A <see cref="JsonConverter"/> for the serialization and deserialization for HTML encoded strings.
    /// </summary>
    internal sealed class HtmlEntityConverter : JsonConverter<string>
    {
        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        ///     The existing value of object being read. If there is no existing value then null will
        ///     be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
            => reader.Value == null ? null : HttpUtility.HtmlDecode(reader.Value.ToString()).Replace("&amp;", "&");

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
            => writer.WriteValue(HttpUtility.HtmlEncode(value));
    }
}