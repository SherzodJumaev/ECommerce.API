﻿namespace ECommerce.API.DTOs.OrderDTOs
{
    public class OrderItemInfoForCustomer
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
