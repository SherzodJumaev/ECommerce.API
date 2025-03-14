﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.CategoryDTOs
{
    public class CreateCategoryRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Name cannot be over 10 characters.")]
        [MinLength(4, ErrorMessage = "Name should be over 4 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(30, ErrorMessage = "Description cannot be over 30 characters.")]
        [MinLength(4, ErrorMessage = "Description should be over 4 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
