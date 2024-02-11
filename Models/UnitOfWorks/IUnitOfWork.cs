using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace AparmentSystemAPI.Models.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<AppUser> UserManager { get; }
        IFlatRepository Flat { get; }
        IPaymentRepository PaymentRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
