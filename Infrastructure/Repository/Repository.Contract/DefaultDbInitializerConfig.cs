namespace Infrastructure.Repository.Contracts
{
    using System.Configuration;

    class DefaultDbInitializerConfig : IDbInitializerConfig
    {
        public DefaultDbInitializerConfig()
        {
            var exceptionOnMigrationMissmatch = ConfigurationManager.AppSettings["ExceptionOnMigrationMissmatch"];
            bool exceptionOnMismatch;

            if (bool.TryParse(exceptionOnMigrationMissmatch, out exceptionOnMismatch))
            {
                this.ExceptionOnMigrationMissmatch = exceptionOnMismatch;
            }
            else
            {
                this.ExceptionOnMigrationMissmatch = false;
            }
        }

        public bool ExceptionOnMigrationMissmatch { get; set; }
    }
}