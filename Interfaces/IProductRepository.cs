using ECommerce.API.DTOs.ProductDTOs;
using ECommerce.API.Helpers;
using ECommerce.API.Models;

namespace ECommerce.API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(ProductQueryObject query);
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(int categoryId, Product product);
        Task<Product> UpdateAsync(int id, UpdateProductRequestDto product);
        Task<Product> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
    }
}
