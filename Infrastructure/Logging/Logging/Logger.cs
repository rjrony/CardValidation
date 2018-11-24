// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using Infrastructure.Interception.Contract;

namespace Infrastructure.Logging
{
    /// <summary>
    ///     The logger.
    /// </summary>
    public class Logger : LoggerBase<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        public Logger(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        /*
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="defaultLoggerName">
        /// The logger name.
        /// </param>
        public Logger(string defaultLoggerName) : base(defaultLoggerName)
        {
        }*/

        /// <summary>
        /// The get item log text.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        protected override StringBuilder GetItemLogText(object item)
        {
            return null;
        }
    }
}