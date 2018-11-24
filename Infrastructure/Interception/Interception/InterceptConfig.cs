// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterceptConfig.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    ///     The intercept config.
    /// </summary>
    public class InterceptConfig
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptConfig"/> class.
        /// </summary>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <param name="shouldIntercept">
        /// The should intercept.
        /// </param>
        public InterceptConfig(IInterceptionBehavior behavior, Func<RegisterEventArgs, bool> shouldIntercept)
            : this(new[] { behavior }, shouldIntercept)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptConfig"/> class.
        /// </summary>
        /// <param name="behaviors">
        /// The behaviors.
        /// </param>
        /// <param name="shouldIntercept">
        /// The should intercept.
        /// </param>
        public InterceptConfig(IInterceptionBehavior[] behaviors, Func<RegisterEventArgs, bool> shouldIntercept)
        {
            if (behaviors == null)
            {
                throw new ArgumentNullException("behaviors");
            }

            if (shouldIntercept == null)
            {
                throw new ArgumentNullException("shouldIntercept");
            }

            this.InterceptionBehaviors = behaviors;
            this.ShouldIntercept = shouldIntercept;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the interception behaviors.
        /// </summary>
        public IInterceptionBehavior[] InterceptionBehaviors { get; private set; }

        /// <summary>
        ///     Gets the should intercept.
        /// </summary>
        public Func<RegisterEventArgs, bool> ShouldIntercept { get; private set; }

        #endregion
    }
}