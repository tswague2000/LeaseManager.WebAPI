using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.PropertyDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
  [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

     public PropertyController(IPropertyService propertyService)
 {
         _propertyService = propertyService;
        }

   [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyService.GetAllAsync();
   return Ok(properties);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
var property = await _propertyService.GetByIdAsync(id);
if (property == null) return NotFound();
return Ok(property);
}

[HttpPost]
public async Task<IActionResult> Create(PropertyCreateDto dto)
{
var property = await _propertyService.CreateAsync(dto);
return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
}

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, PropertyUpdateDto dto)
{
var success = await _propertyService.UpdateAsync(id, dto);
if (!success) return NotFound();
return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var success = await _propertyService.DeleteAsync(id);
if (!success) return NotFound();
return NoContent();
}
}
}