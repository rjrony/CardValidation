// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbBackup.cs" company="">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Infrastructure.Interception.Contract;
using Infrastructure.Logging;
using Infrastructure.Logging.Contracts;

namespace Infrastructure.Repository
{
    /// <summary>
    ///     The db backup.
    /// </summary>
    public class DbBackup
    {
        private readonly IDependencyResolver DependencyResolver;
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbBackup"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        public DbBackup(IDependencyResolver dependencyResolver)
        {
            this.DependencyResolver = dependencyResolver;
        }

        /// <summary>
        ///     Gets the backup path.
        /// </summary>
        public string BackupPath { get; private set; }

        private ILogger Logger
        {
            get { return this.logger ?? (this.logger = this.DependencyResolver.GetCurrentDependencyResolver().Resolve<ILogger>()); }
        }

        /// <summary>
        /// Backup the database provided with the context.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// True if the backup was successfull otherwise false
        ///     The <see cref="bool"/>.
        /// </returns>
        public bool Backup(DbContext context)
        {
            if (!context.Database.Exists())
            {
                return true;
            }

            this.BackupPath = this.DetermineBaseBackupPath(context);

            var fileName = "{0} {1}.bak".FormatWith(DateTime.Now.ToString("yy-MM-dd HH-mm-ss"),
                context.Database.Connection.Database);
            var contextFolder = context.Database.Connection.Database;
            var contextPath = Path.Combine(this.BackupPath, contextFolder);
            var fullFilePath = Path.Combine(contextPath, fileName);

            this.Logger.Info(() => "Database is being backed up...");
            var sql =
                @"Backup database [{0}] to disk = N'{1}'  with NOFORMAT, INIT, NAME = 'Backup of {0}', NOREWIND, NOUNLOAD, NOSKIP, DESCRIPTION = 'Backup of {0}'"
                    .FormatWith(context.Database.Connection.Database, fullFilePath);
            try
            {
                // Doing this in non-transactional scope
                this.CreateDirectory(context, contextPath);

                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
            }
            catch (Exception ex)
            {
                this.Logger.Error(() => "Error while backing up database '{0}'...".FormatWith(context.Database.Connection.Database));
                this.Logger.Exception(ex);
                return false;
            }

            this.Logger.Info(
                () =>
                    "Database '{0}' successfully backed up to '{1}'".FormatWith(context.Database.Connection.Database,
                        fullFilePath));

            //this.Logger.Debug(() => "Cleaning up old backups..");
            ////this.CleanupBackups(contextPath);
            return true;
        }

        private void CleanupBackups(string path)
        {
            var all = Directory.EnumerateFiles(path);
            var allFiles = all.Select(s => new FileInfo(s)).ToList();

            var filesToKeep = allFiles.OrderByDescending(o => o.LastWriteTime).Take(2);

            var fileToDelete = new List<FileInfo>(allFiles);
            fileToDelete.RemoveAll(info => filesToKeep.Any(p => p.Name == info.Name));

            if (!fileToDelete.Any())
            {
                this.Logger.Debug(() => "Nothing found to cleanup!");
                return;
            }

            foreach (var file in fileToDelete)
            {
                var file1 = file;
                this.Logger.Debug(() => "Delete file '{0}'".FormatWith(file1.Name));
                file1.Delete();
            }
        }

        private void CreateDirectory(DbContext dbContext, string directoryPath)
        {
            var sql = @"EXEC master.dbo.xp_create_subdir '{0}'".FormatWith(directoryPath);

            dbContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
        }

        private string DetermineBaseBackupPath(DbContext dbContext)
        {
            const string Sql =
                @"EXEC  master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer',N'BackupDirectory'";
            var defaultBackupPath = dbContext.Database.SqlQuery<DefaultBackupPath>(Sql);

            const string BaseFolder = "DbBackup_BeforeMigration";
            var path = Path.Combine(defaultBackupPath.First().Data, BaseFolder);

            return path;
        }
    }

    internal class DefaultBackupPath
    {
        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}