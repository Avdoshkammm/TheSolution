using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Domain.Entities;

namespace TheSolution.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderProduct>> GetAllOrders();
        Task<IEnumerable<Order>> GetUserOrders(string userID);
        Task<Order> GetOrderInfo(int id);
        Task CreateOrder(string userID, int productID, int quantity);
    }
}
