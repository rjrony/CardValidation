// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception.WebApiBasicConsole
{
    using System;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web.Http;

    using Microsoft.Owin.Hosting;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.ReadLine();
            }
        }
    }

    /// <summary>
    ///     The ExceptionContainer interface.
    /// </summary>
    public interface IExceptionContainer : _Exception
    {
    }

    /// <summary>
    ///     The my exception.
    /// </summary>
    public class MyException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MyException" /> class.
        /// </summary>
        public MyException()
        {
            this.ExceptionContainer = new HttpResponseException(HttpStatusCode.Accepted);
        }

        /// <summary>
        ///     Gets or sets the exception container.
        /// </summary>
        public Exception ExceptionContainer { get; set; }
    }
}