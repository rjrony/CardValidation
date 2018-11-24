using System;
using CardValidation.Repository;
using Effort.Provider;
using Infrastructure.Repository.Contracts;
using Infrastructure.Testing.Repository;
using Infrastructure.Testing.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidation.Api.IntegrationTests
{
    public class BasicRepositorySecured<T> : BaseIntegrationTest<T>
        where T : ITestStartup
    {
        private readonly bool basicDataNeed;

        private readonly bool securityNeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicRepositorySecured{T}"/> class.
        /// </summary>
        /// <param name="securityNeed">
        /// The security need.
        /// </param>
        /// <param name="basicDataNeed">
        /// basic data need
        /// </param>
        public BasicRepositorySecured(bool securityNeed, bool basicDataNeed)
        {
            this.securityNeed = securityNeed;
            this.basicDataNeed = basicDataNeed;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasicRepositorySecured{T}" /> class.
        /// </summary>
        public BasicRepositorySecured()
        {
            this.securityNeed = true;
            this.basicDataNeed = true;
        }

        /// <summary>
        ///     Gets or sets the repository.
        /// </summary>
        public IRepositoryCardValidation Repository { get; set; }

        /// <summary>
        ///     Tears down.
        /// </summary>
        [TestCleanup]
        public void AfterTestRun()
        {
            this.AfterTestRunBase();

            this.BasicDataCleanup();
        }

        /// <summary>
        ///     Sets up.
        /// </summary>
        [TestInitialize]
        public void BeforeTestRun()
        {
            this.BeforeTestRunBase();


            if (this.securityNeed)
            {
                //this.SecurityWork();
            }

            this.Repository = TestStartup.BootstrapperWebApi.DependencyResolver.BeginScope()
                .Resolve<IRepositoryCardValidation>();

            if (this.basicDataNeed)
            {
                this.BasicDataSetup();
            }
        }

        public static void ClassInitializeInitial()
        {
            ClassInitialize();
            //HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(MessageHeaders.DeviceClientId,ConstantValues.Customer.DeviceClient.DeviceClientId.Default);
            HttpClient.DefaultRequestHeaders.Add(ApiVersionSettings.ApiVersionHeaderName, ApiVersionSettings.CurrentMajorVersion + "." + ApiVersionSettings.CurrentMinorVersion);
            // todo : we will add real token in the below line
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "dummy value for authorization header");
        }

        /*
        /// <summary>
        /// The post as json async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task PostAsJsonAsync(string url, object content)
        {
            var response = await HttpClient.PostAsJsonAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, $"{url}:StatusCode {response.StatusCode}, result is {result}");
        }
        */


        /// <summary>
        ///     The basic data cleanup.
        /// </summary>
        private void BasicDataCleanup()
        {
            this.Repository.Dispose();

            var dbConnectionFactoryCustom = (DbConnectionFactoryTest<CardValidationContext>)TestStartup.BootstrapperWebApi.DependencyResolver
                    .Resolve<IDbConnectionFactoryCustom<CardValidationContext>>();
            EffortConnection effortConnection = (EffortConnection)dbConnectionFactoryCustom.Connection;
            effortConnection.Close();
            effortConnection.Dispose();
            dbConnectionFactoryCustom.Connection = null;
        }

        /// <summary>
        ///     The basic data setup.
        /// </summary>
        private async void BasicDataSetup()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
