using AparmentSystemAPI.Flats;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Payments;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace AparmentSystemAPI
{
    public interface IUnitOfWork : IDisposable
    {
       // AppDbContext Context { get; }
        UserManager<AppUser> UserManager { get; }
        IFlatRepository Flat { get; }
        IPaymentRepository PaymentRepository { get; }


    
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
