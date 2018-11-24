// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncHelper.cs" company="SS">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   The async helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The async helper.
    /// </summary>
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new
            TaskFactory(
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        /// <summary>
        /// The run sync.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return MyTaskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// The run sync.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        public static void RunSync(Func<Task> func)
        {
            MyTaskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
