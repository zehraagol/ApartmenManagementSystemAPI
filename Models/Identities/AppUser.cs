using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Payments;
using Microsoft.AspNetCore.Identity;

namespace AparmentSystemAPI.Models.Identities
{
    public class AppUser : IdentityUser<Guid>
    {


        // Create a TCNumber property with unique 
        public string TCNumber { get; set; } = default!;

        // [ForeignKey("Flat")] public Guid FlatId { get; set; } = default!; //foreign key
        public List<Flat> Flat { get; set; } = default!; //navigation property

        public List<Payment> Payments { get; set; } = default!; //navigation property

    }
}
