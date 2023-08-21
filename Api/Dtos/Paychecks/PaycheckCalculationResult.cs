namespace Api.Dtos.Paychecks
{
    // I used tuple first, but for better readability i changed it to DTO
    public class PaycheckCalculationResult
    {
        public decimal Pay { get; set; }
        public int Remainder { get; set; }
    }
}
