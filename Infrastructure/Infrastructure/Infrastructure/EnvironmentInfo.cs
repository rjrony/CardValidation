// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnvironmentInfo.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    /// <summary>
    ///     The environment info.
    /// </summary>
    public static class EnvironmentInfo
    {
        /// <summary>
        ///     Gets a value indicating whether is development machine.
        /// </summary>
        /// <value>
        ///     The is development machine.
        /// </value>
        public static bool IsDevelopmentMachine
        {
            get
            {
                var devEnv = (ConfigurationManager.AppSettings["IsDevelopmentMachine"] ?? string.Empty).ToLowerInvariant();
                bool isDevMachine = devEnv == "true" || devEnv == "1";


                return isDevMachine;
            }
        }

        /// <summary>
        ///     Gets the environment name.
        /// </summary>
        /// <value>
        ///     Returns environment name as string.
        /// </value>
        public static string EnvironmentName
        {
            get
            {
                var envName = ConfigurationManager.AppSettings["Environment"];
                if (string.IsNullOrEmpty(envName))
                {
                    throw new Exception("Environment name is invalid in AppSettings!");
                }

                return envName;
            }
        }

        /// <summary>
        ///     The get entry assembly.
        /// </summary>
        /// <returns>
        ///     The <see cref="Assembly" />.
        /// </returns>
        public static Assembly GetEntryAssembly()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                return entryAssembly;
            }

            if (HttpContext.Current != null && HttpContext.Current.ApplicationInstance != null)
            {
                var type = System.Web.HttpContext.Current.ApplicationInstance.GetType();
                while (type != null && type.Namespace == "ASP")
                {
                    type = type.BaseType;
                }

                return type == null ? null : type.Assembly;
            }

            return null;
        }
        public static Assembly GetEntryAssemblyV2()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                return entryAssembly;
            }
            var types = CurrentDomainTypes.GetTypesDerivingFrom<IAppStartup>(isIncludingAbstract: false);
            //assembly.GetName().Name.Split('.').First();



            return null;
        }
    }
}