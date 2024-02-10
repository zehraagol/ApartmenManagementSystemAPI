namespace AparmentSystemAPI.Models.Payments.DTOs
{
    public class PayPaymentRequestDto
    {
        public bool? isCreditCard { get; set; }
        public int PaymentYear { get; set; }
        public int PaymentMonth { get; set; }
        public string? PaymentType { get; set; }

        //flatno
        public int FlatNo { get; set; }
    }
}
