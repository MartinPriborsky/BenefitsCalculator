namespace Api.Models
{
    public class Paycheck
    {
        public int PaycheckId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? Pay { get; set; }
    }
}
