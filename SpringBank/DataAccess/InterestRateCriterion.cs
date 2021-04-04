namespace SpringBank.DataAccess
{
    /// <summary>
    /// Representation of an onterest rate condition
    /// </summary>
    public class InterestRateCriterion
    {
        public InterestRateCriterion(decimal lowerLimit, decimal upperLimit, decimal interestRate)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            InterestRate = interestRate;
        }

        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal InterestRate { get; set; }
    }
}
