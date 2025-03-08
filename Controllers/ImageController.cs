using ECommerce.API.Interfaces;
using ECommerce.API.Mappers.ProductMaps;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// Uploads an image for a product.
        /// </summary>
        /// <param name="id">The ID of the product to associate with the image.</param>
        /// <param name="file">The image file to upload.</param>
        /// <returns>The updated product with the image path or an error message.</returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UploadImage([FromRoute] int id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var product = await _imageRepository.UploadImage(id, file);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product.FromProductToProductDtoForV2());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
