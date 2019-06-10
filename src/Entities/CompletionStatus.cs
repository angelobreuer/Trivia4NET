namespace Trivia4NET.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    ///     The strongly-typed representation of the completion status for a category.
    /// </summary>
    public sealed class CompletionStatus
    {
        /// <summary>
        ///     Gets the number of total pending questions in the category.
        /// </summary>
        [JsonProperty("total_num_of_pending_questions")]
        public int PendingQuestions { get; internal set; }

        /// <summary>
        ///     Gets the number of total questions in the category.
        /// </summary>
        [JsonProperty("total_num_of_questions")]
        public int Questions { get; internal set; }

        /// <summary>
        ///     Gets the number of total rejected questions in the category.
        /// </summary>
        [JsonProperty("total_num_of_rejected_questions")]
        public int RejectedQuestions { get; internal set; }

        /// <summary>
        ///     Gets the number of total verified / accepted questions in the category.
        /// </summary>
        [JsonProperty("total_num_of_verified_questions")]
        public int VerifiedQuestions { get; internal set; }

        /// <summary>
        ///     Builds a <see cref="string"/> representation of the <see cref="CompletionStatus"/>.
        /// </summary>
        /// <returns>a <see cref="string"/> representation of the <see cref="CompletionStatus"/></returns>
        public override string ToString() => $"total: {Questions}, pending: {PendingQuestions}, rejected: {RejectedQuestions}, verified: {VerifiedQuestions}";
    }
}