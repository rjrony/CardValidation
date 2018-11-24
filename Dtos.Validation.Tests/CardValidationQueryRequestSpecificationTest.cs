using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Api.Dtos.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidation.Api.Dtos.Validation.Tests
{
    [TestClass]
    public class CardValidationQueryRequestSpecificationTest : BaseSpecificationTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ClassInitialize();
        }
        [TestMethod]
        public void InvalidCard()
        {
            var request = new CardValidationQueryRequest
            {
                CardNumber = "12345678901234",
                ExpiryDate = "0120230"
            };
            var validationResults = this.Validate(request);
            Assert.AreEqual(validationResults.Errors.Count, 2);
        }
    }
}
