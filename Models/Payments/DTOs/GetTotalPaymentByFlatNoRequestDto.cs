namespace AparmentSystemAPI.Models.Payments.DTOs
{
    public class GetTotalPaymentByFlatNoRequestDto
    {
        public int FlatNo { get; set; }
        public int PaymentYear { get; set; }
        public int? PaymentMonth { get; set; }
        public string? MonthlyOrYearly { get; set; }
        public string? PaymentType { get; set; }
    }
}
