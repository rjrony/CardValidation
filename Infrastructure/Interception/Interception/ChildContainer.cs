// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChildContainer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System;

    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The child container.
    /// </summary>
    public class ChildContainer : IChildContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChildContainer"/> class.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Parameter must not be null
        /// </exception>
        public ChildContainer(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException("unityContainer");
            }

            this.ContainerWeakReference = new WeakReference<IUnityContainer>(unityContainer);
        }

        /// <summary>
        ///     Gets the i unity container.
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                IUnityContainer container;
                if (this.ContainerWeakReference.TryGetTarget(out container))
                {
                    return container;
                }

                return null;
            }
        }

        private WeakReference<IUnityContainer> ContainerWeakReference { get; set; }
    }
}