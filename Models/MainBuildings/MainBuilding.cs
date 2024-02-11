using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models.MainBuildings
{

    [Table("MainBuilding")]
     public class MainBuilding
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        //builfingage
        public string? BuildingAge { get; set; }
     
        [ForeignKey("UserId")] public Guid? UserId { get; set; } // bu zorunlu olmasın diye nullable yaptık
        public AppUser? User { get; set; }
        public List<Payment> Payments { get; set; } = default!; //navigation property

    }
}
