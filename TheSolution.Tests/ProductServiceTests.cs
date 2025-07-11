using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TheSolution.Application.DTO;
using TheSolution.Application.Services;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;
using TheSolution.Infrastructure.Repositories;

namespace TheSolution.Tests
{
    public class ProductServiceTests
    {
        ////GetProducts
        //[Fact]
        //public async Task SeeAllProducts_ShouldReturnProducts()
        //{
        //    //var dbContext = TestDBContextFactory.Create();
        //    //var productLoggerMock = new Mock<ILogger<ProductRepository>>();
        //    //var productRepository = new ProductRepository(dbContext, productLoggerMock.Object);

        //    //var product1 = new Product { ID = 1, Name = "Test", Cost = 123, Description = "About_Product", Quantity = 123 };
        //    //var product2 = new Product { ID = 2, Name = "Test2", Cost = 234, Description = "About_Product_2", Quantity = 234 };
        //    //dbContext.Products.AddRange(product1, product2);
        //    //await dbContext.SaveChangesAsync();

        //    //var products = await productRepository.GetValues();
        //    //Assert.NotNull(products);
        //    //Assert.Contains(products, p => p.Name == "Test");
        //    //Assert.Contains(products, p => p.Name == "Test2");
        //}

        ////GetProductsDTO
        //[Fact]
        //public async Task GetValues_ReturnsMappedProductDTO()
        //{
        //    var mockUow = new Mock<IUnitOfWork>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockLogger = new Mock<ILogger<ProductService>>();

        //    var products = new List<Product>
        //    {
        //        new Product { ID = 1, Name = "Test", Cost = 123, Description = "About_Product", Quantity = 123},
        //        new Product { ID = 2, Name = "Test2", Cost = 456, Description = "About_Product_2", Quantity = 456}
        //    };

        //    var productsDTOs = new List<ProductDTO>
        //    {
        //        new ProductDTO { ID = 1, Name = "Test", Cost = 123, Description = "About_Product", Quantity = 123},
        //        new ProductDTO { ID = 2, Name = "Test2", Cost = 456, Description = "About_Product_2", Quantity = 456}
        //    };

        //    mockUow.Setup(u => u.Products.GetValues()).ReturnsAsync(products);

        //    mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(products)).Returns(productsDTOs);

        //    var service = new ProductService(mockUow.Object, mockMapper.Object, mockLogger.Object);

        //    var result = await service.GetValues();

        //    Assert.NotNull(result);
        //    Assert.Equal(2, result.Count());
        //    Assert.Equal(1, result.First().ID);
        //    Assert.Equal("Test", result.First().Name);
        //    Assert.Equal(123, result.First().Cost);
        //    Assert.Equal("About_Product", result.First().Description);
        //    Assert.Equal(123, result.First().Quantity);
        //}


        ////GetProduct
        //[Fact]
        //public async Task SeeProductByID_ShouldReturnProduct()
        //{
        //    //var dbContext = TestDBContextFactory.Create();
        //    //var productLoggerMock = new Mock<ILogger<ProductRepository>>();
        //    //var productRepository = new ProductRepository(dbContext, productLoggerMock.Object);

        //    //var product1 = new Product { ID = 1, Name = "Test", Cost = 123, Description = "Test", Quantity = 123 };
        //    //dbContext.Products.Add(product1);
        //    //await dbContext.SaveChangesAsync();

        //    //await productRepository.GetValue(1);
        //    //var product = dbContext.Products.FirstOrDefaultAsync(x => x.ID == 1);
        //    //Assert.NotNull(product);
        //}

        ////Get ProductDTO
        //[Fact]
        //public async Task GetValueByID_ReturnMappedProductDTO()
        //{
        //    var mockUow = new Mock<IUnitOfWork>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockLogger = new Mock<ILogger<ProductService>>();

        //    var product = new Product
        //    {
        //        ID = 1,
        //        Name = "Test",
        //        Cost = 123,
        //        Description = "About_Product",
        //        Quantity = 234
        //    };

        //    var productDTO = new ProductDTO
        //    {
        //        ID = 1,
        //        Name = "Test",
        //        Cost = 123,
        //        Description = "About_Product",
        //        Quantity = 234
        //    };

        //    int id = 1;
        //    mockUow.Setup(u => u.Products.GetValue(id)).ReturnsAsync(product);
        //    mockMapper.Setup(m => m.Map<ProductDTO>(product)).Returns(productDTO);
        //    var service = new ProductService(mockUow.Object, mockMapper.Object, mockLogger.Object);
        //    var result = await service.GetValue(id);

        //    Assert.NotNull(result);
        //    Assert.Equal(1, result.ID);
        //    Assert.Equal("Test", result.Name);
        //    Assert.Equal(123, result.Cost);
        //    Assert.Equal("About_Product", result.Description);
        //    Assert.Equal(234, result.Quantity);
        //}

        ////CreateProduct
        //[Fact]
        //public async Task CreateProductAsync_ShouldCreateNewProduct()
        //{
        //    //var mockUow = new Mock<IUnitOfWork>();
        //    //var dbContext = TestDBContextFactory.Create();
        //    //var productLoggerMock = new Mock<ILogger<ProductRepository>>();
        //    //var productRepo = new ProductRepository(dbContext, productLoggerMock.Object);
        //    var mockUow = new Mock<IUnitOfWork>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockLogger = new Mock<ILogger<ProductService>>();

        //    var newProduct = new Product
        //    {
        //        ID = 1,
        //        Name = "Test",
        //        Cost = 123,
        //        Description = "Test",
        //        Quantity = 123
        //    };

        //    //await mockUow.Setup(u => u.Products.CreateValue(newProduct)).Returns(newProduct);

        //    Assert.NotNull(newProduct);
        //    Assert.Equal(1, newProduct.ID);
        //    Assert.Equal("Test", newProduct.Name);
        //}

        //// CreateProduct
        //// CreateProductDTO

        ////UpdateProduct
        ////UpdateProductDTO

        ////DeleteProduct
        ////DeleteProductDTO

        ////[Fact]
        ////public async Task CreateValue_ReturnSomathing()
        ////{
        ////    var mockUow = new Mock<IUnitOfWork>();
        ////    var mockMapper = new Mock<IMapper>();
        ////    var mockLogger = new Mock<ILogger<ProductService>>();


        ////}
    }
}
