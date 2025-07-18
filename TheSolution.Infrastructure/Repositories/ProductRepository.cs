using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;
using TheSolution.Infrastructure.Data;

namespace TheSolution.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TheSolutionDBContext db;
        private readonly ILogger<ProductRepository> logger;
        public ProductRepository(TheSolutionDBContext _db, ILogger<ProductRepository> _logger)
        {
            db = _db;
            logger = _logger;
        }
        public async Task<IEnumerable<Product>> GetValues()
        {
            IEnumerable<Product> products = await db.Products.AsNoTracking().ToListAsync();
            return products;
        }
        public async Task<Product> GetValue(int id)
        {
            Product? product = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            return product;
        }
        public async Task CreateValue(Product product)
        {
            await db.Products.AddAsync(product);
        }
        public async Task UpdateValue(Product product)
        {
            db.Products.Update(product);
        }
        public async Task DeleteValue(int id)
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.ID == id);
            if(product != null)
            {
                db.Products.Remove(product);
            }
        }
    }
}
