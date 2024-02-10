using AparmentSystemAPI.Models.Payments.Interfaces;

namespace AparmentSystemAPI.Models.Payments
{
    public class PaymentRepository(AppDbContext context) : BaseRepository<Payment>(context), IPaymentRepository
    {
        public void AddUserToPayment(Payment payment, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
