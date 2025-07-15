using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;
using TheSolution.Infrastructure.Data;

namespace TheSolution.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TheSolutionDBContext db;
        private readonly ILogger<OrderRepository> logger;
        public OrderRepository(TheSolutionDBContext _db, ILogger<OrderRepository> _logger)
        {
            db = _db;
            logger = _logger;
        }


        public async Task<IEnumerable<OrderProduct>> GetAllOrders()
        {
            IEnumerable<OrderProduct> orders = await db.OrderProducts.AsNoTracking().Include(o => o.Order).ThenInclude(u => u.User).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userID)
        {
            IEnumerable<Order> userOrders = await db.Orders.AsNoTracking().Where(u => u.UserID == userID).Include(op => op.OrderProducts).ThenInclude(p => p.Product).ToListAsync();
            return userOrders;
        }

        public async Task<Order> GetOrderInfo(int id)
        {
            Order? currentOrder = await db.Orders.AsNoTracking().Include(o => o.OrderProducts).FirstOrDefaultAsync(x => x.ID == id);
            return currentOrder;
        }

        public async Task<bool> CheckQuantity(int productID, int quantity)
        {
            Product orderedProduct = await db.Products.FirstOrDefaultAsync(x => x.ID == productID); 
            if(orderedProduct.Quantity < quantity)
            {
                logger.LogError("Товара недостаточно для заказа");
                return false;
            }
            return true;
        }

        public async Task CreateOrder(string userID, int quantity)
        {
            Order order = new Order
            {
                UserID = userID,
                OrderDate = DateTime.Now,
                Quantity = quantity
            };
            await db.Orders.AddAsync(order);
        }

        public async Task CreateOrderInfo(int orderId, int productID)
        {
            Order newOrder = await db.Orders.FirstOrDefaultAsync(x => x.ID == orderId);
            Product orderedProduct = await db.Products.FirstOrDefaultAsync(x => x.ID == productID);

            OrderProduct orderinfo = new OrderProduct
            {
                OrderID = newOrder.ID,
                ProductID = orderedProduct.ID,
                TotalAmount = newOrder.Quantity * orderedProduct.Cost
            };
            await db.OrderProducts.AddAsync(orderinfo);
        }
    }
}
