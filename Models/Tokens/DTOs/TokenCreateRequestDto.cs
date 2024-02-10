namespace AparmentSystemAPI.Models.Tokens.DTOs
{
    public class TokenCreateRequestDto
    {
        public string PhoneNumber { get; set; } = default!;
        public string TcNumber { get; set; } = default!;
    }
}
