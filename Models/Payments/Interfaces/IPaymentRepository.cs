namespace AparmentSystemAPI.Models.Payments.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        void AddUserToPayment(Payment payment, string userId);

    }
}
