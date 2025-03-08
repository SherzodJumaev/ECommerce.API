using ECommerce.API.Models;

namespace ECommerce.API.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
        Task<Order> CreateAsync(Order order);
        Task<Order> DeleteAsync(int id);
        Task<bool> OrderExistsAsync(int id);
    }
}
