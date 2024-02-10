using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Repositories;
using AparmentSystemAPI.Repositories.Interfaces;

namespace AparmentSystemAPI.Payments
{
    public class PaymentRepository(AppDbContext context) : BaseRepository<Payment>(context), IPaymentRepository
    {
        public void AddUserToPayment(Payment payment, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
