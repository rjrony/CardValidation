// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearializeAndDeserializeIssues.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Testing.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    /// <summary>
    ///     The test task data.
    /// </summary>
    [TestClass]
    //[Ignore]
    public class SearializeAndDeserializeIssues
    {
        #region Static Fields

        private static readonly List<Type> DataTypeDone = new List<Type>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The assembly initialize.
        /// </summary>
        /// <param name="testContext">
        /// The test context.
        /// </param>
        [AssemblyInitialize]
        public static void DoInitializeAssemblies(TestContext testContext)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var type = typeof(SearializeAndDeserializeIssues);
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, type.Namespace);
            var configFile = string.Format("{0}.dll.config", fileName);
            if (File.Exists(configFile))
            {
                return;
            }

            var resourceName = type.Namespace + ".appcopy.config";

            using (Stream stream = type.Assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                File.WriteAllText(configFile, result);
            }
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DataTypeDone.Clear();
        }

        #endregion

        #region Methods

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            switch (name.Name)
            {
                case "Newtonsoft.Json":
                    return typeof(JsonException).Assembly;
                default:
                    return FindAssembly(args.Name);
            }
        }

        private static Assembly FindAssembly(string name)
        {
            var foundAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == name);
            return foundAssembly;
        }

        #endregion
    }
}