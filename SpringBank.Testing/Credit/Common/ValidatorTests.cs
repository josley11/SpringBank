using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpringBank.Common;

namespace SpringBank.Testing.Credit.Common
{
    internal sealed class ValidatorTests
    {
        [TestClass]
        public class IsValidCurrencyAmount
        {
            [TestMethod]
            public void WhenInvoked_ThenExpectedValidationReturned()
            {
                //Act & Assert
                Assert.IsTrue(Validator.IsValidCurrencyAmount(0M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(0.00M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(0.01M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(0.1M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(0.12M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(1M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(1.00M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(1.10M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(12.99M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(1000.99M));
                Assert.IsTrue(Validator.IsValidCurrencyAmount(01.00M));

                Assert.IsFalse(Validator.IsValidCurrencyAmount(1.1234M));

            }
        }
    }
}
