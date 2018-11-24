// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Blockbonds">
//   Copyright © Blockbonds. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace AnxConsole
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    using Blockbonds.AnxClient.Contracts.Dto.Request;

    internal class Program
    {
        private static void Main(string[] args)
        {
            //string key = "e3b25cee-5bbb-4615-91d5-0d423bc03329";
            //string secret = "z1dIWQU9Lh3zSlENJoRVBV41wDsY2Ua9xT2Orz3A8gND9tQNfdIjG9jpLNHnJhSBl7ZnI9bAn+zUvyytNWuItw==";
            string key = "d7516bcd-ab98-48fa-839e-060de41c43fe";
            string secret = "m+ZNM9r/a15vD+bIZVxdiFeDMUeZ+W5BkM6AD1gvsx9vJT1Depk6kHoBMrHrbEO+tRD5HjwRMdX76aWQM0i5jw==";
            string path = "money/info"; //for version 3 example is: "api/3/receive"

            long unixTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long ts = unixTimestamp * 1000 * 1000 * 1000;

            //string command = "{\"nonce\":" + ts + "}";
            //string command = $"nonce={ts}";
            MoneyInfoDto moneyInfoDto = new MoneyInfoDto();
            moneyInfoDto.Nonce = ts;
            string command = moneyInfoDto.ToQueryString();

            HMACSHA512 hmacsha512 = new HMACSHA512(Convert.FromBase64String(secret));
            string input = path + '\0' + command;
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(input));
            byte[] hashData = hmacsha512.ComputeHash(stream);
            string sign = Convert.ToBase64String(hashData);

            var request = System.Net.WebRequest.Create("https://uat.anxpro.com/api/2/" + path) as System.Net.HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";

            request.ContentType = "application/json";
            request.Headers.Add("rest-key", key);
            request.Headers.Add("rest-sign", sign);

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(command);
            using (var writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);
            }

            string responseContent = null;
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}