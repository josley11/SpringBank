using Newtonsoft.Json;

namespace SpringBank.Models
{
    /// <summary>
    /// Represenation of the credit decision that is returned as a result of a credit eligibility search request
    /// </summary>
    public class CreditDecision
    {
        [JsonProperty("IsEligibleForCredit", Required = Required.Always)]
        public bool IsEligibleForCredit { get; set; }

        [JsonProperty("InterestRate", Required = Required.AllowNull)]
        public decimal? InterestRate { get; set; }
    }
}
