using System.ComponentModel.DataAnnotations;

namespace AparmentSystemAPI.Models.Identities.DTOs
{
    public class UserUpdateRequestDto
    {
        [Required]
        public string? TCNumber { get; set; } = default!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
