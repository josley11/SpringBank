using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpringBank.Controllers;
using SpringBank.Models;

namespace SpringBank.Testing.Credit.Controller
{
    internal sealed class CreditControllerTests
    {
        [TestClass]
        public class CreditSearch
        {
            [TestMethod]
            public void GivenInValidCreditSearchRequest_WhenRequestIsNotProcessed()
            {
                // Arrange              
                var creditSearchRequest = CreateCreditSearchRequest();
                var creditDecisionWorker = A.Fake<ICreditDecisionWorker>(); 
                var controller = CreateController(creditDecisionWorker);

                string msg = string.Empty;
                A.CallTo(() => creditDecisionWorker.IsValidRequest(A<CreditSearchRequest>._, out msg)).Returns(false);
                
                // Act
                var actual = controller.CreditSearch(creditSearchRequest);

                // Assert
                A.CallTo(() => creditDecisionWorker.IsValidRequest(creditSearchRequest, out msg))
                    .MustHaveHappenedOnceExactly();
                A.CallTo(() => creditDecisionWorker.GetCreditDecision(creditSearchRequest))
                    .MustNotHaveHappened();                
                Assert.AreEqual(StatusCodes.Status400BadRequest, ((ObjectResult)actual).StatusCode );
            }

            [TestMethod]
            public void GivenValidCreditSearchRequest_WhenRequestIsProcessedAndOKStatuswithCreditDecisionIsReturned()
            {
                // Arrange              

                var creditSearchRequest = CreateCreditSearchRequest();
                var creditDecisionWorker = A.Fake<ICreditDecisionWorker>();
                var controller = CreateController(creditDecisionWorker);

                string msg = string.Empty;
                A.CallTo(() => creditDecisionWorker.IsValidRequest(A<CreditSearchRequest>._, out msg)).Returns(true);
                A.CallTo(() => creditDecisionWorker.GetCreditDecision(A<CreditSearchRequest>._)).Returns(new CreditDecision());

                // Act
                var actual = controller.CreditSearch(creditSearchRequest);

                // Assert
                A.CallTo(() => creditDecisionWorker.IsValidRequest(creditSearchRequest, out msg))
                    .MustHaveHappenedOnceExactly();
                A.CallTo(() => creditDecisionWorker.GetCreditDecision(creditSearchRequest))
                    .MustHaveHappenedOnceExactly();
                Assert.AreEqual(StatusCodes.Status200OK, ((ObjectResult)actual).StatusCode);
            }            
        }

        private static CreditSearchRequest CreateCreditSearchRequest(decimal appliedAmount = 0M, int term = 1, decimal preExistingCredit = 0M)
        {
            return new CreditSearchRequest
            {
                AppliedAmount = appliedAmount,
                Term = term,
                PreExistingCredit = preExistingCredit
            };
        }

        private static CreditController CreateController(ICreditDecisionWorker creditDecisionWorker)
        {
            return new CreditController(creditDecisionWorker ?? A.Fake<ICreditDecisionWorker>());
        }
    }
}
