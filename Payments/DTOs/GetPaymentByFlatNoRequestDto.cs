namespace AparmentSystemAPI.Payments.DTOs
{
    public class GetPaymentByFlatNoRequestDto
    {
        public int FlatNo { get; set; }
        public bool isPaid { get; set; } // odenmis veya odenmemis fatura özelligi. 

        public string? PaymentType { get; set; }
    }
}
