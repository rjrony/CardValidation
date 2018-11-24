// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseIntegrationTest.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception.Contract;

namespace Infrastructure.Testing.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Effort.Provider;

    using Infrastructure.Repository.Contracts;

    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    /// <summary>
    /// The base integration test.
    /// </summary>
    /// <typeparam name="T">
    /// startup class
    /// </typeparam>
    [TestClass]
    public abstract class BaseIntegrationTest<T>
        where T : ITestStartup
    {
        /// <summary>
        ///     Gets the server.
        /// </summary>
        public static TestServer Server { get; private set; }

        /// <summary>
        ///     Gets or sets the http client.
        /// </summary>
        public static HttpClient HttpClient { get; set; }

        /// <summary>
        ///     The class cleanup.
        /// </summary>
        public static void ClassCleanup()
        {
            //Server.Dispose();
        }

        /// <summary>
        ///     The class initialize.
        /// </summary>
        public static void ClassInitialize()
        {
            EffortProviderConfiguration.RegisterProvider(); ////ToDo this code will move to Testing.Repository project for good design 
            Server = TestServerCustom.Create<T>();
            HttpClient = TestServerCustom.GetHttpClient();


        }

        /// <summary>
        ///     Tears down.
        /// </summary>
        public void AfterTestRunBase()
        {
            //HttpClient.Dispose();
        }

        /// <summary>
        ///     Sets up.
        /// </summary>
        public void BeforeTestRunBase()
        {
            HttpClient = TestServerCustom.GetHttpClient();
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <typeparam name="TView">
        /// Generic Return Type
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TView> GetAsync<TView>(string url)
        {
            var response = await HttpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
            TView contentResult = await response.Content.ReadAsAsync<TView>();
            return contentResult;
        }

        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <typeparam name="TContext">
        /// database context
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository"/>.
        ///     repository
        /// </returns>
        public IRepository GetRepository<TContext>(IDependencyResolver dependencyResolver) where TContext : class, new()
        {
            return dependencyResolver.Resolve<IRepository<TContext>>();
        }

        /// <summary>
        /// The post as form url encoded async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="TView">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TView> PostAsFormUrlEncodedAsync<TView>(string url, object data) where TView : class
        {
            var content =
                data.GetType()
                    .GetProperties()
                    .Select(
                        x =>
                        new KeyValuePair<string, string>(
                            x.Name,
                            x.GetGetMethod().Invoke(data, null) == null ? string.Empty : x.GetGetMethod().Invoke(data, null).ToString()))
                    .ToList();

            var urlEncodedContent = new FormUrlEncodedContent(content);
            var response = await HttpClient.PostAsync(url, urlEncodedContent);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
            TView contentResult = await response.Content.ReadAsAsync<TView>();
            return contentResult;
        }

        /// <summary>
        /// The post as json async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <typeparam name="TView">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TView> PostAsJsonAsync<TView>(string url, object content) where TView : class
        {
            var response = await HttpClient.PostAsJsonAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
            TView contentResult = await response.Content.ReadAsAsync<TView>();
            return contentResult;
        }
        public async Task<TView> PutAsJsonAsync<TView,TCommand>(string url, TCommand content) where TView : class
        {
            var response = await HttpClient.PutAsJsonAsync(url, content);
            
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
            TView contentResult = await response.Content.ReadAsAsync<TView>();
            return contentResult;
        }
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
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
        }

        /// <summary>
        /// The post async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <typeparam name="TView">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TView> PostAsync<TView>(string url, HttpContent content) where TView : class
        {
            var response = await HttpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, this.FormattedMessage(url, response.StatusCode, result));
            TView contentResult = await response.Content.ReadAsAsync<TView>();
            return contentResult;
        }

        /// <summary>
        ///     The create http client.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpClient" />.
        /// </returns>
        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient(Server.Handler) { BaseAddress = new Uri("http://localhost") };
            return httpClient;
        }

        private string FormattedMessage(string url, HttpStatusCode statusCode, string result)
        {
            var errorMessage = $"{System.Environment.NewLine}" + $"Url: {url}{System.Environment.NewLine}"
                               + $"Status Code: {(int)statusCode}{System.Environment.NewLine}" + $"Details: {result}";

            return errorMessage;
        }
    }
}