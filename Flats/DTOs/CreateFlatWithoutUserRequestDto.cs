using AparmentSystemAPI.Models;

namespace AparmentSystemAPI.Flats.DTOs
{
    public class CreateFlatWithoutUserRequestDto
    {
        public string BlockInfo { get; set; } = default!;
        //public bool isEmpty { get; set; } = default!;

        public string FlatType { get; set; } = default!;
        public string FloorNumber { get; set; } = default!;
        public int FlatNumber { get; set; }

   

    }
}
