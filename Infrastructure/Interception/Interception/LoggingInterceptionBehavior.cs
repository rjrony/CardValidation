// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingInterceptionBehavior.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System;
    using System.Collections.Generic;

    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    ///     The logging interception behavior.
    /// </summary>
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        #region Fields

        /// <summary>
        ///     The logger.
        /// </summary>
        private ILogger logger;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether will execute.
        /// </summary>
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        private ILogger Logger
        {
            get
            {
                if (this.logger == null || this.logger is NullLogger)
                {
                    this.logger = ServiceLocator.Instance.GetRootContainer().Resolve<ILogger>() ?? new NullLogger();
                }

                return this.logger;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get required interfaces.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="getNext">
        /// The get next.
        /// </param>
        /// <returns>
        /// The <see cref="IMethodReturn"/>.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var loggerName = input.Target.GetType().FullName;

            // Before invoking the method on the original target.
            this.Logger.Debug(
                () => string.Format("Invoking method {0} at {1}", input.MethodBase, DateTime.Now.ToLongTimeString()),
                loggerName);

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                this.logger.Error(
                    () =>
                    string.Format(
                        "Method {0} threw exception {1} at {2}",
                        input.MethodBase,
                        result.Exception.Message,
                        DateTime.Now.ToLongTimeString()));
                this.logger.Exception(result.Exception, loggerName);
            }
            else
            {
                this.logger.Debug(
                    () =>
                    string.Format("Method {0} returned {1} at {2}", input.MethodBase, result.ReturnValue, DateTime.Now.ToLongTimeString()),
                    loggerName);
            }

            return result;
        }

        #endregion
    }
}