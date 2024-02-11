using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models.Flats
{
    public class Flat
    {
        // bir flatin birden çok kullanıcısı olamaz ama bir kullanıcının birden çok dairesi olabilir mantığı ile ilişki kurdum.
        public Guid Id { get; set; }
        public string BlockInfo { get; set; } = default!;
        public bool isEmpty { get; set; } = default!;
        public string FlatType { get; set; } = default!;
        public string FloorNumber { get; set; } = default!;
        public int FlatNumber { get; set; } = default!;
        [ForeignKey("UserId")] public Guid? UserId { get; set; } // bu zorunlu olmasın diye nullable yaptık
        public AppUser? User { get; set; }
        public List<Payment> Payments { get; set; } = default!; //navigation property

    }
}
