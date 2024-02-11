namespace AparmentSystemAPI.Models.Flats.DTOs
{
    public class AddUserToFlatRequestDto
    {
        public string UserTCNumber { get; set; } = default!;
        public int FlatNumber { get; set; } = default!;
    }
}
