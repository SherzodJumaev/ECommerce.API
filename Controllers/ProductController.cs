﻿using ECommerce.API.DTOs.ProductDTOs;
using ECommerce.API.Helpers;
using ECommerce.API.Interfaces;
using ECommerce.API.Mappers.ProductMaps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retrieves a list of products based on the provided query parameters.
        /// </summary>
        /// <param name="query">Filtering, sorting, and pagination options for retrieving products.</param>
        /// <returns>A list of products that match the query parameters.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var products = await _productRepository.GetAllAsync(query);
                var allProducts = products.Select(p => p.FromProductToProductDto());

                Log.Information($"Products fetched successfully.");

                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to fetch Products");
                throw; // Let the error middleware handle the exception
            }
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>
        /// Returns 200 (OK) with the product if found,  
        /// 400 (Bad Request) if the ID is invalid,  
        /// or 404 (Not Found) if no product exists with the given ID.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (id <= 0)
                {
                    return BadRequest();
                }
                else if (product == null)
                {
                    return NotFound($"Product with the given id:{id} is not found.");
                }

                Log.Information($"Product with the given ID: {id} fetched successfully.");

                return Ok(product.FromProductToProductDto());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to fetch Product.");
                throw; // Let the error middleware handle the exception
            }
        }

        /// <summary>
        /// Creates a new product based on the provided product data.
        /// </summary>
        /// <param name="productDto">The data transfer object containing the details of the product to create.</param>
        /// <returns>
        /// Returns 201 (Created) with the created product and its location if successful,  
        /// 400 (Bad Request) if the product name is missing,  
        /// 404 (Not Found) if the specified category does not exist,  
        /// or throws an internal server error if an exception occurs during creation.
        /// </returns>
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(productDto.Name))
            {
                //Log.Warning("Product creation failed due to missing name.");
                return BadRequest(new { Message = "Product name is required.", ErrorCode = "ERR_MISSING_PRODUCT_NAME" });
            }

            try
            {
                var createdProduct = await _productRepository.CreateAsync(productDto.CategoryId, productDto.CreateFromProductDtoToProduct());

                if (createdProduct == null)
                {
                    return NotFound($"Category that correspond to the id:{productDto.CategoryId} is not exist.");
                }

                Log.Information($"Product {createdProduct} created with ID {createdProduct.Id}", createdProduct.Name, createdProduct.Id);

                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct.FromProductToProductDto());

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to create product {productDto.Name}");
                throw; // Let the error middleware handle the exception
            }
        }

        /// <summary>
        /// Updates an existing product with the provided data.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The data transfer object containing the updated product details.</param>
        /// <returns>
        /// Returns 200 (OK) with the updated product if successful,  
        /// 400 (Bad Request) if the ID is invalid,  
        /// or 404 (Not Found) if no product exists with the given ID.
        /// </returns>
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedProduct = await _productRepository.UpdateAsync(id, productDto);

                if (id <= 0)
                {
                    return BadRequest();
                }
                else if (updatedProduct == null)
                {
                    return NotFound($"Product with the given id:{id} is not found.");
                }

                Log.Information($"Product {updatedProduct.Name} updated successfully with ID {updatedProduct.Id}");

                return Ok(updatedProduct.FromProductToProductDto());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to update product {productDto.Name}");
                throw; // Let the error middleware handle the exception
            }
        }

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>
        /// Returns 204 (No Content) if the product was successfully deleted,  
        /// 400 (Bad Request) if the ID is invalid,  
        /// or 404 (Not Found) if no product exists with the given ID.
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var deletedProduct = await _productRepository.DeleteAsync(id);

                if (id <= 0)
                {
                    return BadRequest();
                }
                else if (deletedProduct == null)
                {
                    return NotFound($"Product with the given id:{id} is not found.");
                }

                Log.Information($"Product {deletedProduct.Name} deleted successfully with ID {deletedProduct.Id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete product");
                throw; // Let the error middleware handle the exception
            }
        }
    }
}
