using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

      public OwnerController(IOwnerService ownerService)
    {
     _ownerService = ownerService;
}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
  var owners = await _ownerService.GetAllAsync();
 return Ok(owners);
        }

        [HttpGet("{id}")]
     public async Task<IActionResult> GetById(int id)
  {
 var owner = await _ownerService.GetByIdAsync(id);
      if (owner == null) return NotFound();
   return Ok(owner);
 }

        [HttpPost]
    public async Task<IActionResult> Create([FromBody] OwnerCreateDto dto)
    {
   var owner = await _ownerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner);
}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OwnerUpdateDto dto)
        {
       var success = await _ownerService.UpdateAsync(id, dto);
   if (!success) return NotFound();
       return NoContent();
        }

        [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
  {
            var success = await _ownerService.DeleteAsync(id);
     if (!success) return NotFound();
            return NoContent();
      }
    }
}