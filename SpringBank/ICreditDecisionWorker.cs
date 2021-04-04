using SpringBank.Models;

namespace SpringBank
{
    public interface ICreditDecisionWorker
    { 
        /// <summary>
        /// Checks if the request for credit eligibility has valid parameters
        /// </summary>
        public bool IsValidRequest(CreditSearchRequest searchRequest, out string message);

        /// <summary>
        /// Gets the credit eligibility search decision result
        /// </summary>
        public CreditDecision GetCreditDecision(CreditSearchRequest searchRequest);
    }
}
