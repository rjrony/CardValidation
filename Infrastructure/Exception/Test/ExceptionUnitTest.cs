// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionUnitTest.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Test
{
    using System;
    using System.Collections.Generic;

    using Infrastructure;
    using Infrastructure.Exception;

    using Infrastructure.Exception.ErrorCodes;
  //  using Infrastructure.Exception.Web;
    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using External = Infrastructure.Exception.External;

    /// <summary>
    ///     The exception unit test.
    /// </summary>
    [TestClass]
    public class ExceptionUnitTest
    {
        public IUnityContainer container { get; set; }
        public ExceptionUnitTest()
        {
            this.container = new UnityContainer();
            container.RegisterType<ILogger, NullLogger>();
            container.RegisterType<IDependencyResolver, DependencyResolver>();
            container.RegisterType<IOperationContext, OperationContext>();

            ((IUnitOfWorkContainerSetter)ServiceLocator.Instance).SetUnitOfWorkContainer(container);
        }

        /// <summary>
        ///     The test bad request response exception.
        /// </summary>
        [TestMethod]
        public void TestBadRequestResponseException()
        {
          //  var container = new UnityContainer();
          //  container.RegisterType<IAppExceptionGenerator, AppExceptionGenerator>();
            //container.RegisterType<ILogger, NullLogger>();
            //container.RegisterType<IDependencyResolver, DependencyResolver>();
            //container.RegisterType<IOperationContext, OperationContext>();
            // container.RegisterType<BadRequestResponseException>();
            ////var baseErrorCode = BaseErrorCode.InvalidInput;
            var exception = container.Resolve<BadRequestException>()
                .GetException(BaseErrorCodes.InvalidInput, new List<string> { "Error input", "Error input 2" });
            //    throw GetValue(container, new List<object> { errorList, baseErrorCode });
        }



        /// <summary>
        ///     The test external request response exception.
        /// </summary>
        [TestMethod]
        public void TestExternalRequestResponseException()
        {
            Dictionary<int, string> test = new Dictionary<int, string>();
            test.Add(1, "test");
          //  var container = new UnityContainer();
            //   container.RegisterType<IAppExceptionGenerator, AppExceptionGenerator>();
            //container.RegisterType<ILogger, NullLogger>();
            //container.RegisterType<IDependencyResolver, DependencyResolver>();
            //container.RegisterType<IOperationContext, OperationContext>();

            UserRegistrationInvalidInput input = null;
            Exception exceptionBasic2 = container.Resolve<InvalidInputException>().GetException(input, "The account is temporary locked.");

            Exception exceptionBasic = container.Resolve<InvalidInputException>().GetException(BaseErrorCodes.AuthenticationInvalidAccountLocked, "The account is temporary locked.", test);
            //throw exception;

            var exception = container.Resolve<InvalidInputException>()
                .GetException(
                    new List<ExceptionMessage>
                        {
                            new ExceptionMessage
                                {
                                    Message = "Error",
                                    ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                    NestedMessages = null,
                                    Type = ExceptionErrorMassageType.Nested,
                                    Data = test
                                }
                        });

            var exception2 = container.Resolve<InvalidInputException>()
                .GetException(new List<ExceptionMessage>
                                  {
                                      new ExceptionMessage
                                          {
                                              Message = "Error",
                                              ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                              NestedMessages = null,
                                              Type = ExceptionErrorMassageType.Nested,
                                              Data = test
                                          }
                                  });

            var exceptionExternal = container.Resolve<ExternalRequestException>().GetException(new External(0, null, null));
        }

        /// <summary>
        /// The test invalid input exception.
        /// </summary>
        [TestMethod]
        public void TestInvalidInputException()
        {
         //   var container = new UnityContainer();
          //  container.RegisterType<IAppExceptionGenerator, AppExceptionGenerator>();
            //container.RegisterType<ILogger, NullLogger>();
            //container.RegisterType<IDependencyResolver, DependencyResolver>();
            //container.RegisterType<IOperationContext, OperationContext>();
            // container.RegisterType<BadRequestResponseException>();
            ////var baseErrorCode = BaseErrorCode.InvalidInput;
            var exception = container.Resolve<InvalidInputException>().GetException(new List<ExceptionMessage>
                                  {
                                      new ExceptionMessage
                                          {
                                              Message = "Error1",
                                              ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                              NestedMessages = null,
                                              Type = ExceptionErrorMassageType.Nested
                                          },
                                      new ExceptionMessage
                                          {
                                              Message = "Error2",
                                              ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                              NestedMessages = null,
                                              Type = ExceptionErrorMassageType.Nested
                                          }
                                  });
            //    throw GetValue(container, new List<object> { errorList, baseErrorCode });
        }

        /// <summary>
        /// The test invalid exception with nested specific error code.
        /// </summary>
        [TestMethod]
        public void TestInvalidExceptionWithNestedSpecificErrorCode()
        {
         //   var container = new UnityContainer();
         //   container.RegisterType<IAppExceptionGenerator, AppExceptionGenerator>();
            //container.RegisterType<ILogger, NullLogger>();
            //container.RegisterType<IDependencyResolver, DependencyResolver>();
            //container.RegisterType<IOperationContext, OperationContext>();

            var exceptoinMessageList = new List<ExceptionMessage>();
            var exceptionMessage = new ExceptionMessage
            {
                                           Message = "Error 2",
                                           ErrorCodeValue = BaseErrorCodes.AccountAccessDenied,
                                           NestedMessages = new List<ExceptionMessage>()
                                                                {
                                                                    new ExceptionMessage
                                                                        {
                                                                            Message = "Error 3",
                                                                            ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                                                            NestedMessages = null,
                                                                            Type = ExceptionErrorMassageType.Nested
                                                                        }
                                                                },
                                           Type = ExceptionErrorMassageType.Root
                                       };
            exceptoinMessageList.Add(exceptionMessage);
            exceptoinMessageList.Add(new ExceptionMessage
            {
                                             NestedMessages = null,
                                             ErrorCodeValue = BaseErrorCodes.InvalidInput,
                                             Message = "Yet another error",
                                             Type = ExceptionErrorMassageType.Nested
            });
            var exception = container.Resolve<InvalidInputException>()
                .GetException(exceptoinMessageList, "Error 1");
        }

        /// <summary>
        /// The test validation exception.
        /// </summary>
        [TestMethod]
        public void TestValidationException()
        {
          //  var container = new UnityContainer();
          //  container.RegisterType<IAppExceptionGenerator, AppExceptionGenerator>();
            //container.RegisterType<ILogger, NullLogger>();
            //container.RegisterType<IDependencyResolver, DependencyResolver>();
            //container.RegisterType<IOperationContext, OperationContext>();

            var exception =
                container.Resolve<ValidationException>()
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
                                                            Message =
                                                                "NM 11",
                                                            NestedMessages
                                                                =
                                                                new List<NestedMessage>
                                                                    {
                                                                        new NestedMessage
                                                                            {
                                                                                Message
                                                                                    =
                                                                                    "NM 11"
                                                                            }
                                                                    }
                                                        },
                                                    new NestedMessage
                                                        {
                                                            Message =
                                                                "NM 12"
                                                        }
                                                }
                                    },
                                new NestedMessage { Message = "NM 2" },
                                new NestedMessage { Message = "NM 3" }
                            },
                        "Invalid Input",
                        BaseErrorCodes.InvalidInput,
                        "Something bad happened");
        }

        //        container.Resolve<BadRequestException>(
        //    return
        //{

        //private static Exception GetValue(UnityContainer container, List<object> parameters)
        //            new ParameterOverride("errorList", new List<string>() { "Error input" }),
        //            new ParameterOverride("baseErrorCode", BaseErrorCode.InvalidInput)).Exception;
        //}
    }

    public class UserRegistrationInvalidInput : EnumerationSingleton<int, UserRegistrationInvalidInput>
    {
        public UserRegistrationInvalidInput()
            : base(10052000)
        {
        }
    }
}