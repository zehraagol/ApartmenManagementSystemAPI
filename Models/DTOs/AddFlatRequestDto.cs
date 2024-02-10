using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models.DTOs
{
    public class AddFlatRequestDto
    {
        
        public string BlockInfo { get; set; } = default!;
       // public bool isEmpty { get; set; } = default!;

        public string FlatType { get; set; } = default!;
        public string FloorNumber { get; set; } = default!;
        public int FlatNumber { get; set; } = default!;

       // public string UserTCNumber { get; set; }
        


    }
}
