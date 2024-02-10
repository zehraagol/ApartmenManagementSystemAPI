namespace AparmentSystemAPI.Payments.DTOs
{
    public class RegularlyPayingUsersRequestDto
    {
        // paymetn type and payment period
        public string? PaymentType { get; set; }
        public int PaymentPeriod { get; set; }

    }
}
