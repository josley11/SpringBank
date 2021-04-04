using System;
using SpringBank.Business;
using SpringBank.Common;
using SpringBank.Models;

namespace SpringBank
{
    /// <summary>
    /// Helper class to process credit eligibility request
    /// </summary>
    public class CreditDecisionWorker : ICreditDecisionWorker
    {
        private readonly ICreditDecisionRules _creditDecisionRules;

        public CreditDecisionWorker(ICreditDecisionRules creditDecisionRules)
        {
            _creditDecisionRules = creditDecisionRules ?? throw new ArgumentNullException(nameof(creditDecisionRules));
        }

        /// <inheritdoc />
        public bool IsValidRequest(CreditSearchRequest searchRequest, out string message)
        {
            message = null;
            if (searchRequest.AppliedAmount < 0 || !Validator.IsValidCurrencyAmount(searchRequest.AppliedAmount))
            {
                message = $"Invalid credit amount requested . Amount provided : {searchRequest.AppliedAmount}";
                return false;
            }
           
            if (searchRequest.Term <= 0)
            {
                message = $"Invalid term provided . Term provided : {searchRequest.Term}";
                return false;
            }

            if (searchRequest.PreExistingCredit < 0 || !Validator.IsValidCurrencyAmount(searchRequest.PreExistingCredit))
            {
                message = $"Invalid Pre existing credit amount provided . Amount provided : {searchRequest.PreExistingCredit}";
                return false;
            }

            return true;
        }
        
        /// <inheritdoc />
        public CreditDecision GetCreditDecision(CreditSearchRequest searchRequest)
        {
            var isEligibleForCredit = _creditDecisionRules.IsEligibleForCredit(searchRequest.AppliedAmount);

            return new CreditDecision
            {
                IsEligibleForCredit = isEligibleForCredit,
                InterestRate = isEligibleForCredit ? _creditDecisionRules.GetInterestRate(searchRequest) : null
            };
        }        
    }
}
