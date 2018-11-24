// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentDomainTypes.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    /// <summary>
    ///     The current domain types.
    /// </summary>
    public static class CurrentDomainTypes
    {
        private static readonly List<string> AssembliesWithoutStrongName = new List<string>();

        private static readonly List<Assembly> DomainAssemblies = new List<Assembly>();

        private static readonly List<Type> DomainTypes = new List<Type>();

        private static readonly List<string> NamespaceInitals = new List<string> { "Infrastructure", "Foundation", "ExternalApi", "ProductCatalog" };

        private static readonly object SyncObject = new object();

        private static string debugInfoLoadingName = string.Empty;

        /// <summary>
        ///     Initializes static members of the <see cref="CurrentDomainTypes" /> class.
        /// </summary>
        static CurrentDomainTypes()
        {
            try
            {
                NamespaceInitals.Add(GetCompanyNamePrefix());
                LoadAssembliesFromBinDirectory();
                ReloadTypes();
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                throw typeLoadException.ToDetailedTypeLoadException();
            }
        }

        /// <summary>
        /// The add assembly.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        public static void AddAssembly(AssemblyName assemblyName)
        {
            AddTypesFromAssembly(assemblyName);
        }

        /// <summary>
        ///     The get assemblies.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            lock (SyncObject)
            {
                return DomainAssemblies;
            }
        }

        /// <summary>
        /// The get chilren classes from bin directory.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="isGenericType">
        /// if a cparent class is generic
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<Type> GetChilrenClassesFromBinDirectory(Type type, bool isGenericType)
        {
            IList<Type> childerenTypes;
            if (isGenericType)
            {
                childerenTypes =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(IsCompanyAssembly)
                        .SelectMany(t => t.GetTypes())
                        .Where(t => t.IsClass && t.IsAssignableToGenericType(type) && t != type)
                        .Select(t => t)
                        .ToList();
            }
            else
            {
                childerenTypes =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(IsCompanyAssembly)
                        .SelectMany(t => t.GetTypes())
                        .Where(t => t.IsClass && type.IsAssignableFrom(t) && t != type)
                        .Select(t => t)
                        .ToList();
            }

            return childerenTypes;
        }

        /// <summary>
        /// The get type.
        /// </summary>
        /// <param name="typeName">
        /// The type name.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetType(string typeName)
        {
            return GetTypes().FirstOrDefault(t => t.FullName == typeName);
        }

        /// <summary>
        /// The get type.
        /// </summary>
        /// <param name="typeName">
        /// The type name.
        /// </param>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// typeName and assemblyName must not be null
        /// </exception>
        public static Type GetType(string typeName, string assemblyName)
        {
            var type = GetType(typeName);
            if (type != null)
            {
                return type;
            }

            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new ArgumentNullException("assemblyName");
            }

            LoadAssembly(assemblyName);
            return GetType(typeName);
        }

        /// <summary>
        ///     The get types.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<Type> GetTypes()
        {
            lock (SyncObject)
            {
                return DomainTypes;
            }
        }

        /// <summary>
        /// The get types deriving from.
        /// </summary>
        /// <param name="isIncludingAbstract">
        /// The is including abstract.
        /// </param>
        /// <typeparam name="T">
        /// The type must not be generic, but can be a class or interface
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Type> GetTypesDerivingFrom<T>(bool isIncludingAbstract = true)
        {
            Func<Type, bool> predicate = t => true;

            if (!isIncludingAbstract)
            {
                predicate = t => !t.IsAbstract;
            }

            return GetTypesDerivingFrom<T>(predicate);
        }

        /// <summary>
        /// The get types deriving from.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <typeparam name="T">
        /// The type must not be generic, but can be a class or interface
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Type> GetTypesDerivingFrom<T>(Func<Type, bool> predicate)
        {
            lock (SyncObject)
            {
                return from type in DomainTypes where typeof(T).IsAssignableFrom(type) && predicate(type) && type != typeof(T) select type;
            }
        }

        /// <summary>
        /// The is company assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCompanyAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return false;
            }

            return !assembly.IsDynamic && IsCompanyNamespace(assembly.FullName);
        }

        /// <summary>
        /// The is company assembly.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCompanyAssembly(AssemblyName assemblyName)
        {
            if (assemblyName == null)
            {
                return false;
            }

            return IsCompanyNamespace(assemblyName.FullName);
        }

        /// <summary>
        /// The is company namespace.
        /// </summary>
        /// <param name="fullnamespace">
        /// The fullnamespace.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsCompanyNamespace(string fullnamespace)
        {
            if (string.IsNullOrEmpty(fullnamespace))
            {
                return false;
            }

            bool isCompanyNamespace = false;

            foreach (var namespaceInitial in NamespaceInitals)
            {
                isCompanyNamespace = fullnamespace.StartsWith(namespaceInitial);
                if (isCompanyNamespace)
                {
                    break;
                }
            }

            return isCompanyNamespace;
        }

        /// <summary>
        /// The is instantiable not generic class.
        /// </summary>
        /// <param name="testType">
        /// The test type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsInstantiableNotGenericClass(Type testType)
        {
            return testType.IsAbstract == false && testType.IsGenericTypeDefinition == false && testType.IsInterface == false;
        }

        /// <summary>
        ///     The reload types.
        /// </summary>
        public static void ReloadTypes()
        {
            lock (SyncObject)
            {
                try
                {
                    var toLoadAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(IsCompanyAssembly).ToList();
                    foreach (var domainAssembly in toLoadAssemblies)
                    {
                        var assemblyName = domainAssembly.GetName();
                        //Debug.Print($"Going to AddTypesFromAssembly for class library assembly {assemblyName}");
                        AddTypesFromAssembly(assemblyName);
                    }
                }
                catch (ReflectionTypeLoadException typeLoadException)
                {
                    foreach (var loaderException in typeLoadException.LoaderExceptions)
                    {
                        ShowDebugMessage("EXCEPTION: {0}", loaderException.Message);
                    }

                    throw;
                }
            }
        }

        private static void AddTypesFromAssembly(AssemblyName assemblyName)
        {
            if (assemblyName == null || !IsCompanyAssembly(assemblyName) || AssembliesWithoutStrongName.Contains(assemblyName.Name))
            {
                return;
            }

            Assembly assembly = null;

            try
            {
                debugInfoLoadingName = assemblyName.Name;

                assembly = Assembly.Load(assemblyName);

                AssembliesWithoutStrongName.Add(assemblyName.Name);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                ShowDebugMessage("File not found " + fileNotFoundException.FileName);
                return;
            }
            catch (Exception exception)
            {
                ShowDebugMessage(exception.Message);
                throw;
            }

            lock (SyncObject)
            {
                if (assembly == null || !IsCompanyAssembly(assembly) || DomainAssemblies.Contains(assembly))
                {
                    return;
                }

                var collection = assembly.GetTypes().Where(t => IsCompanyNamespace(t.Namespace));
                DomainTypes.AddRange(collection);

                DomainAssemblies.Add(assembly);
            }

            ////Debug.Print($"--Going to AddTypesFromAssembly for parent assembly {assemblyName}");
            var referencedAssemblies =
                assembly.GetReferencedAssemblies().Where(x => IsCompanyAssembly(x) && !AssembliesWithoutStrongName.Contains(x.Name));
            foreach (var referencedAssembly in referencedAssemblies)
            {
                ////Debug.Print($"    Going to AddTypesFromAssembly for child assembly {referencedAssembly}*:*{assemblyName}");
                AddTypesFromAssembly(referencedAssembly);
            }
        }

        private static string GetBinDirectory()
        {
            var isWebApplication = HttpRuntime.AppDomainAppId != null;
            return isWebApplication ? HttpRuntime.BinDirectory : AppDomain.CurrentDomain.BaseDirectory;
        }

        private static string GetCompanyNamePrefix()
        {
            string companyName = string.Empty;
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)//exe app
            {
                companyName = entryAssembly.FullName.Split('.').First();
                return companyName;
            }
            else //For test only
            {
                var directory = GetBinDirectory();

                var referencedPathsExe = Directory.GetFiles(directory, $"*Tests.dll");
                if (referencedPathsExe.Length > 0)
                {
                    companyName=referencedPathsExe[0].Split('\\').Last().Split('.').First();
                    return companyName;
                }
            }
            
            //web
            var assemblies =AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.FullName.Contains("System") && !x.FullName.Contains("Microsoft")).ToList();
            foreach (var assembly in assemblies)
            {
                try
                {
                    if (assembly.GetTypes().Any(t => typeof(IAppStartup).IsAssignableFrom(t)))
                    {
                        companyName= assembly.FullName.Split('.').First();
                        break;
                    }
                }
                catch (ReflectionTypeLoadException typeLoadException)
                {
                    foreach (var loaderException in typeLoadException.LoaderExceptions)
                    {
                        ShowDebugMessage("EXCEPTION: {0}", loaderException.Message);
                    }
                }
            }
            if (string.IsNullOrEmpty(companyName))
            {
                throw new Exception("No company found from entry assembly.");
            }

            return companyName;
        }

        private static void LoadAssembliesFromBinDirectory()
        {
            var directory = GetBinDirectory();

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(IsCompanyAssembly).ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            //var referencedPaths = Directory.GetFiles(directory, $"{GetCompanyNamePrefix()}*.dll");
            var test = Directory.GetFiles(directory, $"*.dll");
            var referencedPaths = NamespaceInitals.SelectMany(x => Directory.GetFiles(directory, $"{x}*.dll")).ToList();
            var referencedPathsExe = NamespaceInitals.SelectMany(x => Directory.GetFiles(directory, $"{x}*.exe")).ToList();
            referencedPaths = referencedPaths.Concat(referencedPathsExe).ToList();

            //referencedPaths = referencedPaths.Concat(Directory.GetFiles(directory, string.Format("{0}*.dll", "Foundation"))).ToArray();
            //referencedPaths = referencedPaths.Concat(listAllReferencedPaths).ToArray(); 
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();

            foreach (var targetPath in toLoad)
            {
                try
                {
                    var assemblyName = AssemblyName.GetAssemblyName(targetPath);
                    if (assemblyName != null)
                    {
                        AppDomain.CurrentDomain.Load(assemblyName);
                    }
                }
                catch (Exception exception)
                {
                    ShowDebugMessage(exception.Message);
                }
            }
        }

        private static Assembly LoadAssembly(string assemblyName)
        {
            bool isPath = assemblyName.Contains('\\') || assemblyName.Contains('/');

            if (isPath)
            {
                if (!File.Exists(assemblyName))
                {
                    throw new FileNotFoundException(assemblyName);
                }

                var assembly = Assembly.LoadFile(assemblyName);
                lock (SyncObject)
                {
                    DomainTypes.AddRange(assembly.GetTypes());
                }

                return assembly;
            }

            var directory = GetBinDirectory();
            if (!assemblyName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)
                && !assemblyName.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                assemblyName += ".dll";
            }

            assemblyName = Path.Combine(directory, assemblyName);

            return LoadAssembly(assemblyName);
        }

        private static void ShowDebugMessage(string message, params object[] args)
        {
            string toShow = string.Format("CurrentDomainTypes: Actual assembly {0} - ", debugInfoLoadingName) + string.Format(message, args);
            Debug.Print(toShow);
        }
    }
}