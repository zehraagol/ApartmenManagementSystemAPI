using AparmentSystemAPI.Models;
using AparmentSystemAPI.Payments;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Apartment
{
    public class Flat
    {
        public Guid Id { get; set; }
        public string BlockInfo { get; set; } = default!;       
        public bool isEmpty { get; set; } = default!;

        public string FlatType { get; set; } = default!;
        public string FloorNumber { get; set; } = default!;
        public int FlatNumber { get; set; } = default!;

        //public Guid UserId { get; set; }
        [ForeignKey("UserId") ] public Guid? UserId { get; set; } // bu zorunlu olmasın diye nullable yaptık
        public AppUser? User { get; set; } 

        public List<Payment> Payments { get; set; } = default!; //navigation property

       





    }
}
