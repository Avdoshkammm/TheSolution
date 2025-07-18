using AutoMapper;
using Microsoft.Extensions.Logging;
using TheSolution.Application.DTO;
using TheSolution.Application.Interfaces;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;

namespace TheSolution.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogger<ProductService> logger;
        public ProductService(IUnitOfWork _uow, IMapper _mapper, ILogger<ProductService> _logger)
        {
            uow = _uow;
            mapper = _mapper;
            logger = _logger;
        }
        public async Task<IEnumerable<ProductDTO>> GetValues()
        {
            IEnumerable<Product> products = await uow.Products.GetValues();
            try
            {
                logger.LogInformation("GetValues(Product) attemp");
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nGetProducts service error");
                throw;
            }
        }

        public async Task<ProductDTO> GetValue(int id)
        {
            if (id == null)
            {
                logger.LogError("Null in service(GetProductByID)");
                return null;
            }
            Product product = await uow.Products.GetValue(id);
            try
            {
                logger.LogInformation("GetValue(Product) attemp(repo)");
                return mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nGetProduct service error");
                throw;
            }
        }

        public async Task<ProductDTO> CreateValue(ProductDTO productDTO)
        {
            Product createProduct = mapper.Map<Product>(productDTO);
            await uow.BeginTransactionAsync();
            try
            {
                await uow.Products.CreateValue(createProduct);
                await uow.SaveChangesAsync();
                await uow.CommitTransactionAsync();
                return mapper.Map<ProductDTO>(createProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nCreate new product service error");
                await uow.RollbackTransactionAsync();
                throw;
            }


        }

        public async Task<ProductDTO> UpdateValue(ProductDTO productDTO)
        {
            Product updateProduct = mapper.Map<Product>(productDTO);
            await uow.BeginTransactionAsync();
            try
            {
                await uow.Products.UpdateValue(updateProduct);
                await uow.SaveChangesAsync();
                await uow.CommitTransactionAsync();
                return mapper.Map<ProductDTO>(updateProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nUpdate product service error");
                await uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteValue(int id)
        {
            await uow.BeginTransactionAsync();
            try
            {
                await uow.Products.DeleteValue(id);
                await uow.SaveChangesAsync();
                await uow.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nDelete product service error");
                await uow.RollbackTransactionAsync();
                throw;
            }
        }
    }
}