﻿using ECommerce.API.DTOs.CategoryDTOs;
using ECommerce.API.Mappers.ProductMaps;
using ECommerce.API.Models;

namespace ECommerce.API.Mappers.CategoryMaps
{
    public static class CategoryMappers
    {
        public static Category CreateFromCategoryDtoToCategory(this CreateCategoryRequestDto requestDto)
        {
            return new Category
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
            };
        }

        public static CategoryDto FromCategoryToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Products = category.Products.Select(p => p.FromProductToProductDto()).ToList(),
            };
        }

        public static CategoryUpdateDto UpdateFromCategoryDtoToCategory(this UpdateCategoryDto categoryDto, int id)
        {
            return new CategoryUpdateDto
            {
                Id = id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
            };
        }

        public static CategoryUpdateDto UpdateFromCategoryDtoToCategoryDto(this Category category)
        {
            return new CategoryUpdateDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
