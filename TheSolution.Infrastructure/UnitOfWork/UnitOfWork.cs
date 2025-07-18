using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;
using TheSolution.Infrastructure.Data;
using TheSolution.Infrastructure.Repositories;

namespace TheSolution.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TheSolutionDBContext db;
        private readonly ILoggerFactory logger;
        private readonly UserManager<User> um;
        private readonly SignInManager<User> sim;

        private IProductRepository? pRepository;
        private IAccountRepository aRepository;
        private IOrderRepository oRepository;

        private IDbContextTransaction transaction;
        public UnitOfWork(TheSolutionDBContext _db, ILoggerFactory _logger, UserManager<User> _um)
        {
            db = _db;
            logger = _logger;
            um = _um;
        }

        public async Task BeginTransactionAsync()
        {
            if(transaction != null)
            {
                return;
            }
            transaction = await db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if(transaction != null)
            {
                await transaction.CommitAsync();
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if(transaction != null)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if(pRepository == null)
                {
                    var repoLogger = logger.CreateLogger<ProductRepository>();
                    pRepository = new ProductRepository(db, repoLogger);
                }
                return pRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if(oRepository == null)
                {
                    var repoLogger = logger.CreateLogger<OrderRepository>();
                    oRepository = new OrderRepository(db, repoLogger);
                }
                return oRepository;
            }
        }

        public IAccountRepository Accounts
        {
            get
            {
                if(aRepository == null)
                {
                    var repoLogger = logger.CreateLogger<AccountRepository>();
                    aRepository = new AccountRepository(um, sim, repoLogger);
                }
                return aRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
