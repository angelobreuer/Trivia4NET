namespace Trivia4NET.Entities
{
    /// <summary>
    ///     A set of different response codes returned by the Trivia API.
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        ///     Indicates that the results were returned successfully.
        /// </summary>
        Success = 0,

        /// <summary>
        ///     Indicates that the api does not have enough questions for the query.
        /// </summary>
        NoResults = 1,

        /// <summary>
        ///     Indicates that the passed query parameters are invalid.
        /// </summary>
        InvalidParameter = 2,

        /// <summary>
        ///     Indicates that the specified session token was not found.
        /// </summary>
        TokenNotFound = 3,

        /// <summary>
        ///     Indicates that there are no questions for the specified query and token.
        /// </summary>
        TokenEmpty = 4
    }
}