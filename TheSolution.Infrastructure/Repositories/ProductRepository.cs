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
            try
            {
                return products;
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}\nОшибка вывода продуктов");
                throw;
            }
        }
        public async Task<Product> GetValue(int id)
        {
            Product? product = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            try
            {
                return product;
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}\nОшибка поиска продукта");
                throw;
            }
        }
        public async Task CreateValue(Product product)
        {
            try
            {
                await db.Products.AddAsync(product);
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}\nОшибка создания продукта");
                throw;
            }
        }
        public async Task UpdateValue(Product product)
        {
            Product updatedProduct = new Product()
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                Quantity = product.Quantity,
            };
            try
            {
                db.Products.Update(updatedProduct);
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}\nОшибка обновления продукта");
                throw;
            }
        }
        public async Task DeleteValue(int id)
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.ID == id);
            if(product != null)
            {
                try
                {
                    db.Products.Remove(product);
                }
                catch(Exception ex)
                {
                    logger.LogError($"{ex.Message}\nОшибка удаления продукта");
                    throw;
                }
            }
        }
    }
}
