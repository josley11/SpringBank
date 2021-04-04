using System;
using SpringBank.DataAccess;
using SpringBank.Models;

namespace SpringBank.Business
{
    /// <summary>
    /// The class implementation that performs the client's crdeit decision rules.
    /// </summary>
    public class CreditDecisionRules : ICreditDecisionRules
    {
        private readonly ICreditRepository _creditRepository;

        public CreditDecisionRules(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository ?? throw new ArgumentNullException(nameof(creditRepository));
        }

        /// <inheritdoc />
        public bool IsEligibleForCredit(decimal appliedAmount)
        {
            var creditCriteria = _creditRepository.GetCreditCriteria();
            
            foreach (var criterion in creditCriteria)
            {
                if (IsAmountInCriterion(criterion.LowerLimit, criterion.UpperLimit, appliedAmount))
                    return criterion.IsEligible;
            }

            return false;
        }        

        /// <inheritdoc />
        public decimal? GetInterestRate(CreditSearchRequest searchRequest)
        {
            var totalFutureDebt = searchRequest.AppliedAmount + searchRequest.PreExistingCredit;
            var interestRateCriteria = _creditRepository.GetInterestRateCriteria();
                        
            foreach(var criterion in interestRateCriteria)
            {
                if(IsAmountInCriterion(criterion.LowerLimit, criterion.UpperLimit, totalFutureDebt))
                    return criterion.InterestRate;
            }
                        
            return default;
        }        

        private bool IsAmountInCriterion(decimal lowerLimit, decimal upperLimit, decimal value)
        {
            return value >= lowerLimit && value < upperLimit;
        }
    }
}
