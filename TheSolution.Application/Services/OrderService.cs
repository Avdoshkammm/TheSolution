using AutoMapper;
using Microsoft.Extensions.Logging;
using TheSolution.Application.DTO;
using TheSolution.Application.Interfaces;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;

namespace TheSolution.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogger<OrderService> logger;
        public OrderService(IUnitOfWork _uow, IMapper _mapper, ILogger<OrderService> _logger)
        {
            uow = _uow;
            mapper = _mapper;
            logger = _logger;
        }

        public async Task<IEnumerable<OrderProductDTO>> GetOrders()
        {
            IEnumerable<OrderProduct> orders = await uow.Orders.GetAllOrders();
            try
            {
                logger.LogInformation("GetAllOrders service");
                return mapper.Map<IEnumerable<OrderProductDTO>>(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source,"\nGetAllOrders service error");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userID)
        {
            IEnumerable<Order> uOrders = await uow.Orders.GetUserOrders(userID);
            try
            {
                logger.LogInformation("GetUserOrders service");
                return mapper.Map<IEnumerable<OrderDTO>>(uOrders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source,"\nGetUserOrders service error");
                throw;
            }
        }

        public async Task<OrderDTO> GetOrderInfo(int id)
        {
            Order order = await uow.Orders.GetOrderInfo(id);
            try
            {
                logger.LogInformation($"GetOrderInfo {id} service");
                return mapper.Map<OrderDTO>(order);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nGetOrderInfo service error");
                throw;
            }
        }

        public async Task CreateNewOrder(string userID, int productID, int quantity, OrderDTO orderdto)
        {
            await uow.BeginTransactionAsync();
            try
            {
                Task<bool> checkQuantity = uow.Orders.CheckQuantity(productID, quantity);
                if(checkQuantity.Result == false)
                {
                    throw new InvalidOperationException("Продукта недостаточно для заказа");
                }
                var createOrder = uow.Orders.CreateOrder(userID, quantity);
                Task<Order> checkCurrentOrder = uow.Orders.GetOrderInfo(createOrder.Id);
                await uow.Orders.CreateOrderInfo(createOrder.Id, productID);
                await uow.Orders.UpdateProductQuantity(productID,quantity);
                await uow.SaveChangesAsync();
                await uow.CommitTransactionAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nCreateNewOrder service error");
                await uow.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
