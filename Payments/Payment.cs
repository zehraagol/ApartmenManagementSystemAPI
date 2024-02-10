using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Payments
{
    public class Payment
    {
        // odenmis ve odenmemis bilgisi ekleyerek fatura ve fiş logicini ekledim.
        public Guid Id { get; set; }
        public bool? isCreditCard { get; set; } 
        public int PaymentYear { get; set; }
        public DateTime? PaymentDate { get; set; } 
        public int PaymentMonth { get; set; }
        public string? PaymentType { get; set; } 
        public int PaymentAmount { get; set; }

        [ForeignKey("FlatId")] public Guid FlatId { get; set; }
        public Flat? Flat { get; set; }

        [ForeignKey("AppUserId")] public Guid? AppUserId { get; set; }

    }
}
