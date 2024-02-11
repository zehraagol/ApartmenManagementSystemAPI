using AparmentSystemAPI.Models.Identities;

namespace AparmentSystemAPI.Models.Flats.DTOs
{
    public class FlatDto
    {
        public Guid Id { get; set; }
        public string BlockInfo { get; set; } = default!;
        public string State { get; set; } = default!;
        public string FlatType { get; set; } = default!;
        public string FloorNumber { get; set; } = default!;
        public int FlatNumber { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

    }
}
