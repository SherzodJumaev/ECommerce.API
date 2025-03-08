using ECommerce.API.DTOs.CategoryDTOs;
using ECommerce.API.Helpers;
using ECommerce.API.Models;

namespace ECommerce.API.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CategoryQueryObject query);
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<CategoryUpdateDto> UpdateAsync(int id, CategoryUpdateDto categoryDto);
        Task<Category> DeleteAsync(int id);
        Task<bool> CategoryExists(int id);
    }
}
