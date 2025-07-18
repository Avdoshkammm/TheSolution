using TheSolution.Domain.Entities;

namespace TheSolution.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderProduct>> GetAllOrders();
        Task<IEnumerable<Order>> GetUserOrders(string userID);
        Task<Order> GetOrderInfo(int id);
        Task<bool> CheckQuantity(int productID, int quantity);
        Task CreateOrder(string userID, int quantity);
        Task UpdateProductQuantity(int productID, int quantity);
        Task CreateOrderInfo(int orderId, int productID);     //Task CreateNewOrder(string userID, int productID, int quantity);
        //Task AddOrderAndInfoMinusQuantity(int orderID, int quantity);
    }
}
