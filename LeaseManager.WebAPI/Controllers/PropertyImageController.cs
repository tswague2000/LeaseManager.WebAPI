using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.PropertyImageDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
 [Route("api/[controller]")]
    public class PropertyImageController : ControllerBase
    {
   private readonly IPropertyImageService _propertyImageService;

   public PropertyImageController(IPropertyImageService propertyImageService)
 {
      _propertyImageService = propertyImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
     var images = await _propertyImageService.GetAllAsync();
     return Ok(images);
        }

  [HttpGet("{id}")]
     public async Task<IActionResult> GetById(int id)
  {
     var image = await _propertyImageService.GetByIdAsync(id);
            if (image == null) return NotFound();
  return Ok(image);
  }

        [HttpPost]
   public async Task<IActionResult> Create(PropertyImageCreateDto dto)
        {
            var image = await _propertyImageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
        }

 [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
        {
 var success = await _propertyImageService.DeleteAsync(id);
            if (!success) return NotFound();
    return NoContent();
        }
    }
}