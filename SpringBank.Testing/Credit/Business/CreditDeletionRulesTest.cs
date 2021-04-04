using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpringBank.Business;
using SpringBank.DataAccess;
using SpringBank.Models;

namespace SpringBank.Testing.Credit.Business
{
    internal sealed class CreditDeletionRulesTest
    {
        [TestClass]
        public class IsEligibleForCredit
        {
            [TestMethod]
            public void GivenAppliedAmountLessThanMinimumAllowedAmount_ThenFalseIsReturned()
            {
                //Arrange
                var appliedAmount = 1999.99M;
                var creditRepository = A.Fake<ICreditRepository>();
                var creditDecisionRules = CreateCreditDecisionRules(creditRepository);                
                A.CallTo(() => creditRepository.GetCreditCriteria()).Returns(CreditRepository.CreditCriteria);

                //Act
                var actual = creditDecisionRules.IsEligibleForCredit(appliedAmount);

                //Assert
                Assert.IsFalse(actual);
            }

            [TestMethod]
            public void GivenAppliedAmountMoreThanMaximumAllowedAmount_ThenFalseIsReturned()
            {
                //Arrange                
                var appliedAmount = 69000.00M;
                var creditRepository = A.Fake<ICreditRepository>();
                var creditDecisionRules = CreateCreditDecisionRules(creditRepository);                
                A.CallTo(() => creditRepository.GetCreditCriteria()).Returns(CreditRepository.CreditCriteria);

                //Act
                var actual = creditDecisionRules.IsEligibleForCredit(appliedAmount);

                //Assert
                Assert.IsFalse(actual);
            }

            [TestMethod]
            public void GivenEligibleAppliedAmount_ThenTrueIsReturned()
            {
                //Arrange                
                var appliedAmount = 50000.00M;
                var creditRepository = A.Fake<ICreditRepository>();
                var creditDecisionRules = CreateCreditDecisionRules(creditRepository);
                A.CallTo(() => creditRepository.GetCreditCriteria()).Returns(CreditRepository.CreditCriteria);

                //Act
                var actual = creditDecisionRules.IsEligibleForCredit(appliedAmount);

                //Assert
                Assert.IsTrue(actual);
            }
            
        }

        [TestClass]
        public class GetInterestRate
        {
            [TestMethod]
            public void GivenAmount_ThenExpectedInterestRateReturned()
            {
                //Arrange                
                var creditRepository = A.Fake<ICreditRepository>();
                var creditDecisionRules = CreateCreditDecisionRules(creditRepository);
                A.CallTo(() => creditRepository.GetInterestRateCriteria()).Returns(CreditRepository.InterestRateCriteria);
                                
                //Credit search request where total debt < 20000 - should return interest rate 3
                var searchRequest1 = CreateCreditSearchRequest(2000M, 3, 0M);
                var searchRequest2 = CreateCreditSearchRequest(2000M, 3, 1000M);
                var searchRequest3 = CreateCreditSearchRequest(20000M, 3, -0.01M);

                //Credit search request where total debt >= 20000 and < 40000  - should return interest rate 4
                var searchRequest4 = CreateCreditSearchRequest(20000M, 3, 0M);
                var searchRequest5 = CreateCreditSearchRequest(20000M, 3, 1000M);
                var searchRequest6 = CreateCreditSearchRequest(40000M, 3, -0.01M);

                //Credit search request where total debt >= 40000 and < 60000  - should return interest rate 5
                var searchRequest7 = CreateCreditSearchRequest(40000M, 3, 0M);
                var searchRequest8 = CreateCreditSearchRequest(40000M, 3, 1000M);
                var searchRequest9 = CreateCreditSearchRequest(60000M, 3, -0.01M);

                //Credit search request where total debt >= 60000  - should return interest rate 6
                var searchRequest10 = CreateCreditSearchRequest(60000M, 3, 0M);
                var searchRequest11 = CreateCreditSearchRequest(60000M, 3, 1000M);

                //Credit search request where not applicable - should return interest rate null
                var searchRequest = CreateCreditSearchRequest(1000M, 3, 0M);

                //Act & Assert                            
                Assert.AreEqual(3, creditDecisionRules.GetInterestRate(searchRequest1));
                Assert.AreEqual(3, creditDecisionRules.GetInterestRate(searchRequest2));
                Assert.AreEqual(3, creditDecisionRules.GetInterestRate(searchRequest3));

                Assert.AreEqual(4, creditDecisionRules.GetInterestRate(searchRequest4));
                Assert.AreEqual(4, creditDecisionRules.GetInterestRate(searchRequest5));
                Assert.AreEqual(4, creditDecisionRules.GetInterestRate(searchRequest6));

                Assert.AreEqual(5, creditDecisionRules.GetInterestRate(searchRequest7));
                Assert.AreEqual(5, creditDecisionRules.GetInterestRate(searchRequest8));
                Assert.AreEqual(5, creditDecisionRules.GetInterestRate(searchRequest9));

                Assert.AreEqual(6, creditDecisionRules.GetInterestRate(searchRequest10));
                Assert.AreEqual(6, creditDecisionRules.GetInterestRate(searchRequest11));

                Assert.AreEqual(null, creditDecisionRules.GetInterestRate(searchRequest));
            }
        }

        private static CreditDecisionRules CreateCreditDecisionRules(ICreditRepository creditRepository = null)
        {
            return new CreditDecisionRules(creditRepository ?? A.Fake<ICreditRepository>());
        }

        private static CreditSearchRequest CreateCreditSearchRequest(decimal appliedAmount, int term, decimal preExistingCredit)
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
