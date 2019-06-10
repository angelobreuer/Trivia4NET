namespace Trivia4NET.Util
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     An utility class for getting the serialized version of an enumeration.
    /// </summary>
    internal static class EnumUtils
    {
        /// <summary>
        ///     Gets the serialized enumeration member value ( <see cref="EnumMemberAttribute"/>) for
        ///     the specified enumeration <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        ///     If no <see cref="EnumMemberAttribute"/> is applied to the enumeration value, the
        ///     <see cref="object.ToString"/> method is used.
        /// </remarks>
        /// <param name="value">the enumeration value</param>
        /// <returns>
        ///     the serialized representation ( <see cref="EnumMemberAttribute"/>) of the specified
        ///     enumeration <paramref name="value"/>
        /// </returns>
        public static string GetSerializedValue(this Enum value)
        {
            var type = value.GetType();
            var info = type.GetField(value.ToString());
            var attributes = (EnumMemberAttribute[])info.GetCustomAttributes(typeof(EnumMemberAttribute), false);

            return attributes.Length > 0 ? attributes[0].Value : value.ToString();
        }
    }
}