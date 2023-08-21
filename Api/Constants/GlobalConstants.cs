namespace Api
{
    // Define global constants
    // why: to avoid using magic numbers
    public static class GlobalConstants
    {
        public const decimal PROCENT_OF_REDUCTION_IF_OVER_80K = 0.02m; // employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
        public const int NUMBER_OF_PAYCHECKS_PER_YEAR = 26; // 26 paychecks per year
        public const decimal BASE_COST_PER_YEAR = 1000 * 12; // employees have a base cost of $1,000 per month (for benefits)
        public const decimal ONE_DEPENDENT_COST_PER_YEAR = 600 * 12; // each dependent represents an additional $600 cost per month (for benefits)
        public const decimal ONE_DEPENDENT_OVER_FIFTY_ADDITIONAL_COST_PER_YEAR = 200 * 12; // dependents that are over 50 years old will incur an additional $200 per month
    }
}
