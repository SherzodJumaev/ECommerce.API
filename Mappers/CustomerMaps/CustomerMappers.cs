using ECommerce.API.DTOs.CustomerDTOs;
using ECommerce.API.Models;

namespace ECommerce.API.Mappers.CustomerMaps
{
    public static class CustomerMappers
    {
        public static Customer ToCustomerFromCreate(this CustomerDto customerDto)
        {
            return new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Address = customerDto.Address,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber
            };
        }

        public static CustomerDto ToCustomerFromCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
