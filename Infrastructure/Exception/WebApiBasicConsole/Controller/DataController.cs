// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataController.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception.WebApiBasicConsole.Controller
{
    using System.Collections.Generic;
    using System.Web.Http;


    using Infrastructure.Exception.ErrorCodes;

    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The data controller.
    /// </summary>
    public class DataController : ApiController
    {
        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="List" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Data> Get()
        {
            var container = new UnityContainer();
          //  container.RegisterType<IAppExceptionGenerator, CustomHttpResponseExceptionGenerator>();
            container.RegisterType<ILogger, NullLogger>();

            // throw container.Resolve<ExternalRequestException>().GetException(new External(100, new List<string> {"error1", "error2"}, "something bad happened"));

            //var exceptionMsg = new NestedExceptionMessage
            //{
            //    ErrorCode = BaseErrorCode.InvalidInput,
            //    Message = "error occured",
            //    NestedMessages = new List<NestedMessage> {
            //    new NestedMessage
            //    {
            //        Message = "error occured 11",
            //        NestedMessages = new List<NestedMessage>
            //        {
            //            new NestedMessage {Message = "error occured 21"},
            //            new NestedMessage {Message = "error occured 22" }
            //        }

            //    },
            //    new NestedMessage
            //        {
            //            Message = "error occured 12"
            //        }
            //    }
            //};
            throw container.Resolve<ValidationException>()
                .GetException(
                    new List<NestedMessage>
                        {
                            new NestedMessage
                                {
                                    Message = "NM 1",
                                    NestedMessages =
                                        new List<NestedMessage>
                                            {
                                                new NestedMessage
                                                    {
                                                        Message = "NM 11",
                                                        NestedMessages =
                                                            new List
                                                            <NestedMessage>
                                                                {
                                                                    new NestedMessage
                                                                        {
                                                                            Message
                                                                                =
                                                                                "NM 11"
                                                                        }
                                                                }
                                                    },
                                                new NestedMessage { Message = "NM 12" }
                                            }
                                },
                            new NestedMessage { Message = "NM 2" },
                            new NestedMessage { Message = "NM 3" }
                        },
                    "Invalid Input",
                    BaseErrorCodes.InvalidInput,
                    "Something bad happened");

            //return new List<Data>()
            //    {
            //        new Data() { Id = 1, Name = "Laptop" },
            //        new Data() { Id = 2, Name = "IPhone" }
            //    };
        }
    }

    /// <summary>
    ///     The data.
    /// </summary>
    public class Data
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}