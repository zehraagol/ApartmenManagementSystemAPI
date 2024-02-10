namespace AparmentSystemAPI.Tokens.DTOs
{
    public class AdminTokenCreateRequestDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
