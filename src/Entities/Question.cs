namespace Trivia4NET.Entities
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Trivia4NET.Util;

    /// <summary>
    ///     The strongly-typed representation of a question.
    /// </summary>
    public sealed class Question
    {
        /// <summary>
        ///     Gets the correct answer to the question.
        /// </summary>
        [JsonConverter(typeof(HtmlEntityConverter))]
        [JsonRequired, JsonProperty("correct_answer")]
        public string Answer { get; internal set; }

        /// <summary>
        ///     Gets the english name of the category the question is.
        /// </summary>
        [JsonRequired, JsonProperty("category")]
        public string Category { get; internal set; }

        /// <summary>
        ///     Gets the question content.
        /// </summary>
        [JsonRequired, JsonProperty("question")]
        [JsonConverter(typeof(HtmlEntityConverter))]
        public string Content { get; internal set; }

        /// <summary>
        ///     Gets the difficulty level of the question.
        /// </summary>
        [JsonRequired, JsonProperty("difficulty")]
        public Difficulty Difficulty { get; internal set; }

        /// <summary>
        ///     Gets a list of incorrect answers for the questions.
        /// </summary>
        /// <remarks>
        ///     If the question type ( <see cref="Type"/>) is <see cref="QuestionType.YesNo"/> then
        ///     there is only one incorrect answer, either <see langword="true"/> ("True") or
        ///     <see langword="false"/> ("False").
        /// </remarks>
        [JsonRequired, JsonProperty("incorrect_answers", ItemConverterType = typeof(HtmlEntityConverter))]
        public IReadOnlyList<string> IncorrectAnswers { get; internal set; }

        /// <summary>
        ///     Gets the type of the question.
        /// </summary>
        [JsonRequired, JsonProperty("type")]
        public QuestionType Type { get; internal set; }

        /// <summary>
        ///     Builds a <see cref="string"/> representation of the <see cref="Question"/>.
        /// </summary>
        /// <returns>a <see cref="string"/> representation of the <see cref="Question"/></returns>
        public override string ToString() => $"Q: {Content}, A: {Answer}";
    }
}