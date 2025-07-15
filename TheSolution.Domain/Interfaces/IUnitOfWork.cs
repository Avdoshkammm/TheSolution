using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolution.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IAccountRepository Accounts { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync();
    }
}
