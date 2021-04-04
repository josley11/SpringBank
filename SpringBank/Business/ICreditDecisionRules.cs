using SpringBank.Models;

namespace SpringBank.Business
{
    /// <summary>
    /// Performs the client's credit eligibility decision rules.
    /// </summary>
    public interface ICreditDecisionRules
    {
        /// <summary>
        /// Determines whether a person is eligible for credit.
        /// </summary>
        bool IsEligibleForCredit(decimal appliedAmount);

        /// <summary>
        /// Determines interest rate.
        /// </summary>
        decimal? GetInterestRate(CreditSearchRequest searchRequest);
    }
}
