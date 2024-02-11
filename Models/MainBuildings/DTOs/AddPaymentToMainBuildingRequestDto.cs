using AparmentSystemAPI.Models.Flats;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models.MainBuildings.DTOs
{
    public class AddPaymentToMainBuildingRequestDto
    {
        public int PaymentYear { get; set; }
        public int PaymentMonth { get; set; }
        public string? PaymentType { get; set; }
        public int PaymentAmount { get; set; }

    }
}
