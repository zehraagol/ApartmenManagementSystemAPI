
namespace AparmentSystemAPI.Models.Payments.DTOs
{
    public class AddPaymentRequestDto
    {

        public int PaymentYear { get; set; }
        public int PaymentMonth { get; set; }
        public string? PaymentType { get; set; }
        public int PaymentAmount { get; set; }
        public int FlatNo { get; set; }

    }
}
