// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedRepositoryLocker.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;

    using Infrastructure.Logging.Contracts;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    /// The named repository locker.
    /// </summary>
    /// <typeparam name="TContext">
    /// Type of the context
    /// </typeparam>
    public class NamedRepositoryLocker<TContext> : INamedLocker
        where TContext : ContextBase, new()
    {
        private DbConnection connection;

        private TContext context;

        private bool disposed = false;

        private bool isCommited;

        private DbContextTransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRepositoryLocker{TContext}"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public NamedRepositoryLocker(IRepository<TContext> repository, ILogger logger)
        {
            this.Logger = logger;
            this.context = ((IRepositoryContext<TContext>)repository).Context;
            this.transaction = this.context.Database.BeginTransaction();
            this.connection = this.context.Database.Connection;
        }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     The commit.
        /// </summary>
        public void Commit()
        {
            this.context.SaveChanges();
            this.transaction.Commit();
            this.isCommited = true;
        }

        /// <summary>
        ///     Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The lock.
        /// </summary>
        /// <param name="lockName">
        /// The lock name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Lock(string lockName)
        {
            return CreateLock(this.connection, this.transaction, lockName, this.Logger);
        }

        /// <summary>
        ///     The rollback.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (!this.isCommited)
                {
                    this.transaction.Rollback();
                }

                this.connection = null;
                this.transaction = null;
                this.context = null;
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }

        private static bool CreateLock(DbConnection connection, DbContextTransaction transaction, string subject, ILogger logger)
        {
            var cmd = connection.CreateCommand();
            cmd.Transaction = transaction.UnderlyingTransaction;
            cmd.CommandText = "sp_getapplock";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = cmd.CreateParameter();
            param.ParameterName = "Resource";
            param.DbType = DbType.AnsiString;
            param.Value = subject;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "LockMode";
            param.DbType = DbType.AnsiString;
            param.Value = "Exclusive";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "LockOwner";
            param.DbType = DbType.AnsiString;
            param.Value = "Transaction";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = cmd.CreateParameter();
            param.ParameterName = "LockTimeout";
            param.DbType = DbType.Int64;
            param.Value = 5000;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            var returnValue = cmd.CreateParameter();
            returnValue.DbType = DbType.Int32;
            returnValue.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            var lockResult = (int)returnValue.Value;
            if (lockResult == 0 || lockResult == 1)
            {
                var id = subject;
                logger.Debug(() => string.Format("Lock for subject id {0} successful", id));
            }
            else
            {
                var id = subject;
                logger.Warning(() => string.Format("Lock for subject id {0} failed with sp_getapplock error code {1}", id, lockResult));
                return false;
            }

            return true;
        }
    }
}