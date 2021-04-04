using Newtonsoft.Json;

namespace SpringBank.Models
{
    /// <summary>
    /// Representatin of the credit search request for credit eligibility
    /// </summary>
    public class CreditSearchRequest
    {                
        [JsonProperty("AppliedAmount", Required = Required.Always)]
        public decimal AppliedAmount { get; set; }

        [JsonProperty("Term", Required = Required.Always)]
        public int Term { get; set; }

        [JsonProperty("PreExistingCredit", Required = Required.Always)]
        public decimal PreExistingCredit { get; set; }        
    }
}
