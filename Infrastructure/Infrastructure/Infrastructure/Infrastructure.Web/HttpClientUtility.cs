using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Infrastructure.Web
{
    public static class HttpClientUtility
    {
        /// <summary>
        /// Utility method for getting HttpClient
        /// </summary>
        /// <param name="client">The instance of HttpClient</param>
        /// <param name="baseAddress">The base Address for the HttpClient</param>
        /// <param name="keepAlive">Indicates if keep Alive should be set</param>
        /// <param name="defaultReqestHeaders">The default request headers</param>
        /// <param name="connectionLeaseTimeOut"></param>
        /// <returns></returns>
        public static HttpClient GetHttpClient(HttpClient client, Uri baseAddress, bool keepAlive = true,
            List<KeyValuePair<string, string>> defaultReqestHeaders = null, int connectionLeaseTimeOut = 120000)
        {
            if (client == null && baseAddress != null)
            {
                client = new HttpClient(new IntegrateCorrelationIdHandler())
                {
                    BaseAddress = baseAddress,
                };

                if (keepAlive)
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Connection.Clear();
                    client.DefaultRequestHeaders.ConnectionClose = false;
                    client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                    ServicePointManager.FindServicePoint(baseAddress).ConnectionLeaseTimeout = connectionLeaseTimeOut;
                }
                if (defaultReqestHeaders != null && defaultReqestHeaders.Any())
                {
                    defaultReqestHeaders.ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                }

            }
            return client;
        }
    }
}
