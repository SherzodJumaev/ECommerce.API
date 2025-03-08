namespace ECommerce.API.DTOs.OrderDTOs
{
    public class UpdateOrderRequestDto
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
