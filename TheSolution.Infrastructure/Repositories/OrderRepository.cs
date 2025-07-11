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

        public async Task CreateOrder(string userID,int productID, int quantity)
        {
            //1 - запись в таблицу Order
            //2 - запись в таблицу OrderProduct
            //3 - product.Quantity -- (update)

            /* 1 - Поиск продукта +
             * 2 - сравнение с количеством на складе +
             * 3 - формирование заказа +
             * 4 - добавление заказа +
             * 5 - добавление информации о заказе +
             * 6 - quantity--
             * 7 - обновление продукта
             * 8 - обновление записи в Product
             * 9 - добавление информации о заказе в OrderProduct
             * 10 - сохранение изменений
             */

            Product orderedProduct = await db.Products.FirstOrDefaultAsync(x => x.ID == productID);
            
            if (orderedProduct.Quantity <= quantity)
            {
                logger.LogError("Недостаточно товара для заказа");
                return;
            }

            Order order = new Order
            {
                UserID = userID,
                OrderDate = DateTime.UtcNow,
                Quantity = quantity,
            };

            await db.Orders.AddAsync(order);

            //Добавляется запись в таблицу Order, без sca запись в OP не будет создана в принципе
            await db.SaveChangesAsync();

            OrderProduct op = new OrderProduct()
            {
                OrderID = order.ID,
                ProductID = productID,
                TotalAmount = orderedProduct.Cost * quantity
            };

            orderedProduct.Quantity -= quantity;
            db.Products.Update(orderedProduct);
            await db.OrderProducts.AddAsync(op);

            //Сохраняется запись с информацией о товаре
            await db.SaveChangesAsync();

            //Поиск продукта
            //Product? orderedProduct = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ID == productID);
            //if(orderedProduct.Quantity < quantity)
            //{
            //    logger.LogError("Товара недостаточно для заказа");
            //    return;
            //}
            ////Создание заказа
            //Order order = new Order
            //{
            //    UserID = userID,
            //    OrderDate = DateTime.Now,
            //    TotalAmount = totalAmount,
            //};
            ////Добавление заказа в БД
            //await db.Orders.AddAsync(order);
            //await db.SaveChangesAsync();
            ////Создание информации о заказе
            //OrderProduct orderInfo = new OrderProduct
            //{
            //    OrderID = order.ID,
            //    ProductID = productID,
            //    Quantity = quantity,
            //};

            //orderedProduct.Quantity -= quantity;
            //db.Products.Update(orderedProduct);

            //await db.OrderProducts.AddAsync(orderInfo);
            //await db.SaveChangesAsync();
            
        }
    }
}
