using ECommerce.API.Models;

namespace ECommerce.API.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
