// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestServerCustom.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Testing.WebApi
{
    using System;
    using System.Net.Http;

    using Microsoft.Owin.Testing;

    public class TestServerCustom
        //where T : ITestStartup
    {
        private static TestServer Server;

        private static HttpClient HttpClient;

        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TestServer"/>.
        /// </returns>
        public static TestServer Create<T>()
        {
            return Server ?? (Server = TestServer.Create<T>());
        }

        public static HttpClient GetHttpClient()
        {
            return HttpClient ?? (HttpClient = new HttpClient(Server.Handler) { BaseAddress = new Uri("http://localhost") });
        }
    }
}