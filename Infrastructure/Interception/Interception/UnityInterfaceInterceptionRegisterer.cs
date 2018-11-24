// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityInterfaceInterceptionRegisterer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    ///     The unity interface interception registerer.
    /// </summary>
    public class UnityInterfaceInterceptionRegisterer : UnityContainerExtension
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the interception behaviors.
        /// </summary>
        public List<InterceptConfig> InterceptionBehaviors { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnityInterfaceInterceptionRegisterer" /> class.
        /// </summary>
        public UnityInterfaceInterceptionRegisterer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityInterfaceInterceptionRegisterer"/> class.
        /// </summary>
        /// <param name="interceptionBehaviors">
        /// The interception behaviors.
        /// </param>
        public UnityInterfaceInterceptionRegisterer(InterceptConfig[] interceptionBehaviors)
        {
            this.InterceptionBehaviors = new List<InterceptConfig>(interceptionBehaviors);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        protected override void Initialize()
        {
            this.Container.AddNewExtension<Interception>();
            this.Context.Registering += this.OnRegister;
        }

        /// <summary>
        /// The on register.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnRegister(object sender, RegisterEventArgs e)
        {
            if (this.InterceptionBehaviors != null && this.InterceptionBehaviors.Count > 0)
            {
                var container = sender as IUnityContainer;

                if (e != null && e.TypeFrom != null && e.TypeFrom.IsInterface && !e.TypeFrom.IsAssignableFrom(typeof(ICallHandler)))
                {
                    var interception = container.Configure<Interception>().SetInterceptorFor(e.TypeFrom, e.Name, new InterfaceInterceptor());

                    var behaviorsToAdd = this.InterceptionBehaviors.Where(b => b.ShouldIntercept(e));
                    foreach (var interceptConfig in behaviorsToAdd)
                    {
                        foreach (var interceptionBehavior in interceptConfig.InterceptionBehaviors)
                        {
                            var unityBehavior = new InterceptionBehavior(interceptionBehavior);
                            unityBehavior.AddPolicies(e.TypeFrom, e.TypeTo, e.Name, this.Context.Policies);
                        }
                    }
                }
            }
        }

        #endregion
    }
}