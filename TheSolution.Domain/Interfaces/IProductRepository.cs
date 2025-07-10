using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Domain.Entities;

namespace TheSolution.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetValues();
        Task<Product> GetValue(int id);
        Task CreateValue(Product product);
        Task UpdateValue(Product product);
        Task DeleteValue(int id);
    }
}
