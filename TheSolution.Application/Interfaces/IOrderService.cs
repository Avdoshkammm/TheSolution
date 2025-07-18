using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Application.DTO;

namespace TheSolution.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderProductDTO>> GetOrders();
        Task<IEnumerable<OrderDTO>> GetUserOrders(string userID);
        Task<OrderDTO> GetOrderInfo(int id);
        Task CreateNewOrder(string userID, int productID, int quantity, OrderDTO orderdto);
    }
}
