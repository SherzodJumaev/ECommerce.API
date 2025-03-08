using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
