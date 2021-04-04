using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SpringBank.DataAccess;
using System.Collections.Generic;

namespace SpringBank.Testing.Credit.DataAccess
{
    internal sealed class CreditRepositoryTests
    {
        [TestClass]
        public class GetCreditCriteria
        {
            [TestMethod]
            public void WhenInvoked_ThenCreditCriteriaIsReturned()
            {
                //Arrange
                var creditRepository = new CreditRepository();

                //Act
                var actual = creditRepository.GetCreditCriteria();

                //Assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(actual);
                NUnit.Framework.Assert.That(actual, Is.TypeOf<List<CreditCriterion>>());
            }
        }

        [TestClass]
        public class GetInterestRateCriteria
        {
            [TestMethod]
            public void WhenInvoked_ThenInterestRateCriteriaIsReturned()
            {
                //Arrange
                var creditRepository = new CreditRepository();

                //Act
                var actual = creditRepository.GetInterestRateCriteria();

                //Assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(actual);
                NUnit.Framework.Assert.That(actual, Is.TypeOf<List<InterestRateCriterion>>());
            }
        }
    }
}
