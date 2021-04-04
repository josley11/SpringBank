using System.Collections.Generic;

namespace SpringBank.DataAccess
{
    public interface ICreditRepository
    {
        /// <summary>
        /// Returns the conditions for credit eligibility
        /// </summary>
        public List<CreditCriterion> GetCreditCriteria();

        /// <summary>
        /// Returns the conditions for interest rate value
        /// </summary>
        public List<InterestRateCriterion> GetInterestRateCriteria();
    }
}
