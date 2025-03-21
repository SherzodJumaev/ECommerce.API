﻿using ECommerce.API.Enums;

namespace ECommerce.API.Helpers
{
    public class ProductQueryObject
    {
        public string Name { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public SortByNameEnum SortByName { get; set; }
        public ProductEnum SortByPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
