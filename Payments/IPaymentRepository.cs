using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Repositories.Interfaces;

namespace AparmentSystemAPI.Payments
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        void AddUserToPayment(Payment payment, string userId);

    }
}
