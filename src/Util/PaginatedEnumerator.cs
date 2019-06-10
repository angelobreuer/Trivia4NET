namespace Trivia4NET.Util
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     A wrapper for an asynchronous enumerator.
    /// </summary>
    /// <typeparam name="T">the enumeration type</typeparam>
    /// <remarks>
    ///     Adapted from https://gist.github.com/angelobreuer/46f776f31a289e097a74aecdc966755f (PaginatedEnumerator.cs)
    /// </remarks>
    public sealed class PaginatedEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly Func<Task<T[]>> _requestFunc;
        private T[] _items;
        private int _position;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaginatedEnumerator{T}"/> class.
        /// </summary>
        /// <param name="requestFunc">the callback for asynchronously retrieving the new entries</param>
        public PaginatedEnumerator(Func<Task<T[]>> requestFunc)
            => _requestFunc = requestFunc ?? throw new ArgumentNullException(nameof(requestFunc));

        /// <summary>
        ///     Gets the current item.
        /// </summary>
        public T Current { get; private set; }

        /// <summary>
        ///     Gets the number of the current page.
        /// </summary>
        public int CurrentPage { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether the end has been reached.
        /// </summary>
        public bool HasReachedEnd { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether any items are available or whether the next page
        ///     should be requested.
        /// </summary>
        public bool ItemsAvailable => _items != null && _position < _items.Length && !HasReachedEnd;

        /// <summary>
        ///     Gets the number of items remaining in the current page.
        /// </summary>
        public int ItemsRemaining => ItemsAvailable ? _items.Length - _position : 0;

        /// <summary>
        ///     Gets the number of items the current page contains.
        /// </summary>
        public int PageItemCount => _items == null ? 0 : _items.Length;

        /// <summary>
        ///     Does absolutely nothing.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Moves to the next item asynchronously.
        /// </summary>
        /// <param name="cancellationToken">
        ///     a cancellation token used to propagate notification that the asynchronous operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     a task that represents the asynchronous operation. The task result is a value
        ///     indicating whether an item could be get.
        /// </returns>
        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // check if the enumerator end has been reached and no more pages are available to enumerate.
            if (HasReachedEnd)
            {
                return false;
            }

            // check if the end of the page was reached.
            if (_items == null || _position >= _items.Length)
            {
                // request the next page
                _items = await _requestFunc();

                // check if no more items are available
                if (_items == null || _items.Length == 0)
                {
                    // set the flag to indicate that the end has been reached.
                    HasReachedEnd = true;

                    // no items are available
                    return false;
                }

                // reset position
                _position = 0;
            }

            // increase item position
            Current = _items[_position++];
            return true;
        }
    }
}