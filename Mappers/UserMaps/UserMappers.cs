using ECommerce.API.DTOs.UserDTOs;
using ECommerce.API.Models;
using System.Runtime.CompilerServices;

namespace ECommerce.API.Mappers.UserMaps
{
    public static class UserMappers
    {
        public static UserDto FromUserToUserDto(this User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
            };
        }

        public static User FromUserDtoToUser(this UserDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = userDto.Role,
            };
        }

        public static Customer FromUserDtoToCustomer(this UserDto userDto)
        {
            return new Customer
            {
                FirstName = userDto.Username,
                LastName = "",
                Address = "",
                Email = "",
                PhoneNumber = "",
            };
        }
    }
}
