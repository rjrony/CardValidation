using System.Threading.Tasks;

using Moq;
using FizzWare.NBuilder;

using CardValidation.Api.Controllers;
using CardValidation.Api.Dtos;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using CardValidation.Repository.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidation.Api.Tests
{
    [TestClass]
    public class CardControllerTests
    {
        private static Mock<ICardValidatorService> mockedCardValidatorService;
        private static CardController cardController;

        [ClassInitialize]
        public static void SetUp(TestContext a)
        {
            mockedCardValidatorService = new Mock<ICardValidatorService>();

            var validateResultView = Builder<ValidateResultView>.CreateNew()
                .With(x => x.CardType = CardTypeEnum.Visa.ToString("G"))
                .With(x => x.Result = ResultEnum.Valid.ToString("G"))
                .Build();
            mockedCardValidatorService.Setup(x => x.CardValidate(It.IsAny<CardValidationQueryRequest>()))
                .Returns(Task.FromResult(validateResultView));

            cardController = new CardController(mockedCardValidatorService.Object);
        }

        [ClassCleanup]
        public static void TearDown()
        {

        }

        [TestMethod]
        public async Task GetValidateCardTest()
        {
            var validateResultView = await cardController.GetValidateCard("4412345678901234","012020");
            Assert.AreEqual(validateResultView.Result, ResultEnum.Valid.ToString("G"));
        }

    }
}
