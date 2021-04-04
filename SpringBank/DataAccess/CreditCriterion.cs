namespace SpringBank.DataAccess
{
    /// <summary>
    /// Representation of a credit condition
    /// </summary>
    public class CreditCriterion
    {    
        public CreditCriterion(decimal lowerLimit, decimal upperLimit, bool isEligible)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            IsEligible = isEligible;
        }

        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public bool IsEligible { get; set; }
    }
}
