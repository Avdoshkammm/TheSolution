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
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"\nGetProducts service error");
                return Enumerable.Empty<ProductDTO>();
            }
        }

        public async Task<ProductDTO> GetValue(int id)
        {
            if(id == null)
            {
                logger.LogError("Null in service(GetProductByID)");
                return null;
            }
            Product product = await uow.Products.GetValue(id);
            try
            {
                return mapper.Map<ProductDTO>(product);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"\nGetProduct service error");
                return null;
            }
        }

        public async Task<ProductDTO> CreateValue(ProductDTO productDTO)
        {
            Product createProduct = mapper.Map<Product>(productDTO);
            await uow.Products.CreateValue(createProduct);
            await uow.SaveChangesAsync();
            try
            {
                return mapper.Map<ProductDTO>(createProduct);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"\nСreate product service error");
                return null;
            }
        }

        public async Task<ProductDTO> UpdateValue(ProductDTO productDTO)
        {
            Product updateProduct = mapper.Map<Product>(productDTO);
            await uow.Products.UpdateValue(updateProduct);
            await uow.SaveChangesAsync();
            try
            {
                return mapper.Map<ProductDTO>(updateProduct);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"\nUpdate product service error");
                return null;
            }
        }

        public async Task DeleteValue(int id)
        {
            try
            {
                await uow.Products.DeleteValue(id);
                await uow.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"\nDelete product service error");
            }
        }
    }
}