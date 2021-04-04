using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpringBank.Business;
using SpringBank.Models;

namespace SpringBank.Testing.Credit.Business
{
    internal sealed class CreditDecisionWorkerTests
    {
        [TestClass]
        public class IsValidAppliedAmount
        {
            [TestMethod]
            public void WhenInvoked_ThenExpectedValidationReturned()
            {
                //Arrange
                var creditDecisionRules = A.Fake<ICreditDecisionRules>();
                var creditDecisionWorker = new CreditDecisionWorker(creditDecisionRules);

                //Variants of applied amount
                var searchRequest1 = CreateCreditSearchRequest(0M, 1, 0M); 
                var searchRequest2 = CreateCreditSearchRequest(200M, 1, 0M);
                var searchRequest3 = CreateCreditSearchRequest(-200, 1, 0M);                

                //variants of term (in months)
                var searchRequest4 = CreateCreditSearchRequest(200M, 0, 0M);
                var searchRequest5 = CreateCreditSearchRequest(200M, 1, 0M);
                var searchRequest6 = CreateCreditSearchRequest(200M, -1, 0M);

                //variants of pre existing credit amount
                var searchRequest7 = CreateCreditSearchRequest(200M, 1, 0M);
                var searchRequest8 = CreateCreditSearchRequest(200M, 1, 200M);
                var searchRequest9 = CreateCreditSearchRequest(200M, 1, -200M);

                //Act & Assert
                Assert.IsTrue(creditDecisionWorker.IsValidRequest(searchRequest1, out _));
                Assert.IsTrue(creditDecisionWorker.IsValidRequest(searchRequest2, out _));
                Assert.IsFalse(creditDecisionWorker.IsValidRequest(searchRequest3, out _));

                Assert.IsFalse(creditDecisionWorker.IsValidRequest(searchRequest4, out _));
                Assert.IsTrue(creditDecisionWorker.IsValidRequest(searchRequest5, out _));
                Assert.IsFalse(creditDecisionWorker.IsValidRequest(searchRequest6, out _));

                Assert.IsTrue(creditDecisionWorker.IsValidRequest(searchRequest7, out _));
                Assert.IsTrue(creditDecisionWorker.IsValidRequest(searchRequest8, out _));
                Assert.IsFalse(creditDecisionWorker.IsValidRequest(searchRequest9, out _));
            }

            private CreditSearchRequest CreateCreditSearchRequest(decimal appliedAmount, int term, decimal preExistingCredit)
            {
                return new CreditSearchRequest
                {
                    AppliedAmount = appliedAmount,
                    Term = term,
                    PreExistingCredit = preExistingCredit
                };
            }
        }
    }
}
