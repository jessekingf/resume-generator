namespace Common.Threading;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Helper methods for asynchronous methods.
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    /// The task factory to use for calling asynchronous methods.
    /// </summary>
    private static readonly TaskFactory TaskFactory = new TaskFactory(
        CancellationToken.None,
        TaskCreationOptions.None,
        TaskContinuationOptions.None,
        TaskScheduler.Default);

    /// <summary>
    /// Runs a asynchronous method synchronously.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="function">The asynchronous function to call.</param>
    /// <returns>The return from the method.</returns>
    public static TResult RunSync<TResult>(Func<Task<TResult>> function)
    {
        if (function == null)
        {
            throw new ArgumentNullException(nameof(function));
        }

        return TaskFactory
            .StartNew(function)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Runs a asynchronous method synchronously.
    /// </summary>
    /// <param name="function">The asynchronous function to call.</param>
    public static void RunSync(Func<Task> function)
    {
        if (function == null)
        {
            throw new ArgumentNullException(nameof(function));
        }

        TaskFactory
            .StartNew(function)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }
}
