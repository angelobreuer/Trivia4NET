namespace Trivia4NET.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     A set of the question types returned by the Trivia API.
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        ///     A multiple-choice questions which normally contains three wrong and one right answer.
        /// </summary>
        [EnumMember(Value = "multiple")]
        Multiple,

        /// <summary>
        ///     A yes / no question where the answers can be only <see langword="true"/> ("True") or
        ///     <see langword="false"/> ("False").
        /// </summary>
        [EnumMember(Value = "boolean")]
        YesNo
    }
}