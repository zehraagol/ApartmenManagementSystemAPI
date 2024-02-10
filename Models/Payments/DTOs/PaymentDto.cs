using AparmentSystemAPI.Models.Flats;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models.Payments.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public bool isCreditCard { get; set; }
        public int PaymentYear { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentMonth { get; set; }
        public string PaymentType { get; set; } = default!;
        public int PaymentAmount { get; set; }

        [ForeignKey("FlatId")] public Guid FlatId { get; set; }
        public Flat? Flat { get; set; }

        [ForeignKey("AppUserId")] public Guid AppUserId { get; set; }
    }
}
