// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureAppendBlobTarget.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging
{
    using System.Net;
    using System.Text;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using NLog;
    using NLog.Common;
    using NLog.Config;
    using NLog.Layouts;
    using NLog.Targets;

    /// <summary>
    /// The azure append blob target.
    /// </summary>
    [Target("AzureAppendBlob")]
    public sealed class AzureAppendBlobTarget : TargetWithLayout
    {
        private CloudAppendBlob _blob;

        private CloudBlobClient _client;

        private CloudBlobContainer _container;

        /// <summary>
        /// Gets or sets the blob name.
        /// </summary>
        [RequiredParameter]
        public Layout BlobName { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        [RequiredParameter]
        public Layout ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        [RequiredParameter]
        public Layout Container { get; set; }

        //private string _connectionString;

        /// <summary>
        /// The initialize target.
        /// </summary>
        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            this._client = null;
        }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        protected override void Write(LogEventInfo logEvent)
        {
            var connectionString = this.ConnectionString.Render(logEvent);

            if (this._client == null || !string.Equals(null, connectionString, System.StringComparison.OrdinalIgnoreCase))
            {
                this._client = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();
                InternalLogger.Debug("Initialized connection to {0}", connectionString);
            }

            string containerName = this.Container.Render(logEvent);
            string blobName = this.BlobName.Render(logEvent);

            if (this._container == null || this._container.Name != containerName)
            {
                this._container = this._client.GetContainerReference(containerName);
                InternalLogger.Debug("Got container reference to {0}", containerName);

                if (this._container.CreateIfNotExists())
                {
                    InternalLogger.Debug("Created container {0}", containerName);
                }

                this._blob = null;
            }

            if (this._blob == null || this._blob.Name != blobName)
            {
                this._blob = this._container.GetAppendBlobReference(blobName);

                if (!this._blob.Exists())
                {
                    try
                    {
                        this._blob.Properties.ContentType = "text/plain";
                        this._blob.CreateOrReplace(AccessCondition.GenerateIfNotExistsCondition());
                        InternalLogger.Debug("Created blob: {0}", blobName);
                    }
                    catch (StorageException ex) when (ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.Conflict)
                    {
                        // to be expected
                    }
                }
            }

            this._blob.AppendText(this.Layout.Render(logEvent) + "\r\n", Encoding.UTF8);
        }
    }
}