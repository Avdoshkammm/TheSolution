using Microsoft.AspNetCore.Identity;
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
        public UnitOfWork(TheSolutionDBContext _db, ILoggerFactory _logger, UserManager<User> _um)
        {
            db = _db;
            logger = _logger;
            um = _um;
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
