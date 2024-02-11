using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.MainBuildings;
using AparmentSystemAPI.Models.Payments;
using Microsoft.AspNetCore.Identity;

namespace AparmentSystemAPI.Models.Identities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string TCNumber { get; set; } = default!;

        public List<Flat> Flat { get; set; } = default!; //navigation property

        public List<Payment> Payments { get; set; } = default!; //navigation property

        public MainBuilding MainBuildings { get; set; } = default!; //navigation property

    }
}
