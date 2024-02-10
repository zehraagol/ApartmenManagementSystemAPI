using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using static AparmentSystemAPI.Models.UnitOfWorks.UnitOfWork;

namespace AparmentSystemAPI.Models.UnitOfWorks
{

    public class UnitOfWork(AppDbContext context, UserManager<AppUser> userManager, IFlatRepository flat, IPaymentRepository paymentRepository) : IUnitOfWork
    {
        // public AppDbContext Context { get; } = context;
        public IFlatRepository Flat { get; } = flat;

        public UserManager<AppUser> UserManager { get; } = userManager;

        public IPaymentRepository PaymentRepository { get; } = paymentRepository;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
