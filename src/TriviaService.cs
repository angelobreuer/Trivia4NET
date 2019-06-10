namespace Trivia4NET
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Newtonsoft.Json;
    using Trivia4NET.Entities;
    using Trivia4NET.Payloads;
    using Trivia4NET.Util;

    /// <summary>
    ///     A service for making requests to the Trivia API.
    /// </summary>
    public sealed class TriviaService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly bool _autoResetToken;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TriviaService"/> class using the default <see cref="TriviaOptions"/>.
        /// </summary>
        public TriviaService() : this(new TriviaOptions())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TriviaService"/> class.
        /// </summary>
        /// <param name="options">the options for the service</param>
        /// <exception cref="ArgumentNullException">
        ///     thrown if the specified <paramref name="options"/> parameter is <see langword="null"/>.
        /// </exception>
        public TriviaService(TriviaOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            // Initialize private fields
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            _httpClient.BaseAddress = options.BaseAddress;
            _autoResetToken = options.AutoResetToken;
        }

        /// <summary>
        ///     Disposes the underlying HTTP client.
        /// </summary>
        public void Dispose() => _httpClient.Dispose();

        /// <summary>
        ///     Gets all available categories asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<CategoryResponse> GetCategoriesAsync(CancellationToken cancellationToken = default)
            => RequestAsync<CategoryResponse>("api_category.php", cancellationToken);

        /// <summary>
        ///     Gets questions from the Trivia API asynchronously.
        /// </summary>
        /// <param name="token">
        ///     a token (that was returned from <see cref="RequestTokenAsync(CancellationToken)"/>),
        ///     which can be used to distinct the returned questions, so that no duplicate questions
        ///     are returned.
        /// </param>
        /// <param name="amount">the amount of questions to return (maximum: 50 questions)</param>
        /// <param name="difficulty">the difficulty the questions should have</param>
        /// <param name="type">
        ///     the type of the question (either <see cref="QuestionType.YesNo"/> or <see cref="QuestionType.Multiple"/>).
        /// </param>
        /// <param name="categoryId">
        ///     the identifier of the category ( <see cref="TriviaCategory.Id"/>) to return questions
        ///     from, if <see langword="null"/> a random category will be chosen for the questions.
        /// </param>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     thrown if the specified <paramref name="amount"/> is greater than 50.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     thrown if the specified <paramref name="amount"/> is less than 1.
        /// </exception>
        public async Task<QuestionsResponse> GetQuestionsAsync(string token = null, int amount = 20, Difficulty? difficulty = null,
            QuestionType? type = null, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            if (amount > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "It can be only 50 questions requested at a time.");
            }

            if (amount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "It must be requested at least 1 question.");
            }

            // parse an empty query string and add the amount parameter to it
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("amount", amount.ToString());

            // add the token if specified
            if (!string.IsNullOrWhiteSpace(token))
            {
                parameters.Add("token", token);
            }

            // add the difficulty query to the parameters if specified
            if (difficulty.HasValue)
            {
                parameters.Add("difficulty", difficulty.Value.GetSerializedValue());
            }

            // add the type query to the parameters if specified
            if (type.HasValue)
            {
                parameters.Add("type", type.Value.GetSerializedValue());
            }

            // add the category id query to the parameters if specified
            if (categoryId.HasValue)
            {
                parameters.Add("category", categoryId.Value.ToString());
            }

            // make request
            var response = await RequestAsync<QuestionsResponse>("api.php?" + parameters, cancellationToken);

            // check if the token should be reseted if needed
            if (_autoResetToken && !string.IsNullOrWhiteSpace(token) && response.ResponseCode == ResponseCode.TokenEmpty)
            {
                // reset the token, because this request has consumed all possible left questions.
                await ResetTokenAsync(token, cancellationToken);
            }

            return response;
        }

        /// <summary>
        ///     Gets the statistics for the category specified by <paramref name="categoryId"/> asynchronously.
        /// </summary>
        /// <param name="categoryId">the identifier of the category to get the questions for</param>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<CategoryStatisticsResponse> GetStatisticsAsync(int categoryId, CancellationToken cancellationToken = default)
            => RequestAsync<CategoryStatisticsResponse>("api_count.php?category=" + categoryId, cancellationToken);

        /// <summary>
        ///     Gets the statistics for the specified <paramref name="category"/> asynchronously.
        /// </summary>
        /// <param name="category">the category to get the questions for</param>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<CategoryStatisticsResponse> GetStatisticsAsync(TriviaCategory category, CancellationToken cancellationToken = default)
            => GetStatisticsAsync(category.Id, cancellationToken);

        /// <summary>
        ///     Gets the global statistics (overall and foreach category) asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<StatisticsResponse> GetStatisticsAsync(CancellationToken cancellationToken = default)
            => RequestAsync<StatisticsResponse>("api_count_global.php", cancellationToken);

        /// <summary>
        ///     Creates an asynchronous enumerator for question pagination.
        /// </summary>
        /// <param name="token">
        ///     a token (that was returned from <see cref="RequestTokenAsync(CancellationToken)"/>),
        ///     which can be used to distinct the returned questions, so that no duplicate questions
        ///     are returned.
        /// </param>
        /// <param name="pageSize">
        ///     the amount of questions to return per page (maximum: 50 questions)
        /// </param>
        /// <param name="difficulty">the difficulty the questions should have</param>
        /// <param name="type">
        ///     the type of the question (either <see cref="QuestionType.YesNo"/> or <see cref="QuestionType.Multiple"/>).
        /// </param>
        /// <param name="categoryId">
        ///     the identifier of the category ( <see cref="TriviaCategory.Id"/>) to return questions
        ///     from, if <see langword="null"/> a random category will be chosen for the questions.
        /// </param>
        /// <returns>an asynchronous enumerator for question pagination</returns>
        public PaginatedEnumerator<Question> PaginateQuestions(string token = null, int pageSize = 20, Difficulty? difficulty = null, QuestionType? type = null, int? categoryId = null)
            => new PaginatedEnumerator<Question>(async () => (await GetQuestionsAsync(token, pageSize, difficulty, type, categoryId)).Questions.ToArray());

        /// <summary>
        ///     Requests a new session token asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<TokenResponse> RequestTokenAsync(CancellationToken cancellationToken = default)
            => RequestAsync<TokenResponse>("api_token.php?command=request", cancellationToken);

        /// <summary>
        ///     Requests to reset the specified <paramref name="token"/> asynchronously.
        /// </summary>
        /// <param name="token">the token to reset</param>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        public Task<TriviaResponse> ResetTokenAsync(string token, CancellationToken cancellationToken = default)
            => RequestAsync<TriviaResponse>("api_token.php?command=reset&token=" + HttpUtility.UrlEncode(token), cancellationToken);

        /// <summary>
        ///     Sends a request and validates and deserializes its response asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     the type of the strongly-typed representation of the response payload
        /// </typeparam>
        /// <param name="endpoint">the relative URI endpoint (relative to <see cref="TriviaOptions.BaseAddress"/>)</param>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is the response
        ///     received from the Trivia API endpoint.
        /// </returns>
        private async Task<TResponse> RequestAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
            where TResponse : TriviaResponse, new()
        {
            // send the HTTP GET request
            using (var response = await _httpClient.GetAsync(endpoint, cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonConvert.DeserializeObject<TResponse>(content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Error while requesting `{endpoint}` ({payload.ResponseCode}): {content}");
                }

                return payload;
            }
        }
    }
}