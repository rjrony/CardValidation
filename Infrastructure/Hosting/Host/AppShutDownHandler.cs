// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppShutDownHandler.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;

    using NLog;

    using ILogger = Infrastructure.Logging.Contracts.ILogger;

    /// <summary>
    ///     The app shut down handler.
    /// </summary>
    public static class AppShutDownHandler
    {
        /// <summary>
        /// The report shut down reason.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public static void ReportShutDownReason(ILogger logger)
        {
            // config is disposed, so recreate
            LogManager.Configuration = new NLog.LogFactory().Configuration;

            logger.Warning(() => string.Format("Shutdown reason is {0}", HostingEnvironment.ShutdownReason));

            var runtime =
                (HttpRuntime)
                typeof(System.Web.HttpRuntime).InvokeMember(
                    "_theRuntime",
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField,
                    null,
                    null,
                    null);

            if (runtime == null)
            {
                ShutDownLogManager();
                return;
            }

            var shutDownMessage =
                (string)
                runtime.GetType()
                    .InvokeMember(
                        "_shutDownMessage",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                        null,
                        runtime,
                        null);

            var shutDownStack =
                (string)
                runtime.GetType()
                    .InvokeMember(
                        "_shutDownStack",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                        null,
                        runtime,
                        null);

            var messageToLog = string.Format("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}", shutDownMessage, shutDownStack);

            switch (HostingEnvironment.ShutdownReason)
            {
                case ApplicationShutdownReason.HostingEnvironment:
                case ApplicationShutdownReason.ChangeInGlobalAsax:
                case ApplicationShutdownReason.ConfigurationChange:
                case ApplicationShutdownReason.UnloadAppDomainCalled:
                case ApplicationShutdownReason.ChangeInSecurityPolicyFile:
                case ApplicationShutdownReason.BinDirChangeOrDirectoryRename:
                case ApplicationShutdownReason.BrowsersDirChangeOrDirectoryRename:
                case ApplicationShutdownReason.CodeDirChangeOrDirectoryRename:
                case ApplicationShutdownReason.ResourcesDirChangeOrDirectoryRename:
                case ApplicationShutdownReason.IdleTimeout:
                case ApplicationShutdownReason.PhysicalApplicationPathChanged:
                case ApplicationShutdownReason.HttpRuntimeClose:
                case ApplicationShutdownReason.BuildManagerChange:
                    logger.Warning(() => messageToLog);
                    break;
                case ApplicationShutdownReason.InitializationError:
                case ApplicationShutdownReason.MaxRecompilationsReached:
                    logger.Error(() => messageToLog);
                    break;
                default:
                    logger.Warning(() => messageToLog);
                    break;
            }

            ////if (!EventLog.SourceExists(".NET Runtime"))
            //{
            ////    EventLog.CreateEventSource(".NET Runtime", "Application");
            ////}

            //EventLog log = new EventLog();
            //log.Source = ".NET Runtime";
            //log.WriteEntry(messageToLog,EventLogEntryType.Error);
            ShutDownLogManager();
        }

        private static void ShutDownLogManager()
        {
            if (LogManager.Configuration != null)
            {
                LogManager.Shutdown();
            }
        }
    }
}