namespace AparmentSystemAPI.Models.Identities.DTOs
{
    public class UserCreateRequestDto
    {

        public string? FullName { get; set; }
        public string? TCNumber { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        //public int FlatNumber { get; set; }

    }
}
