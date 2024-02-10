using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Payments;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AparmentSystemAPI.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        

        // Create a TCNumber property with unique constraint
    
        public string TCNumber { get; set; } = default!;

       // [ForeignKey("Flat")] public Guid FlatId { get; set; } = default!; //foreign key
        public List<Flat> Flat { get; set; }= default!; //navigation property

        public List<Payment> Payments { get; set; } = default!; //navigation property
      
        
    }
}
