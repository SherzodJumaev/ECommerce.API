using ECommerce.API.Models;

namespace ECommerce.API.Interfaces
{
    public interface IImageRepository
    {
        Task<Product> UploadImage(int id, IFormFile file);
    }
}
