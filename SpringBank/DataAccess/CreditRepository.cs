using System.Collections.Generic;

namespace SpringBank.DataAccess
{
    public class CreditRepository : ICreditRepository
    {
        /// <summary>
        /// List of credit eligibilty conditions
        /// </summary>
        public static readonly List<CreditCriterion> CreditCriteria = new()
        {
            new CreditCriterion(decimal.MinValue, 2000, false),
            new CreditCriterion(2000, 69000, true),
            new CreditCriterion(69000, decimal.MaxValue, false)
        };

        /// <summary>
        /// List of interest rate value conditions
        /// </summary>
        public static readonly List<InterestRateCriterion> InterestRateCriteria = new()
        {
            new InterestRateCriterion(2000, 20000, 3),
            new InterestRateCriterion(20000, 40000, 4),
            new InterestRateCriterion(40000, 60000, 5),
            new InterestRateCriterion(60000, decimal.MaxValue, 6),
        };

        /// <inheritdoc />
        public List<CreditCriterion> GetCreditCriteria()
        {
            return CreditCriteria;
        }

        /// <inheritdoc />
        public List<InterestRateCriterion> GetInterestRateCriteria()
        {
            return InterestRateCriteria;
        }
    }
}
