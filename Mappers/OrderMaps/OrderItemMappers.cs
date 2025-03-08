using ECommerce.API.DTOs.OrderDTOs;
using ECommerce.API.Models;

namespace ECommerce.API.Mappers.OrderMaps
{
    public static class OrderItemMappers
    {
        public static OrderItem ToOrderItem(this CreateOrderItemDto itemInfo)
        {
            return new OrderItem
            {
                Quantity = itemInfo.Quantity,
            };
        }
    }
}
